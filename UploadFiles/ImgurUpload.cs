using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UploadFiles
{
    // https://carry0987.github.io/Imgur-Upload/
    // https://github.com/Auo/ImgurSharp
    public class ImgurUpload
    {
        public string BaseUrl = "https://api.imgur.com";
        public string Endpoint = "https://api.imgur.com/3/image";

        public string ClientId { get; set; }

        public ImgurUpload(string clientId)
        {
            ClientId = clientId;
        }
        public async Task<string> PostFile(string pathFile)
        {
            var result = string.Empty;
            //string base64Image = PhotoStreamToBase64(File.OpenRead(pathFile));
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Client-ID {ClientId}");
            var formContent = new MultipartFormDataContent("update--" + DateTime.Now.ToString(CultureInfo.InvariantCulture))
            {
                { new StreamContent(File.OpenRead(pathFile)), "image" }
            };
            var res = await http.PostAsync(Endpoint, formContent);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultRes = JsonConvert.DeserializeObject<dynamic>(await res.Content.ReadAsStringAsync());
                result = resultRes.data.link;
            }
            return result;
        }

        private string PhotoStreamToBase64(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            byte[] result = memoryStream.ToArray();

            string base64img = System.Convert.ToBase64String(result);
            return base64img;
            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < base64img.Length; i += 32766)
            //{
            //    sb.Append(Uri.EscapeDataString(base64img.Substring(i, Math.Min(32766, base64img.Length - i))));
            //}

            //return sb.ToString();
        }
    }
}
