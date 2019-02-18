using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Fenit.HelpTool.Core.Service.Model.Shifter;

namespace Fenit.HelpTool.Core.ShifterService.Helpers
{
    internal class Mover
    {
        private readonly Action<string> _clear;
        private readonly int _copyWeight = 4, _removeWeight = 2, _fileWeight = 2;
        private readonly Action<double> _messageAction;
        private readonly ShifterConfig _shifterConfig;
        private double _actulPercentage;
        private List<string> _dirList;
        private List<string> _fileList;
        private double _tick;

        public Mover(ShifterConfig shifterConfig, Action<double> messageAction, Action<string> clear)
        {
            _shifterConfig = shifterConfig;
            _messageAction = messageAction;
            _clear = clear;
            PrepareData();
            PreparePercentage();
        }

        private void PreparePercentage()
        {
            var fileScale = _fileWeight;
            if (_shifterConfig.CreateCopy) fileScale += _copyWeight;
            if (_shifterConfig.RemoveAll) fileScale += _removeWeight;
            _tick = GetRestPercentage() / (_dirList.Count + _fileList.Count * fileScale);
        }

        private void PrepareData()
        {
            var allExtension = _shifterConfig.AllExtension();
            var allFiles = _shifterConfig.AllFiles();

            _dirList = Directory.GetDirectories(_shifterConfig.SourcePath, "*", SearchOption.AllDirectories).ToList();
            var files = Directory.GetFiles(_shifterConfig.SourcePath, "*", SearchOption.AllDirectories);
            _fileList = new List<string>();
            foreach (var file in files)
            {
                var info = new FileInfo(file);
                if (!allExtension.Contains(info.Extension) && !allFiles.Contains(info.Name)) _fileList.Add(file);
            }

            Message(2);
        }

        private void Message(double addPercentage)
        {
            _actulPercentage = _actulPercentage + addPercentage;
            _messageAction.Invoke(_actulPercentage);
        }

        public void Work()
        {
            if (_shifterConfig.CreateCopy)
            {
                CreateCopy(_shifterConfig.DestinationPath);
                Message(_copyWeight * _tick * _fileList.Count);
            }

            if (_shifterConfig.RemoveAll)
            {
                _clear(_shifterConfig.DestinationPath);
                Message(_removeWeight * _tick * _fileList.Count);
            }

            Copy();
        }

        private void Copy()
        {
            foreach (var dirPath in _dirList)
            {
                Directory.CreateDirectory(dirPath.Replace(_shifterConfig.SourcePath, _shifterConfig.DestinationPath));
                Message(_tick);
            }

            foreach (var file in _fileList)
            {
                File.Copy(file,
                    file.Replace(_shifterConfig.SourcePath, _shifterConfig.DestinationPath),
                    true);
                Message(_tick * _fileWeight);
            }
        }

        private double GetRestPercentage()
        {
            return 100 - _actulPercentage;
        }

        private void CreateCopy(string path)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var name = $"back_{DateTime.Now:yyyyMMddhhmmss}.zip";
            var zipPath = Path.Combine(dir, name);
            ZipFile.CreateFromDirectory(path, zipPath);
            File.Copy(zipPath, Path.Combine(path, name), true);
        }
    }
}