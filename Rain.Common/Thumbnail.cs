using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Rain.Common
{
  public class Thumbnail
  {
    private Image srcImage;
    private string srcFileName;

    public bool SetImage(string FileName)
    {
      this.srcFileName = Utils.GetMapPath(FileName);
      try
      {
        this.srcImage = Image.FromFile(this.srcFileName);
      }
      catch
      {
        return false;
      }
      return true;
    }

    public bool ThumbnailCallback()
    {
      return false;
    }

    public Image GetImage(int Width, int Height)
    {
      Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
      return this.srcImage.GetThumbnailImage(Width, Height, callback, IntPtr.Zero);
    }

    public void SaveThumbnailImage(int Width, int Height)
    {
      switch (Path.GetExtension(this.srcFileName).ToLower())
      {
        case ".png":
          this.SaveImage(Width, Height, ImageFormat.Png);
          break;
        case ".gif":
          this.SaveImage(Width, Height, ImageFormat.Gif);
          break;
        default:
          this.SaveImage(Width, Height, ImageFormat.Jpeg);
          break;
      }
    }

    public void SaveImage(int Width, int Height, ImageFormat imgformat)
    {
      if ((imgformat == ImageFormat.Gif || this.srcImage.Width <= Width) && this.srcImage.Height <= Height)
        return;
      Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
      Image thumbnailImage = this.srcImage.GetThumbnailImage(Width, Height, callback, IntPtr.Zero);
      this.srcImage.Dispose();
      thumbnailImage.Save(this.srcFileName, imgformat);
      thumbnailImage.Dispose();
    }

    private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
    {
      EncoderParameters encoderParams = new EncoderParameters(1);
      encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
      image.Save(savePath, ici, encoderParams);
      encoderParams.Dispose();
    }

    private static ImageCodecInfo GetCodecInfo(string mimeType)
    {
      foreach (ImageCodecInfo imageEncoder in ImageCodecInfo.GetImageEncoders())
      {
        if (imageEncoder.MimeType == mimeType)
          return imageEncoder;
      }
      return (ImageCodecInfo) null;
    }

    private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
    {
      if (maxWidth <= 0)
        maxWidth = width;
      if (maxHeight <= 0)
        maxHeight = height;
      Decimal num1 = (Decimal) maxWidth;
      Decimal num2 = (Decimal) maxHeight;
      Decimal num3 = num1 / num2;
      Decimal num4 = (Decimal) width;
      Decimal num5 = (Decimal) height;
      int width1;
      int height1;
      if (num4 > num1 || num5 > num2)
      {
        if (num4 / num5 > num3)
        {
          Decimal num6 = num4 / num1;
          width1 = Convert.ToInt32(num4 / num6);
          height1 = Convert.ToInt32(num5 / num6);
        }
        else
        {
          Decimal num6 = num5 / num2;
          width1 = Convert.ToInt32(num4 / num6);
          height1 = Convert.ToInt32(num5 / num6);
        }
      }
      else
      {
        width1 = width;
        height1 = height;
      }
      return new Size(width1, height1);
    }

    public static ImageFormat GetFormat(string name)
    {
      switch (name.Substring(name.LastIndexOf(".") + 1).ToLower())
      {
        case "jpg":
        case "jpeg":
          return ImageFormat.Jpeg;
        case "bmp":
          return ImageFormat.Bmp;
        case "png":
          return ImageFormat.Png;
        case "gif":
          return ImageFormat.Gif;
        default:
          return ImageFormat.Jpeg;
      }
    }

    public static void MakeSquareImage(Image image, string newFileName, int newSize)
    {
      int num = 0;
      int width = image.Width;
      int height = image.Height;
      num = width <= height ? width : height;
      Bitmap bitmap = new Bitmap(newSize, newSize);
      try
      {
        Graphics graphics = Graphics.FromImage((Image) bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.Clear(Color.Transparent);
        if (width < height)
          graphics.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle(0, (height - width) / 2, width, width), GraphicsUnit.Pixel);
        else
          graphics.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle((width - height) / 2, 0, height, height), GraphicsUnit.Pixel);
        Thumbnail.SaveImage((Image) bitmap, newFileName, Thumbnail.GetCodecInfo("image/" + Thumbnail.GetFormat(newFileName).ToString().ToLower()));
      }
      finally
      {
        image.Dispose();
        bitmap.Dispose();
      }
    }

    public static void MakeSquareImage(string fileName, string newFileName, int newSize)
    {
      Thumbnail.MakeSquareImage(Image.FromFile(fileName), newFileName, newSize);
    }

    public static void MakeRemoteSquareImage(string url, string newFileName, int newSize)
    {
      Stream remoteImage = Thumbnail.GetRemoteImage(url);
      if (remoteImage == null)
        return;
      Image image = Image.FromStream(remoteImage);
      remoteImage.Close();
      Thumbnail.MakeSquareImage(image, newFileName, newSize);
    }

    public static void MakeThumbnailImage(
      Image original,
      string newFileName,
      int maxWidth,
      int maxHeight)
    {
      Size newSize = Thumbnail.ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
      using (Image image = (Image) new Bitmap(original, newSize))
      {
        try
        {
          image.Save(newFileName, original.RawFormat);
        }
        finally
        {
          original.Dispose();
        }
      }
    }

    public static void MakeThumbnailImage(
      string fileName,
      string newFileName,
      int maxWidth,
      int maxHeight)
    {
      Thumbnail.MakeThumbnailImage(Image.FromStream((Stream) new MemoryStream(System.IO.File.ReadAllBytes(fileName))), newFileName, maxWidth, maxHeight);
    }

    public static void MakeThumbnailImage(
      string fileName,
      string newFileName,
      int width,
      int height,
      string mode)
    {
      Image image = Image.FromFile(fileName);
      int width1 = width;
      int height1 = height;
      int x = 0;
      int y = 0;
      int width2 = image.Width;
      int height2 = image.Height;
      switch (mode)
      {
        case "HW":
          if ((double) image.Width / (double) image.Height > (double) width1 / (double) height1)
          {
            width2 = image.Width;
            height2 = image.Width * height / width1;
            x = 0;
            y = (image.Height - height2) / 2;
            break;
          }
          height2 = image.Height;
          width2 = image.Height * width1 / height1;
          y = 0;
          x = (image.Width - width2) / 2;
          break;
        case "W":
          height1 = image.Height * width / image.Width;
          break;
        case "H":
          width1 = image.Width * height / image.Height;
          break;
        case "Cut":
          if ((double) image.Width / (double) image.Height > (double) width1 / (double) height1)
          {
            height2 = image.Height;
            width2 = image.Height * width1 / height1;
            y = 0;
            x = (image.Width - width2) / 2;
            break;
          }
          width2 = image.Width;
          height2 = image.Width * height / width1;
          x = 0;
          y = (image.Height - height2) / 2;
          break;
      }
      Bitmap bitmap = new Bitmap(width1, height1);
      try
      {
        Graphics graphics = Graphics.FromImage((Image) bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.Clear(Color.White);
        graphics.DrawImage(image, new Rectangle(0, 0, width1, height1), new Rectangle(x, y, width2, height2), GraphicsUnit.Pixel);
        Thumbnail.SaveImage((Image) bitmap, newFileName, Thumbnail.GetCodecInfo("image/" + Thumbnail.GetFormat(newFileName).ToString().ToLower()));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        image.Dispose();
        bitmap.Dispose();
      }
    }

    public static bool MakeThumbnailImage(
      string fileName,
      string newFileName,
      int maxWidth,
      int maxHeight,
      int cropWidth,
      int cropHeight,
      int X,
      int Y)
    {
      Image image = Image.FromStream((Stream) new MemoryStream(System.IO.File.ReadAllBytes(fileName)));
      Bitmap bitmap = new Bitmap(cropWidth, cropHeight);
      try
      {
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          graphics.Clear(Color.Transparent);
          graphics.DrawImage(image, new Rectangle(0, 0, cropWidth, cropHeight), X, Y, cropWidth, cropHeight, GraphicsUnit.Pixel);
          Thumbnail.SaveImage((Image) new Bitmap((Image) bitmap, maxWidth, maxHeight), newFileName, Thumbnail.GetCodecInfo("image/" + Thumbnail.GetFormat(newFileName).ToString().ToLower()));
          return true;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        image.Dispose();
        bitmap.Dispose();
      }
    }

    public static void MakeRemoteThumbnailImage(
      string url,
      string newFileName,
      int maxWidth,
      int maxHeight)
    {
      Stream remoteImage = Thumbnail.GetRemoteImage(url);
      if (remoteImage == null)
        return;
      Image original = Image.FromStream(remoteImage);
      remoteImage.Close();
      Thumbnail.MakeThumbnailImage(original, newFileName, maxWidth, maxHeight);
    }

    private static Stream GetRemoteImage(string url)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
      httpWebRequest.Method = "GET";
      httpWebRequest.ContentLength = 0L;
      httpWebRequest.Timeout = 20000;
      try
      {
        return httpWebRequest.GetResponse().GetResponseStream();
      }
      catch
      {
        return (Stream) null;
      }
    }
  }
}
