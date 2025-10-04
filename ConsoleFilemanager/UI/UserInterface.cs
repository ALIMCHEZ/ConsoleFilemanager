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

        private List<FileSystemObject> currentFiles { get; set; }

        private StringBuilder _strBuilder = new StringBuilder();
        private int selectIndex = 0;
        
        private string _startFolder;

        private const int WINDOW_WIDTH = 100;
        private const int WINDOW_HEIGHT = 30;
        private const int LIST_START_ROW = 5;
        private const int LIST_END_ROW = 25;
        private const int FILES_WINDOW_WIDTH = 50;

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
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            await Init(startFolder);

            await Task.Delay(50);

            while (true)
            {
                if (rebuild)
                {
                    Console.Clear();
                    await Task.Delay(50);
                    BuildCurrentfolderWindow();
                    BuildFilesWindow();
                    BuildFileinfoWondow();
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


        private void BuildFilesWindow()
        {
            _strBuilder.Clear();
            _strBuilder.Append("+");
            for (int i = 0; i < FILES_WINDOW_WIDTH; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.AppendLine("+");
            int offset = selectIndex >= LIST_END_ROW ? selectIndex - LIST_END_ROW +1: 0;
            for (int i = 0 + offset; i<LIST_END_ROW + offset; i++)
            {
                if (i >=fileSystemObjects.Count)
                {
                    _strBuilder.AppendLine("|" + new string(' ', FILES_WINDOW_WIDTH) + "|");
                    continue;
                }
                if (i == selectIndex)
                {
                    _strBuilder.AppendLine("\u001b[41m|" + fileSystemObjects[i].FileInfo.Name + new string(' ', FILES_WINDOW_WIDTH - fileSystemObjects[i].FileInfo.Name.Length-1) + "|\u001b[0m");
                }
                else
                {
                    _strBuilder.AppendLine("|" + fileSystemObjects[i].FileInfo.Name + new string(' ', FILES_WINDOW_WIDTH - fileSystemObjects[i].FileInfo.Name.Length) + "|");
                }
            }
            _strBuilder.Append("+" + new string('-', FILES_WINDOW_WIDTH-1) + "+");
            Console.WriteLine(_strBuilder.ToString());
        }

        private void BuildCurrentfolderWindow()
        {
            _strBuilder.Clear();
            _strBuilder.Append("+");
            for (int i = 0; i < WINDOW_WIDTH; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.AppendLine("+");
            _strBuilder.Append("|" + _startFolder + new string(' ', Math.Abs(WINDOW_WIDTH - _startFolder.Length)) + "|");
            Console.WriteLine(_strBuilder.ToString());          
        }
        
        private void BuildFileinfoWondow()
        {
            _strBuilder.Clear();
            _strBuilder.Append("+");
            for (int i = 0; i < FILES_WINDOW_WIDTH; i++)
            {
                _strBuilder.Append("-");
            }
            _strBuilder.Append("+");
            Console.SetCursorPosition(FILES_WINDOW_WIDTH, 2);
            Console.WriteLine(_strBuilder.ToString());
            for (int i = 0; i < LIST_END_ROW; i++)
            {
                Console.SetCursorPosition(FILES_WINDOW_WIDTH, 3 + i);
                Console.WriteLine("|" + new string(' ', FILES_WINDOW_WIDTH) + "|");
            }
            Console.SetCursorPosition(FILES_WINDOW_WIDTH, 28);
            Console.WriteLine("+" + new string('-', FILES_WINDOW_WIDTH) + "+");
        }

        private async Task HandleButton(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectIndex > 0)
                    {
                        selectIndex--;
                        rebuild = true;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (fileSystemObjects.Count > selectIndex + 1)
                    {
                        selectIndex++;
                        rebuild = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (fileSystemObjects[selectIndex].FileInfo as DirectoryInfo != null)
                    {
                        await Init(fileSystemObjects[selectIndex].FileInfo.FullName);
                        rebuild = true;
                    }
                    break;
                case ConsoleKey.Backspace:
                    var dir = new DirectoryInfo(_startFolder);
                    if (dir?.Parent != null)
                    {
                        await Init(dir.Parent.FullName);
                    }
                    rebuild = true;
                    break;
            }
        }
    }
}
