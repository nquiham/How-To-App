using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace HowTo
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}