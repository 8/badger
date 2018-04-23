using Badger.Factory;
using Badger.Model;
using System;
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

        private void HandleParameters(ParameterModel p)
        {
            switch (p.Action )
            {
                case ActionType.ShowHelp:
                    Console.WriteLine(p.HelpText);
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
