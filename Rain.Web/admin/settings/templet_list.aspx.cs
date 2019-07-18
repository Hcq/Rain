// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.templet_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class templet_list : ManagePage
  {
    protected HtmlForm form1;
    protected LinkButton btnManage;
    protected DropDownList ddlSitePath;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString());
      this.TreeBind();
      this.RptBind();
    }

    private void TreeBind()
    {
      DataTable table = new Rain.BLL.channel_site().GetList(0, "", "sort_id asc,id desc").Tables[0];
      this.ddlSitePath.Items.Clear();
      this.ddlSitePath.Items.Add(new ListItem("生成模板到", ""));
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        if (string.IsNullOrEmpty(row["templet_path"].ToString()))
          this.ddlSitePath.Items.Add(new ListItem("├ " + row["title"].ToString(), row["build_path"].ToString()));
        else
          this.ddlSitePath.Items.Add(new ListItem("├ " + row["title"].ToString() + "(当前模板：" + row["templet_path"].ToString() + ")", row["build_path"].ToString()));
      }
    }

    private void RptBind()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("skinname", Type.GetType("System.String"));
      dataTable.Columns.Add("name", Type.GetType("System.String"));
      dataTable.Columns.Add("img", Type.GetType("System.String"));
      dataTable.Columns.Add("author", Type.GetType("System.String"));
      dataTable.Columns.Add("createdate", Type.GetType("System.String"));
      dataTable.Columns.Add("version", Type.GetType("System.String"));
      dataTable.Columns.Add("fordntver", Type.GetType("System.String"));
      foreach (DirectoryInfo directory in new DirectoryInfo(Utils.GetMapPath("../../templates/")).GetDirectories())
      {
        DataRow row = dataTable.NewRow();
        template info = this.GetInfo(directory.FullName);
        if (info != null)
        {
          row["skinname"] = (object) directory.Name;
          row["name"] = (object) info.name;
          row["img"] = (object) ("../../templates/" + directory.Name + "/about.png");
          row["author"] = (object) info.author;
          row["createdate"] = (object) info.createdate;
          row["version"] = (object) info.version;
          row["fordntver"] = (object) info.fordntver;
          dataTable.Rows.Add(row);
        }
      }
      this.rptList.DataSource = (object) dataTable;
      this.rptList.DataBind();
    }

    private template GetInfo(string xmlPath)
    {
      template template = new template();
      if (!File.Exists(xmlPath + "\\about.xml"))
        return (template) null;
      try
      {
        foreach (XmlNode readNode in XmlHelper.ReadNodes(xmlPath + "\\about.xml", "about"))
        {
          if (readNode.NodeType != XmlNodeType.Comment && readNode.Name.ToLower() == "template")
          {
            template.name = readNode.Attributes["name"] != null ? readNode.Attributes["name"].Value.ToString() : "";
            template.author = readNode.Attributes["author"] != null ? readNode.Attributes["author"].Value.ToString() : "";
            template.createdate = readNode.Attributes["createdate"] != null ? readNode.Attributes["createdate"].Value.ToString() : "";
            template.version = readNode.Attributes["version"] != null ? readNode.Attributes["version"].Value.ToString() : "";
            template.fordntver = readNode.Attributes["fordntver"] != null ? readNode.Attributes["fordntver"].Value.ToString() : "";
          }
        }
      }
      catch
      {
        return (template) null;
      }
      return template;
    }

    private void MarkTemplates(string buildPath, string skinName)
    {
      string mapPath = Utils.GetMapPath(string.Format("{0}aspx/{1}/", (object) this.siteConfig.webpath, (object) buildPath));
      List<Rain.Model.url_rewrite> list = new Rain.BLL.url_rewrite().GetList("");
      DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
      if (Directory.Exists(mapPath))
      {
        foreach (FileSystemInfo file in directoryInfo.GetFiles())
          file.Delete();
      }
      foreach (Rain.Model.url_rewrite urlRewrite in list)
      {
        if (File.Exists(Utils.GetMapPath(string.Format("{0}templates/{1}/{2}", (object) this.siteConfig.webpath, (object) skinName, (object) urlRewrite.templet))))
          PageTemplate.GetTemplate(this.siteConfig.webpath, "templates", skinName, urlRewrite.templet, urlRewrite.page, urlRewrite.inherit, buildPath, urlRewrite.channel, urlRewrite.pagesize, 1);
      }
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string str = ((HiddenField) this.rptList.Items[index].FindControl("hideSkinName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          this.Response.Redirect("templet_file_list.aspx?skin=" + Utils.UrlEncode(str));
      }
    }

    protected void ddlSitePath_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Build.ToString());
      if (this.ddlSitePath.SelectedValue == "")
      {
        this.ddlSitePath.SelectedIndex = 0;
        this.JscriptMsg("请选择生成的站点", "");
      }
      else
      {
        for (int index = 0; index < this.rptList.Items.Count; ++index)
        {
          string skinName = ((HiddenField) this.rptList.Items[index].FindControl("hideSkinName")).Value;
          if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          {
            this.MarkTemplates(this.ddlSitePath.SelectedValue, skinName);
            new Rain.BLL.channel_site().UpdateField(this.ddlSitePath.SelectedValue, "templet_path='" + skinName + "'");
            this.AddAdminLog(DTEnums.ActionEnum.Build.ToString(), "生成模板:" + skinName);
            this.JscriptMsg("生成模板成功！", "templet_list.aspx");
            return;
          }
        }
        this.ddlSitePath.SelectedIndex = 0;
        this.JscriptMsg("请选择生成的模板！", "");
      }
    }
  }
}
