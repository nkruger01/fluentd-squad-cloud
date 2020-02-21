using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using System;
using System.Collections.Generic;

namespace FluentD
{
    class Program
    {
        private static List<string> _cpfs = new List<string>
        {
            "001.001.001-00",
            "001.001.002-00",
            "001.001.003-00",
            "001.001.004-00",
            "001.005.001-00",
            "001.006.002-00",
            "001.007.003-00",
            "001.008.004-00",
            "002.001.001-00",
            "002.001.002-00",
            "002.001.003-00",
            "002.001.004-00",
            "002.005.001-00",
            "002.006.002-00",
            "002.007.003-00",
            "002.008.004-00",
        };


        static void Main(string[] args)
        {
            var random = new Random();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Tentando enviar mensagem " + i);

                LGPDRecord objeto = new LGPDRecord
                {
                    CPFAcesso = _cpfs[random.Next(0,_cpfs.Count)],
                    DataHoraAcesso = DateTime.Now,
                    CPFVisualizado= _cpfs[random.Next(0, _cpfs.Count)],
                    DadosPessoaisVisualizados = new List<string> {"CPF", "email", "endereço" },
                    DadosSensiveisVisualizados = new List<string> { "religião", "raça", "orientacao sexual", "salario" },
                };
                Api.Log("Mensagem número {num}. Objeto: {@objeto}", i, objeto);
            }

            Api.Flush();
        }
    }

    public class Api
    {
        private static readonly Logger _logger = new LoggerConfiguration()
                .WriteTo.Fluentd("localhost", 24224, "Pare")
                .CreateLogger();

        static Api()
        {
            SelfLog.Enable(Console.Out);
        }

        public static void Log(string message)
        {
            _logger.Information(message);
        }

        public static void Log(string message, params object[] parameters)
        {
            _logger.Information(message, parameters);
        }

        public static void Log(object obj)
        {
            _logger.Information("{@object}", obj);
        }

        public static void Flush()
        {
            System.Threading.Thread.Sleep(100000);
            _logger.Dispose();
        }
    } 
    public class LGPDRecord
    {
        public string CPFAcesso { get; set; }
        public DateTime DataHoraAcesso { get; set; }
        public string CPFVisualizado { get; set; }
        public List<string> DadosPessoaisVisualizados { get; set; }
        public List<string> DadosSensiveisVisualizados { get; set; }
    }
}