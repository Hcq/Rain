// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.templet_file_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class templet_file_edit : ManagePage
  {
    protected HtmlForm form1;
    protected TextBox txtContent;
    protected Button btnSubmit;
    protected string filePath;
    protected string pathName;
    protected string fileName;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.pathName = DTRequest.GetQueryString("path");
      this.fileName = DTRequest.GetQueryString("filename");
      if (string.IsNullOrEmpty(this.pathName) || string.IsNullOrEmpty(this.fileName))
      {
        this.JscriptMsg("传输参数不正确！", "back");
      }
      else
      {
        this.filePath = Utils.GetMapPath("../../templates/" + this.pathName.Replace(".", "") + "/" + this.fileName.Replace("/", ""));
        if (!File.Exists(this.filePath))
        {
          this.JscriptMsg("该文件不存在！", "back");
        }
        else
        {
          if (this.Page.IsPostBack)
            return;
          this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString());
          this.ShowInfo(this.filePath);
        }
      }
    }

    private void ShowInfo(string _path)
    {
      using (StreamReader streamReader = new StreamReader(_path, Encoding.UTF8))
      {
        this.txtContent.Text = streamReader.ReadToEnd();
        streamReader.Close();
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Edit.ToString());
      using (FileStream fileStream = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
      {
        byte[] bytes = Encoding.UTF8.GetBytes(this.txtContent.Text);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
      }
      this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改模板文件:" + this.fileName);
      this.JscriptMsg("模板保存成功！", Utils.CombUrlTxt("templet_file_list.aspx", "skin={0}", this.pathName));
    }
  }
}
