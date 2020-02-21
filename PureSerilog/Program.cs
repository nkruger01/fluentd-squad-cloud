using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;

namespace PureSerilog
{
    class Program
    {
        static void Main(string[] args)
        {
            Api.Log("alo mãe");
        }
    }

    public class Api
    {
        private static readonly Logger _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Users\nathan.kruger\Desktop\log.log")
                .CreateLogger();

        public static void Log(string message)
        {
            _logger.Information(message);
        }
    }
}
