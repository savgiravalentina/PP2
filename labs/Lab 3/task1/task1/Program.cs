using System;
using System.Collections.Generic;
using System.IO;
namespace Example1
{
    enum FSIMode // задаю параметр для дальнейшей работы с Farmanager
    {
        DirectoryInfo = 1, // 1 for directory info as first layer=priority
        File = 2 // 2 for file info as second level=priority
    }
    class Layer
    {
        public DirectoryInfo[] DirectoryContent //what we gonna have (content) in directory info array 
        {
            get; //возвращаем значение
            set; // устанавливаем значение
        }
        public FileInfo[] FileContent // content from fileinfo array
        {
            get;
            set;
        }
        public int SelectedIndex //through the indexes we gonna change colors use buttons
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
        public void Draw() //function to show farmanagers content
        {
            Console.BackgroundColor = ConsoleColor.Black; //initial color for background
            Console.Clear(); //clear to make sure that new one will be what we need
            for (int i = 0; i < DirectoryContent.Length; ++i) // until directory content doesn't end we gonna write it as index + name as arrays name
            {
                SelectedColor(i); //selected directory
                Console.WriteLine((i  ) + ". " + DirectoryContent[i].Name); //порядок для папок
            }
            Console.ForegroundColor = ConsoleColor.Yellow; //color will obrashat' attention 
            for (int i = 0; i < FileContent.Length; ++i) //to do the same with files length
            {
                SelectedColor(i + DirectoryContent.Length); // we add directory length to continue row 
                Console.WriteLine((i + DirectoryContent.Length) + ". " + FileContent[i].Name);//порядок для фалов
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Program
    {
        static bool pathExists(string path, int mode) //task provides to make or show work with path so we have string path and his number mode
        {
            if ((mode == 1 && new DirectoryInfo(path).Exists) || (mode == 2 && new FileInfo(path).Exists)) //if mode 1 to return new directory info(path) exists or the same for files
                return true; //если это папка и ее mode действительный, возвращаем его в farmanager
            else
                return false; //even nothing is suitable
        }
        static void Main(string[] args) //основная часть программы
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\vsavg\Desktop"); //creating new directory
            if (!dir.Exists) //if directory doesnt exists we write it
            {
                Console.WriteLine("Directory not exist"); //на случай, если данной директории не существует
                return;
            }
            Layer l = new Layer //creaating new layers for better showing of far manger work content is important
            {
                DirectoryContent = dir.GetDirectories(), //get directories function
                FileContent = dir.GetFiles(), //get files function 
                SelectedIndex = 0 //index we start
            };
            Stack<Layer> history = new Stack<Layer>(); //создаем именно Stack для работы по принципу LIFO   Stack<Layer> history = new Stack<Layer>()
            history.Push(l); //adding to history new level through push
            bool esc = false; //пока кнопка выхода не нажата работает следующее:
            FSIMode curMode = FSIMode.DirectoryInfo; //idk what's that people write like that FSIMode curMode = FSIMode.DirectoryInfo;
            while (!esc) //until escape is not choosen 
            {
                if (curMode == FSIMode.DirectoryInfo) //current mode equals to fsi mode in directory info
                {
                    history.Peek().Draw(); //reaading through peek its  function and draw what we have its also a function
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Delete: Del | Rename: F4 | Back: Backspace | Exit: Esc");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();
                switch (consolekeyInfo.Key)  //различные случаи или кейсы при которых работают кнопки 
                {
                    case ConsoleKey.UpArrow:
                        if (history.Peek().SelectedIndex > 0)  //при перемещении кнопок вниз вверх stack начинает читать головной элемент как выбранный нами индекс more than 0 we peek it --
                            history.Peek().SelectedIndex--; 
                        break;
                    case ConsoleKey.DownArrow: //for down arrow we peek dircont length+ filecont length -1 more than selected index 
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1 > history.Peek().SelectedIndex)
                            history.Peek().SelectedIndex++; //adding to peek ++ selected index
                        break;
                    case ConsoleKey.Enter:
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length == 0) //складывает в stack все элементы из файлов и папок dircont filecont leng
                            break;
                        int index = history.Peek().SelectedIndex; //index equals to selected
                        if (index < history.Peek().DirectoryContent.Length) //если меньше количества папок то читаем как папку типа до крайней папки идут другие папки history.Peek().DirectoryContent.Length
                        {
                            DirectoryInfo d = history.Peek().DirectoryContent[index];
                            history.Push(new Layer //новый слой для появления новых папой и файлов in dir info d history.Peek().DirectoryContent[index]
                            {
                                DirectoryContent = d.GetDirectories(), //getting directories content
                                FileContent = d.GetFiles(), //getting files content
                                SelectedIndex = 0
                            });
                        }
                        else
                        {
                            curMode = FSIMode.File; //we have fsimode file we gonna open it
                            using (FileStream fs = new FileStream(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName, FileMode.Open, FileAccess.Read))
                            //если это файл то делаем проверку на место в ряду папок и файлов, затем открываем и читаем  (history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName, FileMode.Open, FileAccess.Read
                            {
                                using (StreamReader sr = new StreamReader(fs)) //читает файлы that in using fs
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd()); //read file polnost'u

                                }
                            }

                        }
                        break;
                    case ConsoleKey.Backspace: //going back
                        if (curMode == FSIMode.DirectoryInfo) //current fsi mode for dir info
                        {
                            if (history.Count > 1) //убираем с истории слои чтобы вернуться в предыдущий то есть выйти это для папок в папках и тд history.Count > 1 pop deletes
                                history.Pop();
                        }
                        else
                        {
                            curMode = FSIMode.DirectoryInfo; //если это первый слой то просто выходим из программы
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case ConsoleKey.Escape: //going out prosto bool esc equals true 
                        esc = true;
                        break;
                    case ConsoleKey.Delete: //deleting files
                        if (curMode != FSIMode.DirectoryInfo || (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length) == 0)//cur mode should be equal to FSIMode.DirectoryInfo
                            // history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length) == 0 then deleting breaks
                            break;
                        index = history.Peek().SelectedIndex; //присваем индексу значение выбранного  history.Peek().SelectedIndex
                        int ind = index; //now ind equals to index
                        if (index < history.Peek().DirectoryContent.Length) //сравниваем с индексом расположения папки  index < history.Peek().DirectoryContent.Length
                            history.Peek().DirectoryContent[index].Delete(true); //если это папка то удаляем ее  history.Peek().DirectoryContent[index].Delete(true)
                        else
                            history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Delete(); //случай для файлов history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Delete()
                        int numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 2;
                        // numofcontent -2 because we have 2 writelines that cannot be divided  history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 2
                        history.Pop(); //deleting files
                        if (history.Count == 0) //first layes 0
                        {
                            Layer nl = new Layer
                            {
                                DirectoryContent = dir.GetDirectories(), //get papkis
                                FileContent = dir.GetFiles(), //get files
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind) //idk what is that sorry(  Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl); 
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex; //if history more than 0  index = history.Peek().SelectedIndex
                            DirectoryInfo di = history.Peek().DirectoryContent[index]; //di direcory equals to hist peek dircont arrays index history.Peek().DirectoryContent[index]
                            Layer nl = new Layer
                            {
                                DirectoryContent = di.GetDirectories(), //get dirs content
                                FileContent = di.GetFiles(), //get files content
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind) //idk what is that sorry(  Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    case ConsoleKey.F4: //renaming!
                        if (history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length == 0) //both contents length shouldnt be equal to zero so it breaks 
                            break;
                        index = history.Peek().SelectedIndex; //index equals hist peek selected index
                        string name, fullname; 
                        int selectedMode;
                        if (index < history.Peek().DirectoryContent.Length) //случай для папок  (index < history.Peek().DirectoryContent.Length)
                        {
                            name = history.Peek().DirectoryContent[index].Name; //name equals to name we already have  history.Peek().DirectoryContent[index].Name;
                            fullname = history.Peek().DirectoryContent[index].FullName; //same with fullname
                            selectedMode = 1; //choosing mode 1
                        }
                        else
                        {
                            name = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Name; //для файлов   history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Name
                            fullname = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName; //same as name
                            selectedMode = 2; //cause files mode is 2 
                        }
                        fullname = fullname.Remove(fullname.Length - name.Length); //remove fullname.Length - name.Length deletes all names
                        Console.WriteLine("Please enter the new name, to rename {0}:", name);// creating new name 
                        Console.WriteLine(fullname); //and fullname
                        string newname = Console.ReadLine(); //reading through console newname
                        while (newname.Length == 0 || pathExists(fullname + newname, selectedMode)) //even no new name of pathExists(fullname + newname, selectedMode)
                        {
                            Console.WriteLine("This directory was created, Enter the new one");
                            newname = Console.ReadLine(); //another try to  newname = Console.ReadLine() 
                        }
                        Console.WriteLine(selectedMode); // we have 2 modes for dirs and files 
                        if (selectedMode == 1) //if dirs  new DirectoryInfo(history.Peek().DirectoryContent[index].FullName).MoveTo(fullname + newname);
                        {
                            new DirectoryInfo(history.Peek().DirectoryContent[index].FullName).MoveTo(fullname + newname); //moving to full+name
                        }
                        else //mode 2 for files FileInfo(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName).MoveTo(fullname + newname);
                            
                        new FileInfo(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName).MoveTo(fullname + newname);
                        index = history.Peek().SelectedIndex;//new index equals to selected
                        ind = index; //ind equals to index
                        numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1; //history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1
                        history.Pop(); //deleting 
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