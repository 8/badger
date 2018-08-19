using System;
using System.Reflection;
using Badger.Factory;
using Badger.Model;
using Badger.Service;

namespace Badger
{
    public class Program
    {
        private readonly IParameterFactory ParameterFactory;
        private readonly ISvgService SvgService;

        public Program(IParameterFactory parameterFactory,
                       ISvgService svgService)
        {
            this.ParameterFactory = parameterFactory;
            this.SvgService = svgService;
        }

        public void Run(string[] args)
        {
            var p = this.ParameterFactory.GetParameter(args);

            HandleParameters(p);
        }

        private void WriteVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine($"badger v{version}");
        }

        private void WriteExampleUsage()
        {
            Console.WriteLine("Example usage:");
            Console.WriteLine("\tbadger.exe -o result-success.svg -l \"Testresults\" -r \"100 / 100\" --lc #444444ff --rc #00ff00ff");
        }

        private void WriteHelp(ParameterModel p)
        {
            WriteVersion();
            Console.WriteLine(p.HelpText);
            WriteExampleUsage();
        }

        private void HandleParameters(ParameterModel p)
        {
            switch (p.Action )
            {
                case ActionType.ShowHelp:
                    WriteHelp(p);
                    break;

                case ActionType.CreateImage:
                    Console.WriteLine($"writing file '{p.OutputFile}'");
                    this.CreateImage(p);
                    break;
            }
        }

        private void CreateImage(ParameterModel p)
        {
            switch (p.OutputFileFormat)
            {
                case OutputFileFormat.Svg:
                    this.SvgService.Draw(p);
                    break;
            }
        }
    }
}
