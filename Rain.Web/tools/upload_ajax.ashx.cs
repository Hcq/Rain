// Decompiled with JetBrains decompiler
// Type: Rain.Web.tools.upload_ajax
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.tools
{
  public class upload_ajax : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      switch (DTRequest.GetQueryString("action"))
      {
        case "EditorFile":
          this.EditorFile(context);
          break;
        case "ManagerFile":
          this.ManagerFile(context);
          break;
        default:
          this.UpLoadFile(context);
          break;
      }
    }

    private void UpLoadFile(HttpContext context)
    {
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      string _filepath = DTRequest.GetString("DelFilePath");
      HttpPostedFile file = context.Request.Files["Filedata"];
      bool isWater = false;
      bool isThumbnail = false;
      if (DTRequest.GetQueryString("IsWater") == "1")
        isWater = true;
      if (DTRequest.GetQueryString("IsThumbnail") == "1")
        isThumbnail = true;
      if (file == null)
      {
        context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
      }
      else
      {
        string s = new UpLoad().fileSaveAs(file, isThumbnail, isWater);
        if (!string.IsNullOrEmpty(_filepath) && _filepath.IndexOf("../") == -1 && _filepath.ToLower().StartsWith(siteconfig.webpath.ToLower() + siteconfig.filepath.ToLower()))
          Utils.DeleteUpFile(_filepath);
        context.Response.Write(s);
        context.Response.End();
      }
    }

    private void EditorFile(HttpContext context)
    {
      bool isWater = false;
      if (context.Request.QueryString["IsWater"] == "1")
        isWater = true;
      HttpPostedFile file = context.Request.Files["imgFile"];
      if (file == null)
      {
        this.showError(context, "请选择要上传文件！");
      }
      else
      {
        Dictionary<string, object> dictionary = JsonHelper.DataRowFromJSON(new UpLoad().fileSaveAs(file, false, isWater));
        string str1 = dictionary["status"].ToString();
        string message = dictionary["msg"].ToString();
        if (str1 == "0")
        {
          this.showError(context, message);
        }
        else
        {
          string str2 = dictionary["path"].ToString();
          Hashtable hashtable = new Hashtable();
          hashtable[(object) "error"] = (object) 0;
          hashtable[(object) "url"] = (object) str2;
          context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
          context.Response.Write(JsonHelper.ObjectToJSON((object) hashtable));
          context.Response.End();
        }
      }
    }

    private void showError(HttpContext context, string message)
    {
      Hashtable hashtable = new Hashtable();
      hashtable[(object) "error"] = (object) 1;
      hashtable[(object) nameof (message)] = (object) message;
      context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
      context.Response.Write(JsonHelper.ObjectToJSON((object) hashtable));
      context.Response.End();
    }

    private void ManagerFile(HttpContext context)
    {
      Rain.Model.siteconfig siteconfig = new Rain.BLL.siteconfig().loadConfig();
      string strPath = siteconfig.webpath + siteconfig.filepath + "/";
      string str1 = siteconfig.webpath + siteconfig.filepath + "/";
      string str2 = "gif,jpg,jpeg,png,bmp";
      string mapPath = Utils.GetMapPath(strPath);
      string str3 = context.Request.QueryString["dir"];
      string str4 = context.Request.QueryString["path"];
      string input1 = string.IsNullOrEmpty(str4) ? "" : str4;
      string path;
      string str5;
      string input2;
      string str6;
      if (input1 == "")
      {
        path = mapPath;
        str5 = str1;
        input2 = "";
        str6 = "";
      }
      else
      {
        path = mapPath + input1;
        str5 = str1 + input1;
        input2 = input1;
        str6 = Regex.Replace(input2, "(.*?)[^\\/]+\\/$", "$1");
      }
      string str7 = context.Request.QueryString["order"];
      string str8 = string.IsNullOrEmpty(str7) ? "" : str7.ToLower();
      if (Regex.IsMatch(input1, "\\.\\."))
      {
        context.Response.Write("Access is not allowed.");
        context.Response.End();
      }
      if (input1 != "" && !input1.EndsWith("/"))
      {
        context.Response.Write("Parameter is not valid.");
        context.Response.End();
      }
      if (!Directory.Exists(path))
      {
        context.Response.Write("Directory does not exist.");
        context.Response.End();
      }
      string[] directories = Directory.GetDirectories(path);
      string[] files = Directory.GetFiles(path);
      switch (str8)
      {
        case "size":
          Array.Sort((Array) directories, (IComparer) new upload_ajax.NameSorter());
          Array.Sort((Array) files, (IComparer) new upload_ajax.SizeSorter());
          break;
        case "type":
          Array.Sort((Array) directories, (IComparer) new upload_ajax.NameSorter());
          Array.Sort((Array) files, (IComparer) new upload_ajax.TypeSorter());
          break;
        default:
          Array.Sort((Array) directories, (IComparer) new upload_ajax.NameSorter());
          Array.Sort((Array) files, (IComparer) new upload_ajax.NameSorter());
          break;
      }
      Hashtable hashtable = new Hashtable();
      hashtable[(object) "moveup_dir_path"] = (object) str6;
      hashtable[(object) "current_dir_path"] = (object) input2;
      hashtable[(object) "current_url"] = (object) str5;
      hashtable[(object) "total_count"] = (object) (directories.Length + files.Length);
      List<Hashtable> hashtableList = new List<Hashtable>();
      hashtable[(object) "file_list"] = (object) hashtableList;
      for (int index = 0; index < directories.Length; ++index)
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(directories[index]);
        hashtableList.Add(new Hashtable()
        {
          [(object) "is_dir"] = (object) true,
          [(object) "has_file"] = (object) (directoryInfo.GetFileSystemInfos().Length > 0),
          [(object) "filesize"] = (object) 0,
          [(object) "is_photo"] = (object) false,
          [(object) "filetype"] = (object) "",
          [(object) "filename"] = (object) directoryInfo.Name,
          [(object) "datetime"] = (object) directoryInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")
        });
      }
      for (int index = 0; index < files.Length; ++index)
      {
        FileInfo fileInfo = new FileInfo(files[index]);
        hashtableList.Add(new Hashtable()
        {
          [(object) "is_dir"] = (object) false,
          [(object) "has_file"] = (object) false,
          [(object) "filesize"] = (object) fileInfo.Length,
          [(object) "is_photo"] = (object) (Array.IndexOf<string>(str2.Split(','), fileInfo.Extension.Substring(1).ToLower()) >= 0),
          [(object) "filetype"] = (object) fileInfo.Extension.Substring(1),
          [(object) "filename"] = (object) fileInfo.Name,
          [(object) "datetime"] = (object) fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")
        });
      }
      context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
      context.Response.Write(JsonHelper.ObjectToJSON((object) hashtable));
      context.Response.End();
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

    public class NameSorter : IComparer
    {
      public int Compare(object x, object y)
      {
        if (x == null && y == null)
          return 0;
        if (x == null)
          return -1;
        if (y == null)
          return 1;
        return new FileInfo(x.ToString()).FullName.CompareTo(new FileInfo(y.ToString()).FullName);
      }
    }

    public class SizeSorter : IComparer
    {
      public int Compare(object x, object y)
      {
        if (x == null && y == null)
          return 0;
        if (x == null)
          return -1;
        if (y == null)
          return 1;
        return new FileInfo(x.ToString()).Length.CompareTo(new FileInfo(y.ToString()).Length);
      }
    }

    public class TypeSorter : IComparer
    {
      public int Compare(object x, object y)
      {
        if (x == null && y == null)
          return 0;
        if (x == null)
          return -1;
        if (y == null)
          return 1;
        return new FileInfo(x.ToString()).Extension.CompareTo(new FileInfo(y.ToString()).Extension);
      }
    }
  }
}
