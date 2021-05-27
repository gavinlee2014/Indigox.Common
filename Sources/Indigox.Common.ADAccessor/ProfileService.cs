using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;

namespace Indigox.Common.ADAccessor
{
    public class ProfileService
    {
        private Image profile;
        private const int width = 96;
        private const int height = 96;

        public ProfileService(string url)
        {
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData(url);
            MemoryStream stream = new MemoryStream(buffer);
            profile = Image.FromStream(stream);
        }

        public byte[] GetThumbnailImageForAD()
        {
            MemoryStream stream = GetThumbnailImage(profile, width, height);
            return stream.ToArray();
        }

        public MemoryStream GetThumbnailImage(Image image, int width, int height)
        {
            float TEMP_WIDTH = 0.0f;
            float TEMP_HEIGHT = 0.0f;

            if (!float.TryParse(width.ToString(), out TEMP_WIDTH))
            {
                TEMP_WIDTH = 10.0f;
            }
            if (!float.TryParse(height.ToString(), out TEMP_HEIGHT))
            {
                TEMP_HEIGHT = 10.0f;
            }

            float ASPECT_RATIO = (image.Width > image.Height) ? TEMP_WIDTH / image.Width : TEMP_HEIGHT / image.Height;
            int THUMB_WIDTH = (int)(image.Width * ASPECT_RATIO);
            int THUMB_HEIGHT = (int)(image.Height * ASPECT_RATIO);

            Bitmap bmp = new Bitmap(THUMB_WIDTH, THUMB_HEIGHT);
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.CompositingQuality = CompositingQuality.HighQuality;
            gr.InterpolationMode = InterpolationMode.High;

            Rectangle rectDestination = new Rectangle(0, 0, THUMB_WIDTH, THUMB_HEIGHT);
            gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            ImageCodecInfo jgpEncoder = GetEncoder("JPEG");

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            MemoryStream imageStream = new MemoryStream();
            bmp.Save(imageStream, jgpEncoder, myEncoderParameters);
            bmp.Dispose();
            gr.Dispose();

            return imageStream;
        }

        private ImageCodecInfo GetEncoder(string f)
        {
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals(f))
                {
                    return arrayICI[x];
                }
            }
            return null;
        }
    }
}
