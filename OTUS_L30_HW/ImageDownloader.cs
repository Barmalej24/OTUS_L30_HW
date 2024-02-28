using System.Net;

namespace OTUS_L30_HW
{
    public class ImageDownloader
    {
        public event Action? ImageStarted;
        public event Action? ImageCompleted;
        public void Download(string remoteUri)
        {
            string fileName = "bigimage.jpg";
            var myWebClient = new WebClient();           

            this.ImageStarted!.Invoke();

            myWebClient.DownloadFile(remoteUri, fileName);
            
            this.ImageCompleted!.Invoke();   
        }

        public async Task DownloadAsync(string remoteUri)
        {
            var random = new Random();
            var i = random.Next();

            string fileName = $"bigimage{i}.jpg";
            var myWebClient = new WebClient();

            this.ImageStarted!.Invoke();

            await myWebClient.DownloadFileTaskAsync(remoteUri, fileName);

            this.ImageCompleted!.Invoke();
        }

        public void Subscribe(ImageDownloader image)
        {
            image.ImageStarted += () =>
            {
                Console.WriteLine("Скачивание файла началось");
            };
            image.ImageCompleted += () =>
            {
                Console.WriteLine("Скачивание файла закончилось");
            };
        }
    }
}
