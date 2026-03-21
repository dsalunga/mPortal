using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.WebSystem.Utilities;

namespace DbManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var db = new DbManager();

                foreach (var arg in args)
                {
                    /*
                    var lArg = arg.ToLower();
                    switch (lArg)
                    {
                        case "/backup":
                            break;
                    }*/

                    if (arg.Equals("/backup", StringComparison.InvariantCultureIgnoreCase))
                    {
                        db.Backup((string status) => { Console.WriteLine(status.Replace(WConstants.WBREAK, Environment.NewLine)); });
                        //Console.ReadLine();

                        break;
                    }
                    else if (arg.Equals("/restore", StringComparison.InvariantCultureIgnoreCase))
                    {
                        db.Restore((string status) => { Console.WriteLine(status.Replace(WConstants.WBREAK, Environment.NewLine)); });
                        //Console.ReadLine();

                        break;
                    }
                }
            }
        }
    }
}
