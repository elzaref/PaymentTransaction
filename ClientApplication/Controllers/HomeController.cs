using Helper;
using Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace ClientApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceReference1.Service1Client srv;
        private readonly RSACryptoServiceProvider rsaEncrypt;
        private const int EncraptionKeySize= 472;
        public HomeController()
        {
            srv = new ServiceReference1.Service1Client();
            rsaEncrypt = new RSACryptoServiceProvider(EncraptionKeySize);
        }
        
        [HttpPost]
        public ActionResult Process(RequestData reqData)
        {
            /*Steps
             1- get request object from view and convert it to BCD to reduce request size as requested
             2- get public key for Encryption from WCF to ecrypt request with it and WCF service will 
             decrypt it by private key.
             3-convert request to byte array.
             4- Encrypt the request and send it with public key of request and new public key for service to can
             Encrypt it's response.
             5-send request
             6- WCF service will make reverse operation like 
             7-decrypt the request by privite key.
             8-convert byte array to object.
             9-convert bcd object to normal object
             10- then make this steps for response with public key for ecrypt response.
            */
            ///////////////////////
            //convert request to BCD
            Request req = ModelHelper.GetRequestObjectBcd(reqData);
            //get Encrypted public key from the WCF service base64
            string base64PublicKey = GetRequestKey();
            //convert base64 public key to string
            string publicReqKey = Encoding.ASCII.GetString(Convert.FromBase64String(base64PublicKey));
            //convert the request to byte array to can encript it.
            byte[] jsonBytes = ByteArrayHelper.ObjectToByteArray(req, 48);
            //prepare for encraption by public key
            rsaEncrypt.FromXmlString(publicReqKey);
            //Encrypt the request by public key from WCF
            byte[] encryptedJsonBytes = rsaEncrypt.Encrypt(jsonBytes, false);
            //prepare public key for response to encript it in WCF service
            RSACryptoServiceProvider rsaEncryptRes = new RSACryptoServiceProvider(EncraptionKeySize);
            string publicResKeys = rsaEncryptRes.ToXmlString(false);
            string privateResKeys = rsaEncryptRes.ToXmlString(true);
            string base64PublicResKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(publicResKeys));
            //Make a request to WCF service with public key for request and response and request Encrypted
            var response = srv.ProcessRequest(encryptedJsonBytes, base64PublicKey, base64PublicResKey);
            //Decrypt the response
            byte[] decryptedBytes = rsaEncryptRes.Decrypt(response, false);
            //convert response from byte array to object
            Response res = ByteArrayHelper.ByteArrayToObject<Response>(decryptedBytes);
            //convert BCD response to Normal response object
            ResponseData responseData = ModelHelper.GetResponseData(res);
            //view the response in view
            return View(responseData);

        }
       
        
        [HttpGet]
        public string GetRequestKey()
        {
            //get request public key from WCF service.
            string publicXmlKey = srv.GetRequestKey();
            return publicXmlKey;
        }
        public ActionResult Index()
        {
            RequestData req = new RequestData();
            return View(req);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}