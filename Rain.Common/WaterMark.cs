using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Rain.Common
{
  public class WaterMark
  {
    public static void AddImageSignPic(
      string imgPath,
      string filename,
      string watermarkFilename,
      int watermarkStatus,
      int quality,
      int watermarkTransparency)
    {
      if (!File.Exists(Utils.GetMapPath(imgPath)))
        return;
      Image image1 = Image.FromStream((Stream) new MemoryStream(File.ReadAllBytes(Utils.GetMapPath(imgPath))));
      filename = Utils.GetMapPath(filename);
      if (!watermarkFilename.StartsWith("/"))
        watermarkFilename = "/" + watermarkFilename;
      watermarkFilename = Utils.GetMapPath(watermarkFilename);
      if (!File.Exists(watermarkFilename))
        return;
      Graphics graphics = Graphics.FromImage(image1);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      Image image2 = (Image) new Bitmap(watermarkFilename);
      if (image2.Height >= image1.Height || image2.Width >= image1.Width)
        return;
      ImageAttributes imageAttr = new ImageAttributes();
      ColorMap[] map = new ColorMap[1]
      {
        new ColorMap()
        {
          OldColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue, 0),
          NewColor = Color.FromArgb(0, 0, 0, 0)
        }
      };
      imageAttr.SetRemapTable(map, ColorAdjustType.Bitmap);
      float num = 0.5f;
      if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
        num = (float) watermarkTransparency / 10f;
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, num, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
      });
      imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      int x = 0;
      int y = 0;
      switch (watermarkStatus)
      {
        case 1:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 2:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 3:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 4:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 5:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 6:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 7:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
        case 8:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
        case 9:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
      }
      graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageCodecInfo encoder = (ImageCodecInfo) null;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        if (imageCodecInfo.MimeType.IndexOf("jpeg") > -1)
          encoder = imageCodecInfo;
      }
      EncoderParameters encoderParams = new EncoderParameters();
      long[] numArray = new long[1];
      if (quality < 0 || quality > 100)
        quality = 80;
      numArray[0] = (long) quality;
      EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, numArray);
      encoderParams.Param[0] = encoderParameter;
      if (encoder != null)
        image1.Save(filename, encoder, encoderParams);
      else
        image1.Save(filename);
      graphics.Dispose();
      image1.Dispose();
      image2.Dispose();
      imageAttr.Dispose();
    }

    public static void AddImageSignText(
      string imgPath,
      string filename,
      string watermarkText,
      int watermarkStatus,
      int quality,
      string fontname,
      int fontsize)
    {
      Image image = Image.FromStream((Stream) new MemoryStream(File.ReadAllBytes(Utils.GetMapPath(imgPath))));
      filename = Utils.GetMapPath(filename);
      Graphics graphics = Graphics.FromImage(image);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      Font font = new Font(fontname, (float) fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
      SizeF sizeF = graphics.MeasureString(watermarkText, font);
      float x = 0.0f;
      float y = 0.0f;
      switch (watermarkStatus)
      {
        case 1:
          x = (float) image.Width * 0.01f;
          y = (float) image.Height * 0.01f;
          break;
        case 2:
          x = (float) ((double) image.Width * 0.5 - (double) sizeF.Width / 2.0);
          y = (float) image.Height * 0.01f;
          break;
        case 3:
          x = (float) image.Width * 0.99f - sizeF.Width;
          y = (float) image.Height * 0.01f;
          break;
        case 4:
          x = (float) image.Width * 0.01f;
          y = (float) ((double) image.Height * 0.5 - (double) sizeF.Height / 2.0);
          break;
        case 5:
          x = (float) ((double) image.Width * 0.5 - (double) sizeF.Width / 2.0);
          y = (float) ((double) image.Height * 0.5 - (double) sizeF.Height / 2.0);
          break;
        case 6:
          x = (float) image.Width * 0.99f - sizeF.Width;
          y = (float) ((double) image.Height * 0.5 - (double) sizeF.Height / 2.0);
          break;
        case 7:
          x = (float) image.Width * 0.01f;
          y = (float) image.Height * 0.99f - sizeF.Height;
          break;
        case 8:
          x = (float) ((double) image.Width * 0.5 - (double) sizeF.Width / 2.0);
          y = (float) image.Height * 0.99f - sizeF.Height;
          break;
        case 9:
          x = (float) image.Width * 0.99f - sizeF.Width;
          y = (float) image.Height * 0.99f - sizeF.Height;
          break;
      }
      graphics.DrawString(watermarkText, font, (Brush) new SolidBrush(Color.White), x + 1f, y + 1f);
      graphics.DrawString(watermarkText, font, (Brush) new SolidBrush(Color.Black), x, y);
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageCodecInfo encoder = (ImageCodecInfo) null;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        if (imageCodecInfo.MimeType.IndexOf("jpeg") > -1)
          encoder = imageCodecInfo;
      }
      EncoderParameters encoderParams = new EncoderParameters();
      long[] numArray = new long[1];
      if (quality < 0 || quality > 100)
        quality = 80;
      numArray[0] = (long) quality;
      EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, numArray);
      encoderParams.Param[0] = encoderParameter;
      if (encoder != null)
        image.Save(filename, encoder, encoderParams);
      else
        image.Save(filename);
      graphics.Dispose();
      image.Dispose();
    }
  }
}
