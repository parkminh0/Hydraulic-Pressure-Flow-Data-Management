using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace WooSungEngineering
{
    public class CommonLogic
    {
        public delegate void LoadDataMethod();

        /// <summary>
        /// 이미지 => byte
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public byte[] ImageToByteArray(Image image)
        {
            if (image == null)
                return null;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        /// <summary>
        /// byte => 이미지
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public Image ByteArrayToImage(byte[] array)
        {
            if (array == null)
                return null;

            MemoryStream ms = new MemoryStream(array);
            Image image = Image.FromStream(ms);
            return image;
        }
    }
}
