using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Actions
{
    class CustomCacheKeyFactory : ICacheKeyFactory
    {
        public string GetKey(ImageSource imageSource, object bindingContext)
        {
            if (imageSource == null)
                return null;

            // UriImageSource
            var uriImageSource = imageSource as UriImageSource;
            if (uriImageSource != null)
                return string.Format("{0}+myCustomUriSuffix", uriImageSource.Uri);

            // FileImageSource
            var fileImageSource = imageSource as FileImageSource;
            if (fileImageSource != null)
                return string.Format("{0}+myCustomFileSuffix", fileImageSource.File);

            // StreamImageSource
            var streamImageSource = imageSource as StreamImageSource;
            if (streamImageSource != null)
                return string.Format("{0}+myCustomStreamSuffix", streamImageSource.Stream.GetHashCode());

            throw new NotImplementedException("ImageSource type not supported");
        }
    }
}
