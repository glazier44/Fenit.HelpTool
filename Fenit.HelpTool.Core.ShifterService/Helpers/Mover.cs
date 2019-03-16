using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using Fenit.HelpTool.Core.Service.Model.Extension;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.ShifterService.Helpers
{
    internal class Mover
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Action<string> _clear;
        private readonly int _copyWeight = 4, _removeWeight = 2, _fileWeight = 2;
        private readonly Action<double> _messageAction;
        private readonly ShifterConfig _shifterConfig;
        private double _actulPercentage, _tick;
        private List<string> _dirList;
        private List<string> _fileList;
        private string _zipPath;
        private readonly string _backUpZipDir;

        public Mover(ShifterConfig shifterConfig, Action<double> messageAction, Action<string> clear,
            CancellationTokenSource cancellationTokenSource)
        {
            _shifterConfig = shifterConfig;
            _messageAction = messageAction;
            _clear = clear;
            _backUpZipDir = "ZipBack";
            PrepareData();
            PreparePercentage();
            _cancellationToken = cancellationTokenSource.Token;
        }

        private bool IsCancel()
        {
            return _cancellationToken.IsCancellationRequested;
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

        public Response Work()
        {
            if (_shifterConfig.CreateCopy && !IsCancel())
            {
                CreateCopy();
                Message(_copyWeight * _tick * _fileList.Count);
            }

            if (_shifterConfig.RemoveAll && !IsCancel())
            {
                _clear(_shifterConfig.DestinationPath);
                Message(_removeWeight * _tick * _fileList.Count);
            }

            if (IsCancel() || !Copy())
            {
                Rolback();
                return Response.CreateError("Kopiowanie anulowano!");
            }

            return Response.CreateSucces();
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private bool Copy()
        {
            foreach (var dirPath in _dirList)
            {
                if (IsCancel()) return false;
                CreateDirectory(dirPath.Replace(_shifterConfig.SourcePath, _shifterConfig.DestinationPath));
                Message(_tick);
            }

            foreach (var file in _fileList)
            {
                if (IsCancel()) return false;
                File.Copy(file,
                    file.Replace(_shifterConfig.SourcePath, _shifterConfig.DestinationPath),
                    _shifterConfig.Override);
                Message(_tick * _fileWeight);
            }

            return true;
        }

        private double GetRestPercentage()
        {
            return 100 - _actulPercentage;
        }

        private void CreateCopy()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _backUpZipDir);
            CreateDirectory(dir);

            var name = $"back_{DateTime.Now:yyyyMMddhhmmss}.zip";
            _zipPath = Path.Combine(dir, name);
            ZipFile.CreateFromDirectory(_shifterConfig.DestinationPath, _zipPath);
        }

        private void Rolback()
        {
            _clear(_shifterConfig.DestinationPath);
            ZipFile.ExtractToDirectory(_zipPath, _shifterConfig.DestinationPath);
        }
    }
}