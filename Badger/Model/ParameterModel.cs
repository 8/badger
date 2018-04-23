namespace Badger.Model
{
    public enum OutputFileFormat { Svg, Png }

    public enum ActionType { ShowHelp, CreateImage }

    public class ParameterModel
    {
        public ActionType Action { get; set; }

        public string Label { get; set; }
        public string Result { get; set; }
        public string ResultColor { get; set; }
        public string LabelColor { get; set; }

        public int Height { get; set; }

        public string OutputFile { get; set; }
        public OutputFileFormat OutputFileFormat { get; set; }

        public string HelpText { get; set; }

        public ParameterModel()
        {
            this.OutputFile = "out.svg";
            this.OutputFileFormat = OutputFileFormat.Svg;
            this.Action = ActionType.ShowHelp;
            this.Height = 22;
            this.LabelColor  = "#333333ff";
            this.ResultColor = "#00ff00ff";
        }
    }
}
