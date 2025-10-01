using ConsoleFilemanager.Core.Models;
using ConsoleFilemanager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.UI
{
    internal class UserInterface
    {
        private FileSystemObjectService _objectService { get; set; }
        private List<FileSystemObject> fileSystemObjects { get; set; }

        private StringBuilder _strBuilder;
        
        private string _startFolder;
        public string StartFolder
        {
            get=>_startFolder;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _startFolder = value;
            }
        }



        public UserInterface(string startFolder)
        {
            StartFolder = startFolder;
            _objectService = new FileSystemObjectService();
            fileSystemObjects = _objectService.GetFilesAsync(_startFolder).Result;
            BuilUi();
        }

        private void BuilUi()
        {
            BuildCurrentfolderWindow();
            BuildFilesWindow();
        }

        private void BuildFilesWindow()
        {
            var max = fileSystemObjects.Max(f => f.FileInfo.Name.Length);
            _strBuilder = new StringBuilder();
            _strBuilder.Append("+");
            for (int i = 0; i < max; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.Append("+");
            Console.WriteLine(_strBuilder.ToString());
            foreach (var file in fileSystemObjects)
            {
                Console.WriteLine("|"+file.FileInfo.Name+new string(' ', max - file.FileInfo.Name.Length)+"|");
            }
            Console.WriteLine(_strBuilder.ToString());
        }

        private void BuildCurrentfolderWindow()
        {
            var max = fileSystemObjects.Max(fs => fs.FileInfo.Name.Length);
            _strBuilder = new StringBuilder();
            _strBuilder.Append("+");
            for (int i = 0; i < max; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.Append("+");
            Console.WriteLine(_strBuilder.ToString());
            Console.WriteLine("|" + _startFolder+ new string(' ', max - _startFolder.Length)+"|");
        }
    }
}
