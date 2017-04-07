using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuoteFaker
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                HandleQuote(args);
            }
            else
            {
                Console.WriteLine("Run from command lind QuoteFaker.exe WHOSAID WHATSAIDDDD");
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }

        static void HandleQuote(string[] args)
        {
            var whoSaid = args[0];
            var whatGendlerSaid = args[1];
            var dataObj = QuoteFaker.GenerateQuote(whoSaid, whatGendlerSaid, DateTime.Now);
            Clipboard.SetDataObject(dataObj, true);
            Console.WriteLine("Sucess!!!! Ctrl+v for funs!");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
