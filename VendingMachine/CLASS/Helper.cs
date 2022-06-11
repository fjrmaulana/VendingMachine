using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Fajar_INSPIRO.CLASS
{
    public class Helper
    {
        public Helper()
        {

        }

        public static bool IsNumeric(string data)
        {
            double retNum;
            bool isNum = Double.TryParse(data, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
    }

  
}