using BlazorShared.Models;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Services
{
    public class FileService
    {
        private readonly HttpService _httpService;

        public FileService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> ReadPicture(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                return null;
            }

            return (await _httpService.HttpGetAsync<FileItem>($"files/{pictureName}")).DataBase64;
        }        
    }
}
