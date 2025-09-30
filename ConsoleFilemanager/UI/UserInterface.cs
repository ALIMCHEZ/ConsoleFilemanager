using ConsoleFilemanager.Core.Models;
using ConsoleFilemanager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.UI
{
    internal class UserInterface
    {
        private FileSystemObjectService _objectService { get; set; }
        private List<FileSystemObject> fileSystemObjects { get; set; }

        public UserInterface(string startFolder)
        {
            _objectService = new FileSystemObjectService();
            fileSystemObjects = _objectService.GetFilesAsync(startFolder).Result;
            BuilUi();
        }

        private void BuilUi()
        {
            foreach (var file in fileSystemObjects)
            {
                Console.WriteLine(file.FileInfo.Name);
            }
        }
    }
}
