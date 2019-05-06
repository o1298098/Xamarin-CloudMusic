using CloudMusic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusic.Services
{
    public interface IPicturePicker
    {
        Task<string> GetImageUriAsync();
        Task<List<ImageModel>> GetAllImageUriAsync();
    }
}
