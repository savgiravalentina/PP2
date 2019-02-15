using System;
using System.Collections.Generic;
using System.IO;
namespace Example1
{
    enum FSIMode // задаю параметр для дальнейшей работы с Farmanager
    {
        DirectoryInfo = 1,
        File = 2
    }
    class Layer
    {
        public DirectoryInfo[] DirectoryContent
        {
            get; //возвращаем значение
            set; // устанавливаем значение
        }
        public FileInfo[] FileContent
        {
            get;
            set;
        }
        public int SelectedIndex
        {
            get;
            set;
        }
        void SelectedColor(int i) //функция для подбора цветов в Farmanager
        {
            if (i == SelectedIndex)
                Console.BackgroundColor = ConsoleColor.Magenta;  //выбранный файл
            else
                Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < DirectoryContent.Length; ++i)
            {
                SelectedColor(i);
                Console.WriteLine((i  ) + ". " + DirectoryContent[i].Name); //порядок для папок
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < FileContent.Length; ++i)
            {
                SelectedColor(i + DirectoryContent.Length);
                Console.WriteLine((i + DirectoryContent.Length) + ". " + FileContent[i].Name);//порядок для фалов
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Program
    {
        static bool pathExists(string path, int mode)
        {
            if ((mode == 1 && new DirectoryInfo(path).Exists) || (mode == 2 && new FileInfo(path).Exists))
                return true; //если это папка и ее mode действительный, возвращаем его в farmanager
            else
                return false;
        }
        static void Main(string[] args) //основная часть программы
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\vsavg\Desktop");
            if (!dir.Exists)
            {
                Console.WriteLine("Directory not exist"); //на случай, если данной директории не существует
                return;
            }
            Layer l = new Layer
            {
                DirectoryContent = dir.GetDirectories(),
                FileContent = dir.GetFiles(),
                SelectedIndex = 0
            };
            Stack<Layer> history = new Stack<Layer>(); //создаем именно Stack для работы по принципу LIFO
            history.Push(l);
            bool esc = false; //пока кнопка выхода не нажата работает следующее:
            FSIMode curMode = FSIMode.DirectoryInfo;
            while (!esc)
            {
                if (curMode == FSIMode.DirectoryInfo)
                {
                    history.Peek().Draw();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Delete: Del | Rename: F4 | Back: Backspace | Exit: Esc");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();
                switch (consolekeyInfo.Key)  //различные случаи или кейсы при которых работают кнопки 
                {
                    case ConsoleKey.UpArrow:
                        if (history.Peek().SelectedIndex > 0)  //при перемещении кнопок вниз вверх stack начинает читать головной элемент как выбранный нами индекс
                            history.Peek().SelectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1 > history.Peek().SelectedIndex)
                            history.Peek().SelectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length == 0) //складывает в stack все элементы из файлов и папок
                            break;
                        int index = history.Peek().SelectedIndex;
                        if (index < history.Peek().DirectoryContent.Length) //если меньше количества папок то читаем как папку типа до крайней папки идут другие папки
                        {
                            DirectoryInfo d = history.Peek().DirectoryContent[index];
                            history.Push(new Layer //новый слой для появления новых папой и файлов
                            {
                                DirectoryContent = d.GetDirectories(),
                                FileContent = d.GetFiles(),
                                SelectedIndex = 0
                            });
                        }
                        else
                        {
                            curMode = FSIMode.File;
                            using (FileStream fs = new FileStream(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName, FileMode.Open, FileAccess.Read))
                            //если это файл то делаем проверку на место в ряду папок и файлов, затем открываем и читаем
                            {
                                using (StreamReader sr = new StreamReader(fs)) //читает файлы
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd());

                                }
                            }

                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (curMode == FSIMode.DirectoryInfo)
                        {
                            if (history.Count > 1) //убираем с истории слои чтобы вернуться в предыдущий то есть выйти это для папок в папках и тд
                                history.Pop();
                        }
                        else
                        {
                            curMode = FSIMode.DirectoryInfo; //если это первый слой то просто выходим из программы
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case ConsoleKey.Escape:
                        esc = true;
                        break;
                    case ConsoleKey.Delete:
                        if (curMode != FSIMode.DirectoryInfo || (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length) == 0)
                            break;
                        index = history.Peek().SelectedIndex; //присваем индексу значение выбранного
                        int ind = index;
                        if (index < history.Peek().DirectoryContent.Length) //сравниваем с индексом расположения папки
                            history.Peek().DirectoryContent[index].Delete(true); //если это папка то удаляем ее
                        else
                            history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Delete(); //случай для файлов
                        int numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 2;
                        history.Pop();
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirectoryContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirectoryContent[index];
                            Layer nl = new Layer
                            {
                                DirectoryContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    case ConsoleKey.F4:
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length == 0)
                            break;
                        index = history.Peek().SelectedIndex;
                        string name, fullname;
                        int selectedMode;
                        if (index < history.Peek().DirectoryContent.Length) //случай для папок
                        {
                            name = history.Peek().DirectoryContent[index].Name;
                            fullname = history.Peek().DirectoryContent[index].FullName;
                            selectedMode = 1;
                        }
                        else
                        {
                            name = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Name; //для файлов
                            fullname = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName;
                            selectedMode = 2;
                        }
                        fullname = fullname.Remove(fullname.Length - name.Length);
                        Console.WriteLine("Please enter the new name, to rename {0}:", name);
                        Console.WriteLine(fullname);
                        string newname = Console.ReadLine();
                        while (newname.Length == 0 || pathExists(fullname + newname, selectedMode))
                        {
                            Console.WriteLine("This directory was created, Enter the new one");
                            newname = Console.ReadLine();
                        }
                        Console.WriteLine(selectedMode);
                        if (selectedMode == 1)
                        {
                            new DirectoryInfo(history.Peek().DirectoryContent[index].FullName).MoveTo(fullname + newname);
                        }
                        else
                            new FileInfo(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName).MoveTo(fullname + newname);
                        index = history.Peek().SelectedIndex;
                        ind = index;
                        numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1;
                        history.Pop();
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirectoryContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirectoryContent[index];
                            Layer nl = new Layer
                            {
                                DirectoryContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}