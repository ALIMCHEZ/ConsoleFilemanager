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
        private FileSystemInfo _fileInfo;
        public required FileSystemInfo FileInfo    
        {
            get => _fileInfo;
            init
            {
                if (value.Equals(null))
                    throw new ArgumentNullException(nameof(value));
                else
                {
                    _fileInfo = value;
                }
            }
        }

        public readonly FileSystemObjectType Type;

        public FileSystemObject()
        {

        }
    }
}
