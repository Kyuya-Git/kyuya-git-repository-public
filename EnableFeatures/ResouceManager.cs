using System.Reflection;
using System.Drawing;

namespace EnableFeatures
{
    public class ResouceManager
    {
        const int ImgCount = 4;
        public static Bitmap[] GetImages()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            return new Bitmap[ImgCount]
            {
                new Bitmap(myAssembly.GetManifestResourceStream("EnableFeatures.Resource.OK.bmp")),
                new Bitmap(myAssembly.GetManifestResourceStream("EnableFeatures.Resource.ERROR.bmp")),
                new Bitmap(myAssembly.GetManifestResourceStream("EnableFeatures.Resource.ATTENTION.bmp")),
                new Bitmap(myAssembly.GetManifestResourceStream("EnableFeatures.Resource.Logo_DX.png"))
            };
        }
    }

    public enum ImageID
    {
        STATUS_OK = 0,
        STATUS_ERROR,
        STATUS_ATTENTION,
        LOGO_GDX
    }
}
