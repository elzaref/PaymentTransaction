using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class BCDHelper
    {
        public static byte[] ToBcd(decimal value)
        {
            if (value < 0 || value > 999999999999999999)
                throw new ArgumentOutOfRangeException("value");
            int half1=0, half2=0, storeHalf1;
            for(decimal i = 1000000000; i >= 100; i/= 10)
            {
                //10100
                //100
                //1
                //
                //100
                //10
                half1 = (int)(value % i);
                storeHalf1 = half1;
                half2 = (int)(value / i);
                if((half2==0)||( (value%(i/10))!=half1 ))
                {
                    break;
                }
            }
            //int half1 = (int)(value % 1000000000);
            //int storeHalf1 = half1;
            //int half2 = (int)(value / 1000000000);

            //byte[] ret = new byte[7
            byte ret;
            Double num = ((double)(value.ToString().Length)) / 2;
            int len = (int)(Math.Ceiling(num));
            if (len < 3)
                len = 2;
            //double lim= ((double)(half1.ToString().Length)) / 2;
            //int limit= (int)(Math.Ceiling(lim));
            List<byte> lis = new List<byte>();
            for (int i = 0; i < len; i++)
            {
                if (half1.ToString().Length == 1 && half2>0)
                {
                    half1 = (int)(((double)(((double)half1 / 10) + half2)) * 10);
                    half2 = 0;
                }
                //else if (i == 3 && half2 <= 0)
                //    break;
                ret = (byte)(half1 % 10);
                half1 /= 10;
                if (half1.ToString().Length == 1 && half2 > 0)
                {
                    half1 = (int)(((double)(((double)half1 / 10) + half2)) * 10);
                    half2 = 0;
                }
                ret |= (byte)((half1 % 10) << 4);
                lis.Add(ret);
                half1 /= 10;
            }
            byte[] result = new byte[lis.Count];

            for (int i = 0; i < lis.Count; i++)
            {
                result[i] = lis[i];
            }
            return result;
        }

        public static decimal BCD5ToDecimal(byte [] bcd)
        {
            decimal outInt = 0;

            for (int i = 0; i < bcd.Length; i++)
            {
                decimal mul = (decimal)Math.Pow(10, (i * 2));
                outInt += (decimal)(((bcd[i] & 0xF)) * mul);
                mul = (decimal)Math.Pow(10, (i * 2) + 1);
                outInt += (decimal)(((bcd[i] >> 4)) * mul);
            }

            return outInt;
        }
    }
}
