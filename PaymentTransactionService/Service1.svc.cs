using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PaymentTransactionService
{
    public class Service1 : IService1
    {
        private RSACryptoServiceProvider rsaKeys;
        private static readonly Dictionary<string, string> Keys = new Dictionary<string, string>();
        private  string privateXmlKeys;
        private  string publicXmlKeys;
        private const int EncraptionKeySize = 472;

        public Service1()
        {
        }
        public string GetRequestKey()
        {
            //prepare and get the key to encrypt the request and WCF can decrypt it by the private key.
            rsaKeys = new RSACryptoServiceProvider(EncraptionKeySize);
            publicXmlKeys = rsaKeys.ToXmlString(false);
            privateXmlKeys = rsaKeys.ToXmlString(true);
            //we store the private in list to make request can get the suiable private key for it
            Keys.Add(publicXmlKeys, privateXmlKeys);
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(publicXmlKeys));
        }
        public byte [] ProcessRequest(byte[] request,string base64PublicKey, string base64PublicResKey)
        {
            //convert base64 public key to string
            string publicXmlKey = Encoding.ASCII.GetString(Convert.FromBase64String(base64PublicKey));
            //prepare the object to decypt the request
            RSACryptoServiceProvider rsaDecrypt = new RSACryptoServiceProvider(EncraptionKeySize);
            rsaDecrypt.FromXmlString(Keys[publicXmlKey]);
            //Decrypt request by privite key
            byte[] decryptedBytes = rsaDecrypt.Decrypt(request, false);
            //convert request from byte array to object
            Request req = ByteArrayHelper.ByteArrayToObject<Request>(decryptedBytes);
            //convert BCD request to Normal request object
            RequestData requestData = ModelHelper.GetRequestData(req);
            //remove the key from the list because we done with it.
            Keys.Remove(publicXmlKey);
            //make response object
            ResponseData responseData = new ResponseData()
            {
                ApprovalCode = "123123",
                Message = "Success",
                DateTime = DateTime.Now,
                ResponseCode = "00"
            };
            //convert response to BCD
            Response res = ModelHelper.GetResponseObjectBcd(responseData);
            //Convert it to byte array
            byte[] jsonBytes = ByteArrayHelper.ObjectToByteArray(res, 45);
            //prepare to encypt it by public key
            string publicResKey = Encoding.ASCII.GetString(Convert.FromBase64String(base64PublicResKey));
            rsaDecrypt.FromXmlString(publicResKey);
            //encrypt the request
            byte[] encryptedJsonBytes = rsaDecrypt.Encrypt(jsonBytes, false);
            //return the response.
            return encryptedJsonBytes;
        }
    }
}
