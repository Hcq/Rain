// Decompiled with JetBrains decompiler
// Type: Rain.Web.UI.UpLoad
// Assembly: Rain.Web.UI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3E78344A-857F-445F-804E-AF1B978FD5C7
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.UI.dll

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Web;
using Rain.Common;

namespace Rain.Web.UI
{
  public class UpLoad
  {
    private Rain.Model.siteconfig siteConfig;

    public UpLoad()
    {
      this.siteConfig = new Rain.BLL.siteconfig().loadConfig();
    }

    public bool cropSaveAs(
      string fileName,
      string newFileName,
      int maxWidth,
      int maxHeight,
      int cropWidth,
      int cropHeight,
      int X,
      int Y)
    {
      if (!this.IsImage(Utils.GetFileExt(fileName)))
        return false;
      string mapPath = Utils.GetMapPath(newFileName.Substring(0, newFileName.LastIndexOf("/") + 1));
      if (!Directory.Exists(mapPath))
        Directory.CreateDirectory(mapPath);
      try
      {
        return Thumbnail.MakeThumbnailImage(Utils.GetMapPath(fileName), Utils.GetMapPath(newFileName), 180, 180, cropWidth, cropHeight, X, Y);
      }
      catch
      {
        return false;
      }
    }

    public string fileSaveAs(HttpPostedFile postedFile, bool isThumbnail, bool isWater)
    {
      try
      {
        string fileExt = Utils.GetFileExt(postedFile.FileName);
        int contentLength = postedFile.ContentLength;
        string str1 = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);
        string str2 = Utils.GetRamCode() + "." + fileExt;
        string str3 = "thumb_" + str2;
        string upLoadPath = this.GetUpLoadPath();
        string mapPath = Utils.GetMapPath(upLoadPath);
        string str4 = upLoadPath + str2;
        string str5 = upLoadPath + str3;
        if (!this.CheckFileExt(fileExt))
          return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
        if (!this.CheckFileSize(fileExt, contentLength))
          return "{\"status\": 0, \"msg\": \"文件超过限制的大小！\"}";
        if (!Directory.Exists(mapPath))
          Directory.CreateDirectory(mapPath);
        postedFile.SaveAs(mapPath + str2);
        if (this.IsImage(fileExt) && (this.siteConfig.imgmaxheight > 0 || this.siteConfig.imgmaxwidth > 0))
          Thumbnail.MakeThumbnailImage(mapPath + str2, mapPath + str2, this.siteConfig.imgmaxwidth, this.siteConfig.imgmaxheight);
        if (this.IsImage(fileExt) && isThumbnail && this.siteConfig.thumbnailwidth > 0 && this.siteConfig.thumbnailheight > 0)
          Thumbnail.MakeThumbnailImage(mapPath + str2, mapPath + str3, this.siteConfig.thumbnailwidth, this.siteConfig.thumbnailheight, "Cut");
        else
          str5 = str4;
        if (this.IsWaterMark(fileExt) && isWater)
        {
          switch (this.siteConfig.watermarktype)
          {
            case 1:
              WaterMark.AddImageSignText(str4, str4, this.siteConfig.watermarktext, this.siteConfig.watermarkposition, this.siteConfig.watermarkimgquality, this.siteConfig.watermarkfont, this.siteConfig.watermarkfontsize);
              break;
            case 2:
              WaterMark.AddImageSignPic(str4, str4, this.siteConfig.watermarkpic, this.siteConfig.watermarkposition, this.siteConfig.watermarkimgquality, this.siteConfig.watermarktransparency);
              break;
          }
        }
        return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \"" + str1 + "\", \"path\": \"" + str4 + "\", \"thumb\": \"" + str5 + "\", \"size\": " + (object) contentLength + ", \"ext\": \"" + fileExt + "\"}";
      }
      catch
      {
        return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
      }
    }

    public string remoteSaveAs(string fileUri)
    {
      WebClient webClient = new WebClient();
      string empty = string.Empty;
      string _fileExt = fileUri.LastIndexOf(".") != -1 ? Utils.GetFileExt(fileUri) : "gif";
      string str1 = Utils.GetRamCode() + "." + _fileExt;
      string upLoadPath = this.GetUpLoadPath();
      string mapPath = Utils.GetMapPath(upLoadPath);
      string str2 = upLoadPath + str1;
      if (!Directory.Exists(mapPath))
        Directory.CreateDirectory(mapPath);
      try
      {
        webClient.DownloadFile(fileUri, mapPath + str1);
        if (this.IsWaterMark(_fileExt))
        {
          switch (this.siteConfig.watermarktype)
          {
            case 1:
              WaterMark.AddImageSignText(str2, str2, this.siteConfig.watermarktext, this.siteConfig.watermarkposition, this.siteConfig.watermarkimgquality, this.siteConfig.watermarkfont, this.siteConfig.watermarkfontsize);
              break;
            case 2:
              WaterMark.AddImageSignPic(str2, str2, this.siteConfig.watermarkpic, this.siteConfig.watermarkposition, this.siteConfig.watermarkimgquality, this.siteConfig.watermarktransparency);
              break;
          }
        }
      }
      catch
      {
        return string.Empty;
      }
      webClient.Dispose();
      return str2;
    }

    private string GetUpLoadPath()
    {
      string str1 = this.siteConfig.webpath + this.siteConfig.filepath + "/";
      string str2;
      if (this.siteConfig.filesave == 1)
      {
        str2 = str1 + DateTime.Now.ToString("yyyyMMdd");
      }
      else
      {
        string str3 = str1;
        DateTime now = DateTime.Now;
        string str4 = now.ToString("yyyyMM");
        now = DateTime.Now;
        string str5 = now.ToString("dd");
        str2 = str3 + str4 + "/" + str5;
      }
      return str2 + "/";
    }

    private bool IsWaterMark(string _fileExt)
    {
      if (this.siteConfig.watermarktype > 0)
      {
        if (new ArrayList()
        {
          (object) "bmp",
          (object) "jpeg",
          (object) "jpg",
          (object) "png"
        }.Contains((object) _fileExt.ToLower()))
          return true;
      }
      return false;
    }

    private bool IsImage(string _fileExt)
    {
      return new ArrayList()
      {
        (object) "bmp",
        (object) "jpeg",
        (object) "jpg",
        (object) "gif",
        (object) "png"
      }.Contains((object) _fileExt.ToLower());
    }

    private bool CheckFileExt(string _fileExt)
    {
      string[] strArray = new string[10]
      {
        "asp",
        "aspx",
        "ashx",
        "asa",
        "asmx",
        "asax",
        "php",
        "jsp",
        "htm",
        "html"
      };
      foreach (string str in strArray)
      {
        if (str.ToLower() == _fileExt.ToLower())
          return false;
      }
      string str1 = this.siteConfig.fileextension + "," + this.siteConfig.videoextension;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.ToLower() == _fileExt.ToLower())
          return true;
      }
      return false;
    }

    private bool CheckFileSize(string _fileExt, int _fileSize)
    {
      ArrayList arrayList = new ArrayList((ICollection) this.siteConfig.videoextension.ToLower().Split(','));
      if (this.IsImage(_fileExt))
      {
        if (this.siteConfig.imgsize > 0 && _fileSize > this.siteConfig.imgsize * 1024)
          return false;
      }
      else if (arrayList.Contains((object) _fileExt.ToLower()))
      {
        if (this.siteConfig.videosize > 0 && _fileSize > this.siteConfig.videosize * 1024)
          return false;
      }
      else if (this.siteConfig.attachsize > 0 && _fileSize > this.siteConfig.attachsize * 1024)
        return false;
      return true;
    }
  }
}
