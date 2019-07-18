// Decompiled with JetBrains decompiler
// Type: Rain.BLL.article_attribute_field
// Assembly: Rain.BLL, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8924FCAE-37FA-4301-A188-DA020D33C8EB
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.BLL.dll

using System.Collections.Generic;
using System.Data;

namespace Rain.BLL
{
  public class article_attribute_field
  {
    private readonly Rain.Model.siteconfig siteConfig = new siteconfig().loadConfig();
    private readonly Rain.DAL.article_attribute_field dal;

    public article_attribute_field()
    {
      this.dal = new Rain.DAL.article_attribute_field(this.siteConfig.sysdatabaseprefix);
    }

    public bool Exists(int id)
    {
      return this.dal.Exists(id);
    }

    public bool Exists(string column_name)
    {
      return this.dal.Exists(column_name);
    }

    public int Add(Rain.Model.article_attribute_field model)
    {
      switch (model.control_type)
      {
        case "single-text":
        case "multi-text":
        case "multi-checkbox":
          if (model.data_length > 0 && model.data_length <= (int) byte.MaxValue)
          {
            model.data_type = "text(" + (object) model.data_length + ")";
            break;
          }
          if (model.data_length > (int) byte.MaxValue)
          {
            model.data_type = "memo";
            break;
          }
          model.data_length = 50;
          model.data_type = "text";
          break;
        case "editor":
          model.data_type = "memo";
          break;
        case "images":
          model.data_type = "text(255)";
          break;
        case "video":
          model.data_type = "text(255)";
          break;
        case "number":
          model.data_type = model.data_place <= 0 ? "integer" : "decimal(9," + (object) model.data_place + ")";
          break;
        case "checkbox":
          model.data_type = "integer";
          break;
        case "multi-radio":
          if (model.data_type == "int")
          {
            model.data_length = 4;
            model.data_type = "integer";
            break;
          }
          if (model.data_length > 0 && model.data_length <= (int) byte.MaxValue)
            model.data_type = "text(" + (object) model.data_length + ")";
          else if (model.data_length > (int) byte.MaxValue)
          {
            model.data_type = "memo";
          }
          else
          {
            model.data_length = 50;
            model.data_type = "text";
          }
          break;
      }
      return this.dal.Add(model);
    }

    public void UpdateField(int id, string strValue)
    {
      this.dal.UpdateField(id, strValue);
    }

    public bool Update(Rain.Model.article_attribute_field model)
    {
      switch (model.control_type)
      {
        case "single-text":
        case "multi-text":
        case "multi-checkbox":
          if (model.data_length > 0 && model.data_length <= (int) byte.MaxValue)
          {
            model.data_type = "text(" + (object) model.data_length + ")";
            break;
          }
          if (model.data_length > (int) byte.MaxValue)
          {
            model.data_type = "memo";
            break;
          }
          model.data_length = 50;
          model.data_type = "text";
          break;
        case "editor":
          model.data_type = "memo";
          break;
        case "images":
          model.data_type = "text(255)";
          break;
        case "video":
          model.data_type = "text(255)";
          break;
        case "number":
          model.data_type = model.data_place <= 0 ? "integer" : "decimal(9," + (object) model.data_place + ")";
          break;
        case "checkbox":
          model.data_type = "integer";
          break;
        case "multi-radio":
          if (model.data_type == "int")
          {
            model.data_length = 4;
            model.data_type = "integer";
            break;
          }
          if (model.data_length > 0 && model.data_length <= (int) byte.MaxValue)
            model.data_type = "text(" + (object) model.data_length + ")";
          else if (model.data_length > (int) byte.MaxValue)
          {
            model.data_type = "memo";
          }
          else
          {
            model.data_length = 50;
            model.data_type = "text";
          }
          break;
      }
      return this.dal.Update(model);
    }

    public bool Delete(int id)
    {
      return this.dal.Delete(id);
    }

    public Rain.Model.article_attribute_field GetModel(int id)
    {
      return this.dal.GetModel(id);
    }

    public List<Rain.Model.article_attribute_field> GetModelList(
      int channel_id,
      string strWhere)
    {
      return this.DataTableToList(this.dal.GetList(channel_id, strWhere).Tables[0]);
    }

    public List<Rain.Model.article_attribute_field> DataTableToList(
      DataTable dt)
    {
      List<Rain.Model.article_attribute_field> articleAttributeFieldList = new List<Rain.Model.article_attribute_field>();
      int count = dt.Rows.Count;
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
        {
          Rain.Model.article_attribute_field articleAttributeField = new Rain.Model.article_attribute_field();
          if (dt.Rows[index]["id"].ToString() != "")
            articleAttributeField.id = int.Parse(dt.Rows[index]["id"].ToString());
          articleAttributeField.name = dt.Rows[index]["name"].ToString();
          articleAttributeField.title = dt.Rows[index]["title"].ToString();
          articleAttributeField.control_type = dt.Rows[index]["control_type"].ToString();
          articleAttributeField.data_type = dt.Rows[index]["data_type"].ToString();
          if (dt.Rows[index]["data_length"].ToString() != "")
            articleAttributeField.data_length = int.Parse(dt.Rows[index]["data_length"].ToString());
          if (dt.Rows[index]["data_place"].ToString() != "")
            articleAttributeField.data_place = int.Parse(dt.Rows[index]["data_place"].ToString());
          articleAttributeField.item_option = dt.Rows[index]["item_option"].ToString();
          articleAttributeField.default_value = dt.Rows[index]["default_value"].ToString();
          if (dt.Rows[index]["is_required"].ToString() != "")
            articleAttributeField.is_required = int.Parse(dt.Rows[index]["is_required"].ToString());
          if (dt.Rows[index]["is_password"].ToString() != "")
            articleAttributeField.is_password = int.Parse(dt.Rows[index]["is_password"].ToString());
          if (dt.Rows[index]["is_html"].ToString() != "")
            articleAttributeField.is_html = int.Parse(dt.Rows[index]["is_html"].ToString());
          if (dt.Rows[index]["editor_type"].ToString() != "")
            articleAttributeField.editor_type = int.Parse(dt.Rows[index]["editor_type"].ToString());
          articleAttributeField.valid_tip_msg = dt.Rows[index]["valid_tip_msg"].ToString();
          articleAttributeField.valid_error_msg = dt.Rows[index]["valid_error_msg"].ToString();
          articleAttributeField.valid_pattern = dt.Rows[index]["valid_pattern"].ToString();
          if (dt.Rows[index]["sort_id"].ToString() != "")
            articleAttributeField.sort_id = int.Parse(dt.Rows[index]["sort_id"].ToString());
          if (dt.Rows[index]["is_sys"].ToString() != "")
            articleAttributeField.is_sys = int.Parse(dt.Rows[index]["is_sys"].ToString());
          articleAttributeFieldList.Add(articleAttributeField);
        }
      }
      return articleAttributeFieldList;
    }

    public DataSet GetList(int channel_id, string strWhere)
    {
      return this.dal.GetList(channel_id, strWhere);
    }

    public DataSet GetList(int Top, string strWhere, string filedOrder)
    {
      return this.dal.GetList(Top, strWhere, filedOrder);
    }

    public DataSet GetList(
      int pageSize,
      int pageIndex,
      string strWhere,
      string filedOrder,
      out int recordCount)
    {
      return this.dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
    }
  }
}
