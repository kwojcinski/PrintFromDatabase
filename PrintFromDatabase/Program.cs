using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PrintFromDatabase
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static int tableWidth = 100;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            PrintCountries();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void PrintCountries()
        {
            var playerDAL = new PlayerDAL(_iconfiguration);

            var list = playerDAL.GetList();
            Console.Clear();
            PrintLine();
            PrintRow("Imię", "Nazwisko", "Kraj", "Data urodzenia", "Wzrost (cm)", "Waga (kg)");
            PrintLine();
            list.ForEach(item =>
            {
                PrintRow(item.imie, item.nazwisko, item.kraj, item.data_ur.ToShortDateString(), item.wzrost.ToString(), item.waga.ToString());
            });
            PrintLine();
            Console.ReadLine();
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}

