using ConsoleFilemanager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleFilemanager.Services
{
    internal class FileSystemObjectService : IFileSystemObjectService
    {
        public async Task DeleteAsync(string path)
        {
            File.Delete(path);
        }

        public async Task<List<FileSystemObject>> GetFilesAsync(string path)
        {
            return new DirectoryInfo(path).GetFileSystemInfos().Select(f => new FileSystemObject() { FileInfo = f }).ToList();
        }
    }
}
