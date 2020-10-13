using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class RequestData
    {
        public string ProcessingCode { get; set; }
        public string SystemTraceNr { get; set; }
        public string FunctionCode { get; set; }
        public string CardNo { get; set; }
        public string CardHolder { get; set; }
        public decimal AmountTrxn { get; set; }
        public string CurrencyCode { get; set; }

    }
}
