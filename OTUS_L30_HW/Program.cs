using System.Net;

namespace OTUS_L30_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string remoteUri = "https://bfoto.ru/news/wp-content/uploads/2016/05/foto-flowers-bfoto-ru_2015.jpg";
            
            Console.WriteLine("Выбери вариан исполнения");
            Console.WriteLine("1 - Обычный метод (Пункты 1 - 3)");
            Console.WriteLine("2 - Асинхронный метод (Пункты 4 - 5)");
            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Method13(remoteUri);
                    break;
                case "2":
                    Method45(remoteUri);
                    break;
            } 
        }

        private static void Method45(string remoteUri)
        {
            var tasks = new List<Task>();
            for (int i = 1; i <= 10; i++)
            {
                var imageDownloader = new ImageDownloader();
                imageDownloader.Subscribe(imageDownloader);
                tasks.Add(imageDownloader.DownloadAsync(remoteUri));
            }
            Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
            var key = Console.ReadKey();
            do
            {
                if (key.Key == ConsoleKey.A)
                {
                    return;
                }
                else
                {
                    Console.WriteLine();
                    foreach (var task in tasks)
                    {
                        Console.WriteLine($"Статус заргузки изображения в задание {task.Id} :: {task.IsCompleted}");
                    }
                    key = Console.ReadKey();
                }
            }
            while (true);
        }

        private static void Method13(string remoteUri)
        {
            var imageDownloader = new ImageDownloader();
            imageDownloader.Subscribe(imageDownloader);
            imageDownloader.Download(remoteUri);
            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadKey();
        }

       
    }
}
