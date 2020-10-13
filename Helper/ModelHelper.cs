using Model;
using System;

namespace Helper
{
    public class ModelHelper
    {
        public static Request GetRequestObjectBcd(RequestData requestData)
        {
            Request req = new Request()
            {

                ProcessingCode = BCDHelper.ToBcd(Convert.ToDecimal(requestData.ProcessingCode)),
                SystemTraceNr = BCDHelper.ToBcd(Convert.ToDecimal(requestData.SystemTraceNr)),
                FunctionCode = BCDHelper.ToBcd(Convert.ToDecimal(requestData.FunctionCode)),
                CardNo = BCDHelper.ToBcd(Convert.ToDecimal(requestData.CardNo)),
                CardHolder = requestData.CardHolder,
                AmountTrxn = BCDHelper.ToBcd(Convert.ToDecimal(requestData.AmountTrxn)),
                CurrencyCode = BCDHelper.ToBcd(Convert.ToDecimal(requestData.CurrencyCode)),
            };
            return req;
        }
        public static RequestData GetRequestData(Request request)
        {
            RequestData req = new RequestData()
            {
                ProcessingCode = BCDHelper.BCD5ToDecimal(request.ProcessingCode).ToString(),
                SystemTraceNr = BCDHelper.BCD5ToDecimal(request.SystemTraceNr).ToString(),
                FunctionCode = BCDHelper.BCD5ToDecimal(request.FunctionCode).ToString(),
                CardNo = BCDHelper.BCD5ToDecimal(request.CardNo).ToString(),
                CardHolder = request.CardHolder,
                AmountTrxn = BCDHelper.BCD5ToDecimal(request.AmountTrxn),
                CurrencyCode = BCDHelper.BCD5ToDecimal(request.CurrencyCode).ToString(),
            };
            return req;
        }
        public static Response GetResponseObjectBcd(ResponseData responseData)
        {
            string date = responseData.DateTime.ToString("yyyyMMddhhmm");
            decimal dateNumbers =Convert.ToDecimal(date);
            Response res = new Response()
            {
                ResponseCode = BCDHelper.ToBcd(Convert.ToDecimal(responseData.ResponseCode)),
                Message = responseData.Message,
                ApprovalCode = BCDHelper.ToBcd(Convert.ToDecimal(responseData.ApprovalCode)),
                DateTime = BCDHelper.ToBcd(dateNumbers)
            };
            return res;
        }
        public static ResponseData GetResponseData(Response response)
        {
            decimal date = BCDHelper.BCD5ToDecimal(response.DateTime);
            string dateTime = date.ToString();
            DateTime oDate = DateTime.ParseExact(dateTime, "yyyyMMddHHmm", null);
            ResponseData res = new ResponseData()
            {
                ResponseCode = BCDHelper.BCD5ToDecimal(response.ResponseCode).ToString(),
                Message = response.Message,
                ApprovalCode = BCDHelper.BCD5ToDecimal(response.ApprovalCode).ToString(),
                DateTime = oDate
            };
            return res;
        }
    }
}
