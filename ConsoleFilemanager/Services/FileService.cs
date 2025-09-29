using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFilemanager.Services
{
    internal class FileService : IFileService
    {
        public async Task DeleteAsync(string path)
        {
            File.Delete(path);
        }

        public async Task<List<string>> GetFilesAsync(string path)
        {
            return Directory.GetFiles(path).Select(f=>Path.GetFileName(f)).ToList();
        }
    }
}
