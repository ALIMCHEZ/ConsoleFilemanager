using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.Services
{
    internal interface IFileService
    {
        Task<List<string>> GetFilesAsync(string path);
        Task DeleteAsync(string path);
    }
}
