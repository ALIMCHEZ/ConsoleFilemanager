using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.Core.Models
{
    internal class FileSystemObject
    {
        private string _path;
        public required string Path    
        {
            get => _path;
            init
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));
                else
                {
                    _path = value;
                }
            }
        }

        public readonly FileSystemObjectType Type;

        public FileSystemObject(string fileSystemObjectPath)
        {
            Path = fileSystemObjectPath;
        }
    }
}
