using ConsoleFilemanager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.Services
{
    internal interface IFileSystemObjectService
    {
        Task<List<FileSystemObject>> GetFilesAsync(string path);
        Task DeleteAsync(string path);
    }
}
