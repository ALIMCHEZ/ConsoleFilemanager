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
        private FileSystemObjectService _objectService = new FileSystemObjectService();
        private List<FileSystemObject> fileSystemObjects { get; set; }

        private StringBuilder _strBuilder = new StringBuilder();
        private int selectIndex = 0;
        
        private string _startFolder;

        private bool rebuild = true;
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

        private async Task Init(string startFolder)
        {
            selectIndex = 0;
            StartFolder = startFolder;
            fileSystemObjects = await _objectService.GetFilesAsync(_startFolder);

        }

        public async Task RunAsync(string startFolder)
        {

           
            await Init(startFolder);

            await Task.Delay(50);

            while (true)
            {
                if (rebuild)
                {
                    BuilUi();
                    rebuild = false;
                }


                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    await HandleButton(key);
                }

                await Task.Delay(50);
            }
        }

        private void BuilUi()
        {
            BuildCurrentfolderWindow();
            BuildFilesWindow();
        }

        private void BuildFilesWindow()
        {

            var max = fileSystemObjects.Max(f => f.FileInfo.Name.Length);
            _strBuilder.Clear();
            _strBuilder.Append("+");
            for (int i = 0; i < max; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.Append("+");
            Console.WriteLine(_strBuilder.ToString());
            for (int i = 0; i<fileSystemObjects.Count; i++)
            {
                if (i==selectIndex)
                    Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("|" + fileSystemObjects[i].FileInfo.Name+new string(' ', max - fileSystemObjects[i].FileInfo.Name.Length)+"|");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine(_strBuilder.ToString());
        }

        private void BuildCurrentfolderWindow()
        {
            Console.Clear();
            var max = fileSystemObjects.Max(fs => fs.FileInfo.Name.Length);
            _strBuilder.Clear();
            _strBuilder.Append("+");
            for (int i = 0; i < max; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.Append("+");
            Console.WriteLine(_strBuilder.ToString());
            Console.WriteLine("|" + _startFolder+ new string(' ', Math.Abs(max - _startFolder.Length))+"|");
        }

        private async Task HandleButton(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectIndex > 0) selectIndex--;
                    rebuild = true;
                    break;

                case ConsoleKey.DownArrow:
                    if (fileSystemObjects.Count  > selectIndex + 1) selectIndex++;
                    rebuild = true;
                    break;
                case ConsoleKey.Enter:
                    if (fileSystemObjects[selectIndex].FileInfo as DirectoryInfo != null)
                        await Init(fileSystemObjects[selectIndex].FileInfo.FullName);
                    rebuild = true;
                    break;
                case ConsoleKey.Backspace:
                    var dir = new DirectoryInfo(_startFolder);
                    if (dir?.Parent!=null) 
                        await Init(dir.Parent.FullName);
                    rebuild = true;
                    break;
            }
        }
    }
}
