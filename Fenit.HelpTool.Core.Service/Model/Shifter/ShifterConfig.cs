namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class ShifterConfig : BaseShifterConfig
    {
        private string _destinationPath;
        private string _sourcePath;

        public string SourcePath
        {
            get => _sourcePath;
            set => SetProperty(ref _sourcePath, value);
        }

        public string DestinationPath
        {
            get => _destinationPath;
            set => SetProperty(ref _destinationPath, value);
        }

        public string Pattern { get; set; }
        public string ExcludeFile { get; set; }
        public bool RemoveAll { get; set; }
        public bool CreateCopy { get; set; }
        public bool Override { get; set; } = true;
    }
}