using System;
using System.Text;
using System.Linq;
using System.IO;

string path = @"text.txt"; //название файла
string path2 = @"\...\...\...\...\...\...\...\...\...\text.txt"; //относ путь
string pathDir = @"C:\Users\Ксения\source\repos\Lab_8\Lab_8\bin\Debug\net6.0"; //абс путь
FileInfo Information = new FileInfo(path); // вызов функции инфы о файле
Console.WriteLine(Information.FullName); //абс путь файла через функцию
Console.WriteLine(Information.Name); // имя файла
Console.WriteLine($"{Information.Length} байт"); 
Console.WriteLine();
Console.WriteLine(path2); // относ путь



Console.WriteLine(Path.GetFileNameWithoutExtension(path)); //абс путь
Console.WriteLine(Environment.CurrentDirectory); //относ путь

Console.WriteLine();

if (File.Exists(path))
{
    string Text = File.ReadAllText(path);

    var Words = Text.Split(new string [] { " ", "\n", "\r\n","\t" }, StringSplitOptions.RemoveEmptyEntries); // разбиваем на слова
    

    Console.WriteLine($"Количество слов:{Words.Length.ToString()}");

    bool Flag1 = false;
    bool Flag2 = false;
    bool Flag3 = false;

    for (int i = 0; i < Text.Length; i++)
    {
        if (Text[i] >= 'a' && Text[i] <= 'z' || Text[i] >= 'A' && Text[i] <= 'Z') //проверка на латину
        {
            Flag1 = true;
        }

        if (Text[i] >= 'а' && Text[i] <= 'я' || Text[i] >= 'A' && Text[i] <= 'Z') // проверка на кириллицу
        {
            Flag2 = true;
        }

        if (Char.IsDigit(Text[i])) // проверка на цифры
        {
            Flag3 = true;
        }
    }

    if (Flag1)
    {
        Console.WriteLine("В тексте есть латиница");
    }

    if (Flag2)
    {
        Console.WriteLine("В тексте есть кириллица");
    }

    if (Flag3)
    {
        Console.WriteLine("В тексте есть цифры");
    }
    var stat = Words.Distinct()
              .ToDictionary(word => word, word => Words.Count(x => x == word))
              .OrderByDescending(x => x.Value);
    Console.WriteLine();

    StringBuilder sb = new StringBuilder();
    foreach (var item in stat)
    {
        sb.AppendLine($"{item.Key} {item.Value}");
    }

    StreamReader sr = new StreamReader("text.txt");

    int k = 0;
    while ((Text= sr.ReadLine()) != null) // читаем по дной линии пока не дойдем до конца файла
    {
        k++;
    }
    Console.WriteLine("Количество строк:" + k.ToString()); //кол-во строк
    //сохранение
    sb.AppendLine($"{Information.Name}\n{Information.FullName}\n{Information.CreationTime}\n{Information.Length} байт\nКоличество строк: {k.ToString()}\nКоличество слов: {Words.Length.ToString()}");
    File.WriteAllText("Result_1.txt", sb.ToString());
}
else
{
    Console.WriteLine("Данного файла не существует");
}




