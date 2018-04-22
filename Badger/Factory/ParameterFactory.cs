using Badger.Model;
using Mono.Options;
using System.IO;

namespace Badger.Factory
{
    public interface IParameterFactory
    {
        ParameterModel GetParameter(string[] args);
    }

    public class ParameterFactory : IParameterFactory
    {
        public ParameterModel GetParameter(string[] args)
        {
            var options = new OptionSet();
            var p = new ParameterModel();

            options.Add("?|help", "shows this help", s => p.Action = ActionType.ShowHelp);
            options.Add("o=|output-file", "the output file to write to", s => {
                p.OutputFile = s;
                p.Action = ActionType.CreateImage;
            });
            options.Add("l=|label", "the label of badge (left-side)", s => { p.Label = s; });
            options.Add("lc=|label-color", "the background color of the left-side", s => { p.LabelColor = s; });
            options.Add("r=|result", "the result of badge (right-side)", s => { p.Result = s; });
            options.Add("rc=|result-color", "the background color of the right-side", s => { p.ResultColor = s; });
            options.Add("h=|height", "the height of the badge", s => { if (int.TryParse(s, out int height)) p.Height = height; });

            options.Parse(args);

            /* actiontype is automatically used when label and result is supplied */
            if (!string.IsNullOrWhiteSpace(p.Label)
             && !string.IsNullOrWhiteSpace(p.Result))
                p.Action = ActionType.CreateImage;

            /* set help text */
            StringWriter sw = new StringWriter();
            options.WriteOptionDescriptions(sw);
            p.HelpText = sw.ToString();

            return p;
        }
    }
}
