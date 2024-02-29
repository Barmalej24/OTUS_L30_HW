using System;
using System.Net;
using System.Net.Http;

namespace OTUS_L30_HW
{
    public class ImageDownloader
    {
        public event Action? ImageStarted;
        public event Action? ImageCompleted;
        public void Download(string remoteUri)
        {
            string fileName = "bigimage.jpg";
            using var myWebClient = new WebClient();

            if (this.ImageStarted != null)
            {
                ImageStarted.Invoke();
            }

            myWebClient.DownloadFile(remoteUri, fileName);

            if (this.ImageStarted != null)
            {
                ImageStarted.Invoke();
            }
        }

        public async Task DownloadAsync(string remoteUri, CancellationToken cancellationToken)
        {
            var random = new Random();
            var i = random.Next();
            string fileName = $"bigimage{i}.jpg";
            using var httpClient = new HttpClient();

            if (this.ImageStarted != null)
            {
                ImageStarted!.Invoke();
            }

            using (var response = await httpClient.GetAsync(remoteUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                using var fileStream = File.Create(fileName);
                using var httpStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                await httpStream.CopyToAsync(fileStream, cancellationToken);
            }

            if (this.ImageStarted != null)
            {
                ImageCompleted!.Invoke();
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        public void Subscribe()
        {
            this.ImageStarted += () =>
            {
                Console.WriteLine("Скачивание файла началось");
            };
            this.ImageCompleted += () =>
            {
                Console.WriteLine("Скачивание файла закончилось");
            };
        }
    }
}
