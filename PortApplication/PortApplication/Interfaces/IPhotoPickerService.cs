using System.IO;
using System.Threading.Tasks;

namespace PortApplication.Interfaces
{
    public interface IPhotoPickerService
    {
        /// <summary>
        /// Used as dependency service to implement photo picker
        /// </summary>
        /// <returns></returns>
        Task<Stream> GetImageStreamAsync();
    }
}
