using System;
using System.Collections.Generic;
using System.Text;

namespace ws_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new eu.europa.ec.checkVatService();
            bool valid;
            string name, address;
            string countryCode = "EL";
            string vatNumber;

            if (args.Length > 0)
            {
                vatNumber = args[0];
                try
                {
                    DateTime dt = service.checkVat(ref countryCode, ref vatNumber, out valid, out name, out address);
                    var tmp = name.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"status\" : "); sb.Append(valid ? "true" : "false");
                    sb.Append(",\"name\" : \""); sb.Append((valid && (tmp.Length > 0)) ? tmp[0] : string.Empty);
                    sb.Append("\",\"title\" : \""); sb.Append((valid && (tmp.Length > 1)) ? tmp[1] : string.Empty);
                    sb.Append("\",\"afm\" : \""); sb.Append(vatNumber);
                    sb.Append("\",\"address\" : \""); sb.Append(address);
                    sb.Append("\"}");
                    Console.WriteLine(sb.ToString());
                }
                catch (Exception)
                {
                    Console.WriteLine("{\"status\" : null,\"name\" : null,\"title\" : null,\"afm\" : null,\"address\" : null}");
                }
            }
        }
    }
}
