﻿namespace Badger.Model
{
    public enum OutputFileFormat { Svg, Png }

    public enum ActionType { ShowHelp, CreateImage }

    public class ParameterModel
    {
        public ActionType Action { get; set; }

        public string Label { get; set; }
        public string Result { get; set; }
        public string ResultColor { get; set; }

        public string OutputFile { get; set; }
        public OutputFileFormat OutputFileFormat { get; set; }

        public string HelpText { get; set; }
    }
}