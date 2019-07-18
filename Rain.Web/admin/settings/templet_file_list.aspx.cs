// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.templet_file_list
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
  public class templet_file_list : ManagePage
  {
    protected string skinName = string.Empty;
    protected HtmlForm form1;
    protected LinkButton btnDelete;
    protected Repeater rptList;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.skinName = DTRequest.GetQueryString("skin");
      if (string.IsNullOrEmpty(this.skinName))
      {
        this.JscriptMsg("传输参数不正确！", "back");
      }
      else
      {
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString());
        this.RptBind(this.skinName);
      }
    }

    private void RptBind(string skin_name)
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("name", Type.GetType("System.String"));
      dataTable.Columns.Add("skinname", Type.GetType("System.String"));
      dataTable.Columns.Add("creationtime", Type.GetType("System.String"));
      dataTable.Columns.Add("updatetime", Type.GetType("System.String"));
      foreach (FileInfo file in new DirectoryInfo(Utils.GetMapPath("../../templates/" + skin_name)).GetFiles())
      {
        if (file.Name != "about.xml" && file.Name != "about.png")
        {
          DataRow row = dataTable.NewRow();
          row["name"] = (object) file.Name;
          row["skinname"] = (object) skin_name;
          row["creationtime"] = (object) file.CreationTime;
          row["updatetime"] = (object) file.LastWriteTime;
          dataTable.Rows.Add(row);
        }
      }
      this.rptList.DataSource = (object) dataTable;
      this.rptList.DataBind();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      this.ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Delete.ToString());
      for (int index = 0; index < this.rptList.Items.Count; ++index)
      {
        string str = ((HiddenField) this.rptList.Items[index].FindControl("hideName")).Value;
        if (((CheckBox) this.rptList.Items[index].FindControl("chkId")).Checked)
          Utils.DeleteFile("../../templates/" + this.skinName + "/" + str);
      }
      this.AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除模板文件，模板:" + this.skinName);
      this.JscriptMsg("文件删除成功！", Utils.CombUrlTxt("templet_file_list.aspx", "skin={0}", this.skinName));
    }
  }
}
