using System;
using System.Collections.Generic;
using System.Linq;
using Fenit.HelpTool.Core.Service.Model.Shifter.Events;
using Fenit.Toolbox.Core.Extension;

namespace Fenit.HelpTool.Core.Service.Model.Shifter
{
    public class ShifterConfig : BaseShifterConfig, ICloneable
    {
        private string _destinationPath,
            _sourcePath,
            _type,
            _version,
            _destinationZipPath,
            _appName,
            _appNamePattern,
            _program;


        public string SourcePath
        {
            get => _sourcePath;
            set
            {
                SetProperty(ref _sourcePath, value);
                _sourcePathEdit?.Invoke(_sourcePath);
            }
        }

        public string AppName
        {
            get => _appName;
            set => SetProperty(ref _appName, value);
        }

        public string AppNamePattern
        {
            get => _appNamePattern;
            set => SetProperty(ref _appNamePattern, value);
        }

        public string DestinationPath
        {
            get => _destinationPath;
            set
            {
                SetProperty(ref _destinationPath, value);
                DestinationZipPath = _destinationPath.TrimPath();
            }
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public string Type
        {
            get => _type;
            set
            {
                SetProperty(ref _type, value);
                _typeEdit?.Invoke(_type);
            }
        }

        public string Program
        {
            get => _program;
            set => SetProperty(ref _program, value);
        }

        public string DestinationZipPath
        {
            get => _destinationZipPath;
            set => SetProperty(ref _destinationZipPath, value);
        }

        public string Pattern { get; set; }
        public string ExcludeFile { get; set; }
        public bool RemoveAll { get; set; }
        public bool CreateCopy { get; set; }
        public bool Override { get; set; } = true;
        public bool Archive { get; set; } = false;

        public object Clone()
        {
            return MemberwiseClone();
        }


        private event TypeEdit _typeEdit;

        public event TypeEdit TypeEdit
        {
            add => _typeEdit += value;
            remove => _typeEdit -= value;
        }

        private event SourcePathEdit _sourcePathEdit;

        public event SourcePathEdit SourcePathEdit
        {
            add => _sourcePathEdit += value;
            remove => _sourcePathEdit -= value;
        }

        public void PrepareAppName()
        {
            if (!string.IsNullOrEmpty(AppNamePattern))
            {
                var text = AppNamePattern.Replace("[N]", Program);
                text = text.Replace("[V]", Version);
                text = text.Replace("[D]", DateTime.Now.ToString("ddMMyyyy"));
                AppName = text;
            }
            else
            {
                AppName = Program + ".zip";
            }
        }

        public void LoadDataOnForm()
        {
            Type = Type;

        }

        public void AddId(List<ShifterConfig> shifterConfig)
        {
            Id = (shifterConfig.Any() ? shifterConfig.OrderBy(w => w.Id).Last().Id : 0) + 1;
        }
    }
}