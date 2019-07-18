// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.plugin_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class plugin_list : ManagePage
  {
    protected HtmlForm form1;
    protected LinkButton lbtnInstall;
    protected LinkButton lbtnUnInstall;
    protected LinkButton lbtnRemark;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ChkAdminLevel("sys_plugin_config", DTEnums.ActionEnum.View.ToString());
      this.RptBind();
    }

    private void RptBind()
    {
      List<Rain.Model.plugin> pluginList = new List<Rain.Model.plugin>();
      this.rptList.DataSource = (object) new Rain.BLL.plugin().GetList(Utils.GetMapPath("../../plugins/"));
      this.rptList.DataBind();
    }

    private void RemoveTemplates(string dirName)
    {
      XmlNodeList xmlNodeList = XmlHelper.ReadNodes(Utils.GetMapPath("../../plugins/" + dirName + "/plugin.config"), "plugin/urls");
      if (xmlNodeList.Count <= 0)
        return;
      foreach (XmlElement xmlElement in xmlNodeList)
      {
        if (xmlElement.NodeType != XmlNodeType.Comment && xmlElement.Name.ToLower() == "rewrite" && xmlElement.Attributes["page"] != null && (xmlElement.Attributes["name"] != null && xmlElement.Attributes["type"] != null))
          Utils.DeleteFile(this.siteConfig.webpath + "aspx/plugin/" + xmlElement.Attributes["page"].Value);
      }
    }

    protected void lbtnInstall_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_plugin_config", DTEnums.ActionEnum.Instal.ToString());
      string mapPath1 = Utils.GetMapPath("../../plugins/");
      Rain.BLL.plugin plugin = new Rain.BLL.plugin();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string str = ((HiddenField) this.rptList.Items[index].FindControl("hidDirName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked && plugin.GetInfo(mapPath1 + str + "\\").isload == 0)
        {
          string path = mapPath1 + str + "\\bin\\";
          if (Directory.Exists(path))
          {
            foreach (string file in Directory.GetFiles(path))
            {
              FileInfo fileInfo = new FileInfo(file);
              if (fileInfo.Extension.ToLower() == ".dll")
              {
                string mapPath2 = Utils.GetMapPath(this.siteConfig.webpath + "bin\\" + fileInfo.Name);
                File.Copy(fileInfo.FullName, mapPath2, true);
              }
            }
          }
          plugin.ExeSqlStr(mapPath1 + str + "\\", "plugin/install");
          plugin.AppendNodes(mapPath1 + str + "\\", "plugin/urls");
          plugin.AppendMenuNodes(string.Format("{0}plugins/{1}/", (object) this.siteConfig.webpath, (object) str), mapPath1 + str + "\\", "plugin/menu", "sys_plugin_manage");
          plugin.MarkTemplet(this.siteConfig.webpath, "plugins/" + str, "templet", mapPath1 + str + "\\", "plugin/urls");
          plugin.UpdateNodeValue(mapPath1 + str + "\\", "plugin/isload", "1");
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Instal.ToString(), "安装插件");
      this.JscriptMsg("插件安装成功！", "plugin_list.aspx", "parent.loadMenuTree");
    }

    protected void lbtnUnInstall_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_plugin_config", DTEnums.ActionEnum.UnLoad.ToString());
      string mapPath1 = Utils.GetMapPath("../../plugins/");
      Rain.BLL.plugin plugin = new Rain.BLL.plugin();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string dirName = ((HiddenField) this.rptList.Items[index].FindControl("hidDirName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked && plugin.GetInfo(mapPath1 + dirName + "\\").isload == 1)
        {
          string path = mapPath1 + dirName + "/bin/";
          if (Directory.Exists(path))
          {
            foreach (string file in Directory.GetFiles(path))
            {
              FileInfo fileInfo = new FileInfo(file);
              if (fileInfo.Extension.ToLower() == ".dll")
              {
                string mapPath2 = Utils.GetMapPath(this.siteConfig.webpath + "bin/" + fileInfo.Name);
                if (File.Exists(mapPath2))
                  File.Delete(mapPath2);
              }
            }
          }
          plugin.ExeSqlStr(mapPath1 + dirName + "\\", "plugin/uninstall");
          plugin.RemoveNodes(mapPath1 + dirName + "\\", "plugin/urls");
          plugin.RemoveMenuNodes(mapPath1 + dirName + "\\", "plugin/menu");
          this.RemoveTemplates(dirName);
          plugin.UpdateNodeValue(mapPath1 + dirName + "\\", "plugin/isload", "0");
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.UnLoad.ToString(), "卸载插件");
      this.JscriptMsg("插件卸载成功！", "plugin_list.aspx", "parent.loadMenuTree");
    }

    protected void lbtnRemark_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_plugin_config", DTEnums.ActionEnum.Build.ToString());
      string mapPath = Utils.GetMapPath("../../plugins/");
      Rain.BLL.plugin plugin = new Rain.BLL.plugin();
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string str = ((HiddenField) this.rptList.Items[index].FindControl("hidDirName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
        {
          if (plugin.GetInfo(mapPath + str + "\\").isload == 1)
            plugin.MarkTemplet(this.siteConfig.webpath, "plugins/" + str, "templet", mapPath + str + "\\", "plugin/urls");
          else
            this.JscriptMsg("该插件尚未安装！", "plugin_list.aspx");
        }
      }
      this.AddAdminLog(DTEnums.ActionEnum.Build.ToString(), "生成插件模板");
      this.JscriptMsg("生成模板成功！", "plugin_list.aspx");
    }
  }
}
