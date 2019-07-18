// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.settings.builder_html
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.BLL;
using Rain.Common;
using Rain.Web.UI;

namespace Rain.Web.admin.settings
{
    public class builder_html : ManagePage
    {
        protected HtmlForm form1;
        protected Repeater rptList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;
            this.ChkAdminLevel("sys_builder_html", DTEnums.ActionEnum.View.ToString());
            this.RptBind();
        }

        private void RptBind()
        {
            this.rptList.DataSource = (object)new channel_site().GetList(0, string.Empty, "sort_id asc,id desc");
            this.rptList.DataBind();
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Rain.BLL.channel channel = new Rain.BLL.channel();
            if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
                return;
            Repeater control = (Repeater)e.Item.FindControl("rptChannel");
            int int32 = Convert.ToInt32(((DataRowView)e.Item.DataItem)["id"]);
            control.DataSource = (object)channel.GetList(0, "site_id=" + (object)int32, "sort_id asc,id desc");
            control.DataBind();
        }
    }
}
