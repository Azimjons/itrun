using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("a) Показать список продуктов");
        Console.WriteLine("b) Добавить новый продукт");
        Console.WriteLine("c) Продать продукт");

        string choice = Console.ReadLine();
        string filename = "products.txt";

        switch (choice)
        {
            case "a":
                ShowProducts(filename);
                break;
            case "b":
                Console.WriteLine("Введите имя продукта:");
                string productName = Console.ReadLine();
                Console.WriteLine("Введите количество:");
                int productQuantity = int.Parse(Console.ReadLine());
                AddProduct(filename, productName, productQuantity);
                break;
            case "c":
                Console.WriteLine("Введите имя продукта:");
                string soldProductName = Console.ReadLine();
                Console.WriteLine("Введите количество, проданное продукта:");
                int soldProductQuantity = int.Parse(Console.ReadLine());
                SellProduct(filename, soldProductName, soldProductQuantity);
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }

        Console.ReadLine();
    }

    static void ShowProducts(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            Console.WriteLine("Список продуктов:");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void AddProduct(string filename, string productName, int productQuantity)
    {
        if (File.Exists(filename))
        {
            bool productExists = false;
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0].Equals(productName))
                {
                    int quantity = int.Parse(parts[1]) + productQuantity;
                    lines[i] = productName + "," + quantity;
                    productExists = true;
                }
            }

            if (!productExists)
            {
                string newLine = productName + "," + productQuantity;
                File.AppendAllText(filename, newLine + Environment.NewLine);
            }
            else
            {
                File.WriteAllLines(filename, lines);
            }

            Console.WriteLine("Продукт сохранен в файл.");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void SellProduct(string filename, string soldProductName, int soldProductQuantity)
    {
        if (File.Exists(filename))
        {
            bool productExists = false;
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0].Equals(soldProductName))
                {
                    int quantity = int.Parse(parts[1]) - soldProductQuantity;
                    if (quantity < 0)
                    {
                        Console.WriteLine("Недостаточно продукта на складе.");
                    }
                    else
                    {
                        lines[i] = soldProductName + "," + quantity;
                        productExists = true;
                    }
                }
            }

            if (!productExists)
            {
                Console.WriteLine("Продукт не найден.");
            }
            else
            {
                File.WriteAllLines(filename, lines);
                Console.WriteLine("Продукт продан.");
            }
        }
    }
}