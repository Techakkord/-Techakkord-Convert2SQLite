using System;
using System.Diagnostics;
using System.IO;

namespace C5
{
    class Converter
    {
        static void Main(string[] args)
        {
            //Консоль принимает два аргумента (названия конвертируемого и итогового файлов соответственно)
            //Пример запуска конвертации в консоли: C:\Users\jonak\source\repos\C5\C5\bin\Debug\netcoreapp3.1> .\C5.exe IsueDic.sdf 1.sqlite
            string source = args[0];
            string target = args[1];
            //Отлавливаем некоторые возможные ошибки 
            if (!(File.Exists(source)))
            {
                Console.Write("Нет исходного файла");
                Environment.Exit(0);
            }
            if (File.Exists(target))
            {
                Console.Write("Целевой файл уже существует");
                Environment.Exit(0);
            }
            if (File.Exists("tmp.sql"))
            {
                File.Delete("tmp.sql");
                Console.Write("Контроль зачистки");
            }
            if (!(File.Exists("ExportSqlCE40.exe")))
            {
                Console.Write("В директории нет необходимых рабочих файлов");
                Environment.Exit(0);
            }
            if (!(File.Exists("QuickGraph.Data.dll")))
            {
                Console.Write("В директории нет необходимых рабочих файлов");
                Environment.Exit(0);
            }
            if (!(File.Exists("QuickGraph.dll")))
            {
                Console.Write("В директории нет необходимых рабочих файлов");
                Environment.Exit(0);
            }
            if (!(File.Exists("QuickGraph.Graphviz.dll")))
            {
                Console.Write("В директории нет необходимых рабочих файлов");
                Environment.Exit(0);
            }
            if (!(File.Exists("sqlite3.exe")))
            {
                Console.Write("В директории нет необходимых рабочих файлов");
                Environment.Exit(0);
            }
            //вызов сторонней программы, которая создаёт dump базы данных SQL Compact в формате *.sql
            var prc = new ProcessStartInfo("ExportSQLCE40.exe");
            prc.ArgumentList.Add("Data Source =" + source + ";");
            prc.ArgumentList.Add("tmp.sql");
            prc.ArgumentList.Add("sqlite");
            var i = Process.Start(prc);
            i.WaitForExit();
            // вызов сторонней программы, которая из dump'а  формирует базу данных sqlite
            var prc2 = new ProcessStartInfo("sqlite3.exe");
            prc2.ArgumentList.Add(target);
            prc2.ArgumentList.Add(".read tmp.sql");
            var i2 = Process.Start(prc2);
            i2.WaitForExit();

        }

    }

}