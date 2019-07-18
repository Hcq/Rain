// Decompiled with JetBrains decompiler
// Type: Rain.Web.admin.article.article_edit
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Rain.Common;
using Rain.Model;
using Rain.Web.UI;

namespace Rain.Web.admin.article
{
  public class article_edit : ManagePage
  {
    private string action = DTEnums.ActionEnum.Add.ToString();
    protected string channel_name = string.Empty;
    private int id = 0;
    protected HtmlForm form1;
    protected HtmlGenericControl field_tab_item;
    protected DropDownList ddlCategoryId;
    protected RadioButtonList rblStatus;
    protected CheckBoxList cblItem;
    protected TextBox txtTitle;
    protected HtmlGenericControl div_sub_title;
    protected Label div_sub_title_title;
    protected TextBox field_control_sub_title;
    protected Label div_sub_title_tip;
    protected TextBox txtImgUrl;
    protected HtmlGenericControl div_goods_no;
    protected Label div_goods_no_title;
    protected TextBox field_control_goods_no;
    protected Label div_goods_no_tip;
    protected HtmlGenericControl div_stock_quantity;
    protected Label div_stock_quantity_title;
    protected TextBox field_control_stock_quantity;
    protected Label div_stock_quantity_tip;
    protected HtmlGenericControl div_market_price;
    protected Label div_market_price_title;
    protected TextBox field_control_market_price;
    protected Label div_market_price_tip;
    protected HtmlGenericControl div_sell_price;
    protected Label div_sell_price_title;
    protected TextBox field_control_sell_price;
    protected Label div_sell_price_tip;
    protected Repeater rptPrice;
    protected HtmlGenericControl div_point;
    protected Label div_point_title;
    protected TextBox field_control_point;
    protected Label div_point_tip;
    protected TextBox txtSortId;
    protected TextBox txtClick;
    protected TextBox txtAddTime;
    protected HtmlGenericControl div_albums_container;
    protected HtmlInputHidden hidFocusPhoto;
    protected Repeater rptAlbumList;
    protected HtmlGenericControl div_attach_container;
    protected Repeater rptAttachList;
    protected TextBox txtCallIndex;
    protected TextBox txtLinkUrl;
    protected HtmlGenericControl div_source;
    protected Label div_source_title;
    protected TextBox field_control_source;
    protected Label div_source_tip;
    protected HtmlGenericControl div_author;
    protected Label div_author_title;
    protected TextBox field_control_author;
    protected Label div_author_tip;
    protected TextBox txtZhaiyao;
    protected HtmlTextArea txtContent;
    protected HtmlGenericControl field_tab_content;
    protected TextBox txtSeoTitle;
    protected TextBox txtSeoKeywords;
    protected TextBox txtSeoDescription;
    protected Button btnSubmit;
    protected int channel_id;

    protected void Page_Init(object sernder, EventArgs e)
    {
      this.channel_id = DTRequest.GetQueryInt("channel_id");
      this.CreateOtherField(this.channel_id);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      string queryString = DTRequest.GetQueryString("action");
      if (this.channel_id == 0)
      {
        this.JscriptMsg("频道参数不正确！", "back");
      }
      else
      {
        this.channel_name = new Rain.BLL.channel().GetChannelName(this.channel_id);
        if (queryString == DTEnums.ActionEnum.Edit.ToString() || queryString == DTEnums.ActionEnum.Copy.ToString())
        {
          this.action = queryString;
          this.id = DTRequest.GetQueryInt("id");
          if (this.id == 0)
          {
            this.JscriptMsg("传输参数不正确！", "back");
            return;
          }
          if (!new Rain.BLL.article().Exists(this.id))
          {
            this.JscriptMsg("信息不存在或已被删除！", "back");
            return;
          }
        }
        if (this.Page.IsPostBack)
          return;
        this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.View.ToString());
        this.ShowSysField(this.channel_id);
        this.GroupBind(string.Empty);
        this.TreeBind(this.channel_id);
        if (this.action == DTEnums.ActionEnum.Edit.ToString())
          this.ShowInfo(this.id);
      }
    }

    private void CreateOtherField(int _channel_id)
    {
      List<Rain.Model.article_attribute_field> modelList = new Rain.BLL.article_attribute_field().GetModelList(this.channel_id, "is_sys=0");
      if (modelList.Count > 0)
      {
        this.field_tab_item.Visible = true;
        this.field_tab_content.Visible = true;
      }
      foreach (Rain.Model.article_attribute_field articleAttributeField in modelList)
      {
        HtmlGenericControl htmlGenericControl1 = new HtmlGenericControl("dl");
        HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("dt");
        HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("dd");
        htmlGenericControl2.InnerHtml = articleAttributeField.title;
        switch (articleAttributeField.control_type)
        {
          case "single-text":
          case "multi-text":
          case "images":
          case "video":
          case "number":
          case "datetime":
            TextBox textBox = new TextBox();
            textBox.ID = "field_control_" + articleAttributeField.name;
            if (articleAttributeField.control_type == "single-text")
            {
              textBox.CssClass = "input normal";
              if (articleAttributeField.is_password == 1)
                textBox.TextMode = TextBoxMode.Password;
            }
            else if (articleAttributeField.control_type == "multi-text")
            {
              textBox.CssClass = "input";
              textBox.TextMode = TextBoxMode.MultiLine;
            }
            else if (articleAttributeField.control_type == "number")
              textBox.CssClass = "input small";
            else if (articleAttributeField.control_type == "datetime")
            {
              textBox.CssClass = "input rule-date-input";
              textBox.Attributes.Add("onfocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
            }
            else if (articleAttributeField.control_type == "images" || articleAttributeField.control_type == "video")
              textBox.CssClass = "input normal upload-path";
            textBox.Text = articleAttributeField.default_value;
            if (!string.IsNullOrEmpty(articleAttributeField.valid_tip_msg))
              textBox.Attributes.Add("tipmsg", articleAttributeField.valid_tip_msg);
            if (!string.IsNullOrEmpty(articleAttributeField.valid_error_msg))
              textBox.Attributes.Add("errormsg", articleAttributeField.valid_error_msg);
            if (!string.IsNullOrEmpty(articleAttributeField.valid_pattern))
            {
              textBox.Attributes.Add("datatype", articleAttributeField.valid_pattern);
              textBox.Attributes.Add("sucmsg", " ");
            }
            Label label1 = new Label();
            label1.CssClass = "Validform_checktip";
            label1.Text = articleAttributeField.valid_tip_msg;
            htmlGenericControl3.Controls.Add((Control) textBox);
            if (articleAttributeField.control_type == "images")
            {
              HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("div");
              htmlGenericControl4.Attributes.Add("class", "upload-box upload-img");
              htmlGenericControl4.Attributes.Add("style", "margin-left:4px;");
              htmlGenericControl3.Controls.Add((Control) htmlGenericControl4);
            }
            if (articleAttributeField.control_type == "video")
            {
              HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("div");
              htmlGenericControl4.Attributes.Add("class", "upload-box upload-video");
              htmlGenericControl4.Attributes.Add("style", "margin-left:4px;");
              htmlGenericControl3.Controls.Add((Control) htmlGenericControl4);
            }
            htmlGenericControl3.Controls.Add((Control) label1);
            break;
          case "editor":
            HtmlTextArea htmlTextArea = new HtmlTextArea();
            htmlTextArea.ID = "field_control_" + articleAttributeField.name;
            htmlTextArea.Attributes.Add("style", "visibility:hidden;");
            if (articleAttributeField.editor_type == 1)
              htmlTextArea.Attributes.Add("class", "editor-mini");
            else
              htmlTextArea.Attributes.Add("class", "editor");
            htmlTextArea.Value = articleAttributeField.default_value;
            if (!string.IsNullOrEmpty(articleAttributeField.valid_tip_msg))
              htmlTextArea.Attributes.Add("tipmsg", articleAttributeField.valid_tip_msg);
            if (!string.IsNullOrEmpty(articleAttributeField.valid_error_msg))
              htmlTextArea.Attributes.Add("errormsg", articleAttributeField.valid_error_msg);
            if (!string.IsNullOrEmpty(articleAttributeField.valid_pattern))
            {
              htmlTextArea.Attributes.Add("datatype", articleAttributeField.valid_pattern);
              htmlTextArea.Attributes.Add("sucmsg", " ");
            }
            Label label2 = new Label();
            label2.CssClass = "Validform_checktip";
            label2.Text = articleAttributeField.valid_tip_msg;
            htmlGenericControl3.Controls.Add((Control) htmlTextArea);
            htmlGenericControl3.Controls.Add((Control) label2);
            break;
          case "checkbox":
            CheckBox checkBox = new CheckBox();
            checkBox.ID = "field_control_" + articleAttributeField.name;
            if (articleAttributeField.default_value == "1")
              checkBox.Checked = true;
            HtmlGenericControl htmlGenericControl5 = new HtmlGenericControl("div");
            htmlGenericControl5.Attributes.Add("class", "rule-single-checkbox");
            htmlGenericControl5.Controls.Add((Control) checkBox);
            htmlGenericControl3.Controls.Add((Control) htmlGenericControl5);
            if (!string.IsNullOrEmpty(articleAttributeField.valid_tip_msg))
            {
              Label label3 = new Label();
              label3.CssClass = "Validform_checktip";
              label3.Text = articleAttributeField.valid_tip_msg;
              htmlGenericControl3.Controls.Add((Control) label3);
              break;
            }
            break;
          case "multi-radio":
            RadioButtonList radioButtonList = new RadioButtonList();
            radioButtonList.ID = "field_control_" + articleAttributeField.name;
            radioButtonList.RepeatDirection = RepeatDirection.Horizontal;
            radioButtonList.RepeatLayout = RepeatLayout.Flow;
            HtmlGenericControl htmlGenericControl6 = new HtmlGenericControl("div");
            htmlGenericControl6.Attributes.Add("class", "rule-multi-radio");
            htmlGenericControl6.Controls.Add((Control) radioButtonList);
            string itemOption1 = articleAttributeField.item_option;
            string[] separator1 = new string[2]
            {
              "\r\n",
              "\n"
            };
            foreach (string str in itemOption1.Split(separator1, StringSplitOptions.None))
            {
              char[] chArray = new char[1]{ '|' };
              string[] strArray = str.Split(chArray);
              if (strArray.Length == 2)
                radioButtonList.Items.Add(new ListItem(strArray[0], strArray[1]));
            }
            radioButtonList.SelectedValue = articleAttributeField.default_value;
            Label label4 = new Label();
            label4.CssClass = "Validform_checktip";
            label4.Text = articleAttributeField.valid_tip_msg;
            htmlGenericControl3.Controls.Add((Control) htmlGenericControl6);
            htmlGenericControl3.Controls.Add((Control) label4);
            break;
          case "multi-checkbox":
            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "field_control_" + articleAttributeField.name;
            checkBoxList.RepeatDirection = RepeatDirection.Horizontal;
            checkBoxList.RepeatLayout = RepeatLayout.Flow;
            HtmlGenericControl htmlGenericControl7 = new HtmlGenericControl("div");
            htmlGenericControl7.Attributes.Add("class", "rule-multi-checkbox");
            htmlGenericControl7.Controls.Add((Control) checkBoxList);
            string itemOption2 = articleAttributeField.item_option;
            string[] separator2 = new string[2]
            {
              "\r\n",
              "\n"
            };
            foreach (string str in itemOption2.Split(separator2, StringSplitOptions.None))
            {
              char[] chArray = new char[1]{ '|' };
              string[] strArray = str.Split(chArray);
              if (strArray.Length == 2)
                checkBoxList.Items.Add(new ListItem(strArray[0], strArray[1]));
            }
            checkBoxList.SelectedValue = articleAttributeField.default_value;
            Label label5 = new Label();
            label5.CssClass = "Validform_checktip";
            label5.Text = articleAttributeField.valid_tip_msg;
            htmlGenericControl3.Controls.Add((Control) htmlGenericControl7);
            htmlGenericControl3.Controls.Add((Control) label5);
            break;
        }
        htmlGenericControl1.Controls.Add((Control) htmlGenericControl2);
        htmlGenericControl1.Controls.Add((Control) htmlGenericControl3);
        this.field_tab_content.Controls.Add((Control) htmlGenericControl1);
      }
    }

    private void ShowSysField(int _channel_id)
    {
      foreach (Rain.Model.article_attribute_field model in new Rain.BLL.article_attribute_field().GetModelList(this.channel_id, "is_sys=1"))
      {
        HtmlGenericControl control = this.FindControl("div_" + model.name) as HtmlGenericControl;
        if (control != null)
        {
          control.Visible = true;
          ((Label) control.FindControl("div_" + model.name + "_title")).Text = model.title;
          ((TextBox) control.FindControl("field_control_" + model.name)).Text = model.default_value;
          ((Label) control.FindControl("div_" + model.name + "_tip")).Text = model.valid_tip_msg;
        }
      }
      Rain.Model.channel model1 = new Rain.BLL.channel().GetModel(_channel_id);
      if (model1.is_albums == 1)
        this.div_albums_container.Visible = true;
      if (model1.is_attach != 1)
        return;
      this.div_attach_container.Visible = true;
    }

    private void TreeBind(int _channel_id)
    {
      DataTable list = new Rain.BLL.article_category().GetList(0, _channel_id);
      this.ddlCategoryId.Items.Clear();
      this.ddlCategoryId.Items.Add(new ListItem("请选择类别...", ""));
      foreach (DataRow row in (InternalDataCollectionBase) list.Rows)
      {
        string str1 = row["id"].ToString();
        int num = int.Parse(row["class_layer"].ToString());
        string text = row["title"].ToString().Trim();
        if (num == 1)
        {
          this.ddlCategoryId.Items.Add(new ListItem(text, str1));
        }
        else
        {
          string str2 = "├ " + text;
          this.ddlCategoryId.Items.Add(new ListItem(Utils.StringOfChar(num - 1, "　") + str2, str1));
        }
      }
    }

    private void GroupBind(string strWhere)
    {
      if (this.siteConfig.memberstatus == 0)
        return;
      Rain.Model.channel model = new Rain.BLL.channel().GetModel(this.channel_id);
      if (model == null || model.is_spec == 0)
        return;
      DataSet list = new Rain.BLL.user_groups().GetList(0, strWhere, "grade asc,id desc");
      if (list.Tables[0].Rows.Count <= 0)
        return;
      this.rptPrice.DataSource = (object) list;
      this.rptPrice.DataBind();
    }

    private void ShowInfo(int _id)
    {
      Rain.Model.article model1 = new Rain.BLL.article().GetModel(_id);
      this.ddlCategoryId.SelectedValue = model1.category_id.ToString();
      this.txtCallIndex.Text = model1.call_index;
      this.txtTitle.Text = model1.title;
      this.txtLinkUrl.Text = model1.link_url;
      string str1 = model1.img_url.Substring(model1.img_url.LastIndexOf("/") + 1);
      if (!str1.StartsWith("thumb_"))
        this.txtImgUrl.Text = model1.img_url;
      this.txtSeoTitle.Text = model1.seo_title;
      this.txtSeoKeywords.Text = model1.seo_keywords;
      this.txtSeoDescription.Text = model1.seo_description;
      this.txtZhaiyao.Text = model1.zhaiyao;
      this.txtContent.Value = model1.content;
      TextBox txtSortId = this.txtSortId;
      int num = model1.sort_id;
      string str2 = num.ToString();
      txtSortId.Text = str2;
      TextBox txtClick = this.txtClick;
      num = model1.click;
      string str3 = num.ToString();
      txtClick.Text = str3;
      this.rblStatus.SelectedValue = model1.status.ToString();
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
        this.txtAddTime.Text = model1.add_time.ToString("yyyy-MM-dd HH:mm:ss");
      if (model1.is_msg == 1)
        this.cblItem.Items[0].Selected = true;
      if (model1.is_top == 1)
        this.cblItem.Items[1].Selected = true;
      if (model1.is_red == 1)
        this.cblItem.Items[2].Selected = true;
      if (model1.is_hot == 1)
        this.cblItem.Items[3].Selected = true;
      if (model1.is_slide == 1)
        this.cblItem.Items[4].Selected = true;
      foreach (Rain.Model.article_attribute_field model2 in new Rain.BLL.article_attribute_field().GetModelList(this.channel_id, ""))
      {
        switch (model2.control_type)
        {
          case "single-text":
          case "multi-text":
          case "images":
          case "video":
          case "number":
          case "datetime":
            TextBox control1 = this.FindControl("field_control_" + model2.name) as TextBox;
            if (control1 != null && model1.fields.ContainsKey(model2.name))
            {
              if (model2.is_password == 1)
                control1.Attributes.Add("value", model1.fields[model2.name]);
              else
                control1.Text = model1.fields[model2.name];
              break;
            }
            break;
          case "editor":
            HtmlTextArea control2 = this.FindControl("field_control_" + model2.name) as HtmlTextArea;
            if (control2 != null && model1.fields.ContainsKey(model2.name))
            {
              control2.Value = model1.fields[model2.name];
              break;
            }
            break;
          case "checkbox":
            CheckBox control3 = this.FindControl("field_control_" + model2.name) as CheckBox;
            if (control3 != null && model1.fields.ContainsKey(model2.name))
            {
              control3.Checked = model1.fields[model2.name] == "1";
              break;
            }
            break;
          case "multi-radio":
            RadioButtonList control4 = this.FindControl("field_control_" + model2.name) as RadioButtonList;
            if (control4 != null && model1.fields.ContainsKey(model2.name))
            {
              control4.SelectedValue = model1.fields[model2.name];
              break;
            }
            break;
          case "multi-checkbox":
            CheckBoxList control5 = this.FindControl("field_control_" + model2.name) as CheckBoxList;
            if (control5 != null && model1.fields.ContainsKey(model2.name))
            {
              string[] strArray = model1.fields[model2.name].Split(',');
              for (int index = 0; index < control5.Items.Count; ++index)
              {
                control5.Items[index].Selected = false;
                foreach (string str4 in strArray)
                {
                  if (control5.Items[index].Value == str4)
                    control5.Items[index].Selected = true;
                }
              }
              break;
            }
            break;
        }
      }
      if (str1.StartsWith("thumb_"))
        this.hidFocusPhoto.Value = model1.img_url;
      this.rptAlbumList.DataSource = (object) model1.albums;
      this.rptAlbumList.DataBind();
      this.rptAttachList.DataSource = (object) model1.attach;
      this.rptAttachList.DataBind();
      if (model1.group_price == null)
        return;
      for (int index = 0; index < this.rptPrice.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptPrice.Items[index].FindControl("hideGroupId")).Value);
        foreach (user_group_price userGroupPrice in model1.group_price)
        {
          if (int32 == userGroupPrice.group_id)
          {
            ((HiddenField) this.rptPrice.Items[index].FindControl("hidePriceId")).Value = userGroupPrice.id.ToString();
            ((TextBox) this.rptPrice.Items[index].FindControl("txtGroupPrice")).Text = userGroupPrice.price.ToString();
          }
        }
      }
    }

    private Dictionary<string, string> SetFieldValues(int _channel_id)
    {
      DataTable table = new Rain.BLL.article_attribute_field().GetList(_channel_id, "").Tables[0];
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        switch (row["control_type"].ToString())
        {
          case "single-text":
          case "multi-text":
          case "images":
          case "video":
          case "number":
          case "datetime":
            TextBox control1 = this.FindControl("field_control_" + row["name"].ToString()) as TextBox;
            if (control1 != null)
            {
              dictionary.Add(row["name"].ToString(), control1.Text.Trim());
              break;
            }
            break;
          case "editor":
            HtmlTextArea control2 = this.FindControl("field_control_" + row["name"].ToString()) as HtmlTextArea;
            if (control2 != null)
            {
              dictionary.Add(row["name"].ToString(), control2.Value);
              break;
            }
            break;
          case "checkbox":
            CheckBox control3 = this.FindControl("field_control_" + row["name"].ToString()) as CheckBox;
            if (control3 != null)
            {
              if (control3.Checked)
                dictionary.Add(row["name"].ToString(), "1");
              else
                dictionary.Add(row["name"].ToString(), "0");
              break;
            }
            break;
          case "multi-radio":
            RadioButtonList control4 = this.FindControl("field_control_" + row["name"].ToString()) as RadioButtonList;
            if (control4 != null)
            {
              dictionary.Add(row["name"].ToString(), control4.SelectedValue);
              break;
            }
            break;
          case "multi-checkbox":
            CheckBoxList control5 = this.FindControl("field_control_" + row["name"].ToString()) as CheckBoxList;
            if (control5 != null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              for (int index = 0; index < control5.Items.Count; ++index)
              {
                if (control5.Items[index].Selected)
                  stringBuilder.Append(control5.Items[index].Value.Replace(',', '，') + ",");
              }
              dictionary.Add(row["name"].ToString(), Utils.DelLastComma(stringBuilder.ToString()));
              break;
            }
            break;
        }
      }
      return dictionary;
    }

    private bool DoAdd()
    {
      bool flag = false;
      Rain.Model.article model = new Rain.Model.article();
      Rain.BLL.article article = new Rain.BLL.article();
      model.channel_id = this.channel_id;
      model.category_id = Utils.StrToInt(this.ddlCategoryId.SelectedValue, 0);
      model.call_index = this.txtCallIndex.Text.Trim();
      model.title = this.txtTitle.Text.Trim();
      model.link_url = this.txtLinkUrl.Text.Trim();
      model.img_url = this.txtImgUrl.Text;
      model.seo_title = this.txtSeoTitle.Text.Trim();
      model.seo_keywords = this.txtSeoKeywords.Text.Trim();
      model.seo_description = this.txtSeoDescription.Text.Trim();
      model.zhaiyao = !string.IsNullOrEmpty(this.txtZhaiyao.Text.Trim()) ? Utils.DropHTML(this.txtZhaiyao.Text, (int) byte.MaxValue) : Utils.DropHTML(this.txtContent.Value, (int) byte.MaxValue);
      model.content = this.txtContent.Value;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.click = int.Parse(this.txtClick.Text.Trim());
      model.status = Utils.StrToInt(this.rblStatus.SelectedValue, 0);
      model.is_msg = 0;
      model.is_top = 0;
      model.is_red = 0;
      model.is_hot = 0;
      model.is_slide = 0;
      if (this.cblItem.Items[0].Selected)
        model.is_msg = 1;
      if (this.cblItem.Items[1].Selected)
        model.is_top = 1;
      if (this.cblItem.Items[2].Selected)
        model.is_red = 1;
      if (this.cblItem.Items[3].Selected)
        model.is_hot = 1;
      if (this.cblItem.Items[4].Selected)
        model.is_slide = 1;
      model.is_sys = 1;
      model.user_name = this.GetAdminInfo().user_name;
      model.add_time = Utils.StrToDateTime(this.txtAddTime.Text.Trim());
      model.fields = this.SetFieldValues(this.channel_id);
      if (this.txtImgUrl.Text.Trim() == "")
        model.img_url = this.hidFocusPhoto.Value;
      string[] values1 = this.Request.Form.GetValues("hid_photo_name");
      string[] values2 = this.Request.Form.GetValues("hid_photo_remark");
      if (values1 != null && values1.Length > 0)
      {
        List<article_albums> articleAlbumsList = new List<article_albums>();
        for (int index = 0; index < values1.Length; ++index)
        {
          string[] strArray = values1[index].Split('|');
          if (strArray.Length == 3)
          {
            if (!string.IsNullOrEmpty(values2[index]))
              articleAlbumsList.Add(new article_albums()
              {
                original_path = strArray[1],
                thumb_path = strArray[2],
                remark = values2[index]
              });
            else
              articleAlbumsList.Add(new article_albums()
              {
                original_path = strArray[1],
                thumb_path = strArray[2]
              });
          }
        }
        model.albums = articleAlbumsList;
      }
      string[] values3 = this.Request.Form.GetValues("hid_attach_filename");
      string[] values4 = this.Request.Form.GetValues("hid_attach_filepath");
      string[] values5 = this.Request.Form.GetValues("hid_attach_filesize");
      string[] values6 = this.Request.Form.GetValues("txt_attach_point");
      if (values3 != null && values4 != null && (values5 != null && values6 != null) && (values3.Length > 0 && values4.Length > 0 && values5.Length > 0) && values6.Length > 0)
      {
        List<Rain.Model.article_attach> articleAttachList = new List<Rain.Model.article_attach>();
        for (int index = 0; index < values3.Length; ++index)
        {
          int num1 = Utils.StrToInt(values5[index], 0);
          string fileExt = Utils.GetFileExt(values4[index]);
          int num2 = Utils.StrToInt(values6[index], 0);
          articleAttachList.Add(new Rain.Model.article_attach()
          {
            file_name = values3[index],
            file_path = values4[index],
            file_size = num1,
            file_ext = fileExt,
            point = num2
          });
        }
        model.attach = articleAttachList;
      }
      List<user_group_price> userGroupPriceList = new List<user_group_price>();
      for (int index = 0; index < this.rptPrice.Items.Count; ++index)
      {
        int int32 = Convert.ToInt32(((HiddenField) this.rptPrice.Items[index].FindControl("hideGroupId")).Value);
        Decimal num = Convert.ToDecimal(((TextBox) this.rptPrice.Items[index].FindControl("txtGroupPrice")).Text.Trim());
        userGroupPriceList.Add(new user_group_price()
        {
          group_id = int32,
          price = num
        });
      }
      model.group_price = userGroupPriceList;
      if (article.Add(model) > 0)
      {
        this.AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道内容:" + model.title);
        flag = true;
      }
      return flag;
    }

    private bool DoEdit(int _id)
    {
      bool flag = false;
      Rain.BLL.article article = new Rain.BLL.article();
      Rain.Model.article model = article.GetModel(_id);
      model.channel_id = this.channel_id;
      model.category_id = Utils.StrToInt(this.ddlCategoryId.SelectedValue, 0);
      model.call_index = this.txtCallIndex.Text.Trim();
      model.title = this.txtTitle.Text.Trim();
      model.link_url = this.txtLinkUrl.Text.Trim();
      model.img_url = this.txtImgUrl.Text;
      model.seo_title = this.txtSeoTitle.Text.Trim();
      model.seo_keywords = this.txtSeoKeywords.Text.Trim();
      model.seo_description = this.txtSeoDescription.Text.Trim();
      model.zhaiyao = !string.IsNullOrEmpty(this.txtZhaiyao.Text.Trim()) ? Utils.DropHTML(this.txtZhaiyao.Text, (int) byte.MaxValue) : Utils.DropHTML(this.txtContent.Value, (int) byte.MaxValue);
      model.content = this.txtContent.Value;
      model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 99);
      model.click = int.Parse(this.txtClick.Text.Trim());
      model.status = Utils.StrToInt(this.rblStatus.SelectedValue, 0);
      model.is_msg = 0;
      model.is_top = 0;
      model.is_red = 0;
      model.is_hot = 0;
      model.is_slide = 0;
      if (this.cblItem.Items[0].Selected)
        model.is_msg = 1;
      if (this.cblItem.Items[1].Selected)
        model.is_top = 1;
      if (this.cblItem.Items[2].Selected)
        model.is_red = 1;
      if (this.cblItem.Items[3].Selected)
        model.is_hot = 1;
      if (this.cblItem.Items[4].Selected)
        model.is_slide = 1;
      model.add_time = Utils.StrToDateTime(this.txtAddTime.Text.Trim());
      model.update_time = new DateTime?(DateTime.Now);
      model.fields = this.SetFieldValues(this.channel_id);
      if (this.txtImgUrl.Text.Trim() == "")
        model.img_url = this.hidFocusPhoto.Value;
      if (model.albums != null)
        model.albums.Clear();
      string[] values1 = this.Request.Form.GetValues("hid_photo_name");
      string[] values2 = this.Request.Form.GetValues("hid_photo_remark");
      if (values1 != null)
      {
        List<article_albums> articleAlbumsList = new List<article_albums>();
        for (int index = 0; index < values1.Length; ++index)
        {
          string[] strArray = values1[index].Split('|');
          int num = Utils.StrToInt(strArray[0], 0);
          if (strArray.Length == 3)
          {
            if (!string.IsNullOrEmpty(values2[index]))
              articleAlbumsList.Add(new article_albums()
              {
                id = num,
                article_id = _id,
                original_path = strArray[1],
                thumb_path = strArray[2],
                remark = values2[index]
              });
            else
              articleAlbumsList.Add(new article_albums()
              {
                id = num,
                article_id = _id,
                original_path = strArray[1],
                thumb_path = strArray[2]
              });
          }
        }
        model.albums = articleAlbumsList;
      }
      if (model.attach != null)
        model.attach.Clear();
      string[] values3 = this.Request.Form.GetValues("hid_attach_id");
      string[] values4 = this.Request.Form.GetValues("hid_attach_filename");
      string[] values5 = this.Request.Form.GetValues("hid_attach_filepath");
      string[] values6 = this.Request.Form.GetValues("hid_attach_filesize");
      string[] values7 = this.Request.Form.GetValues("txt_attach_point");
      if (values3 != null && values4 != null && (values5 != null && values6 != null) && (values7 != null && values3.Length > 0 && (values4.Length > 0 && values5.Length > 0)) && values6.Length > 0 && values7.Length > 0)
      {
        List<Rain.Model.article_attach> articleAttachList = new List<Rain.Model.article_attach>();
        for (int index = 0; index < values4.Length; ++index)
        {
          int num1 = Utils.StrToInt(values3[index], 0);
          int num2 = Utils.StrToInt(values6[index], 0);
          string fileExt = Utils.GetFileExt(values5[index]);
          int num3 = Utils.StrToInt(values7[index], 0);
          articleAttachList.Add(new Rain.Model.article_attach()
          {
            id = num1,
            article_id = _id,
            file_name = values4[index],
            file_path = values5[index],
            file_size = num2,
            file_ext = fileExt,
            point = num3
          });
        }
        model.attach = articleAttachList;
      }
      List<user_group_price> userGroupPriceList = new List<user_group_price>();
      for (int index = 0; index < this.rptPrice.Items.Count; ++index)
      {
        int num1 = 0;
        if (!string.IsNullOrEmpty(((HiddenField) this.rptPrice.Items[index].FindControl("hidePriceId")).Value))
          num1 = Convert.ToInt32(((HiddenField) this.rptPrice.Items[index].FindControl("hidePriceId")).Value);
        int int32 = Convert.ToInt32(((HiddenField) this.rptPrice.Items[index].FindControl("hideGroupId")).Value);
        Decimal num2 = Convert.ToDecimal(((TextBox) this.rptPrice.Items[index].FindControl("txtGroupPrice")).Text.Trim());
        userGroupPriceList.Add(new user_group_price()
        {
          id = num1,
          article_id = _id,
          group_id = int32,
          price = num2
        });
      }
      model.group_price = userGroupPriceList;
      if (article.Update(model))
      {
        this.AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道内容:" + model.title);
        flag = true;
      }
      return flag;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (this.action == DTEnums.ActionEnum.Edit.ToString())
      {
        this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString());
        if (!this.DoEdit(this.id))
          this.JscriptMsg("保存过程中发生错误啦！", string.Empty);
        else
          this.JscriptMsg("修改信息成功！", "article_list.aspx?channel_id=" + (object) this.channel_id);
      }
      else
      {
        this.ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Add.ToString());
        if (!this.DoAdd())
          this.JscriptMsg("保存过程中发生错误！", string.Empty);
        else
          this.JscriptMsg("添加信息成功！", "article_list.aspx?channel_id=" + (object) this.channel_id);
      }
    }
  }
}
