using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace Rain.Common
{
  public class JsonHelper
  {
    public static string ObjectToJSON(object obj)
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      try
      {
        return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(scriptSerializer.Serialize(obj)));
      }
      catch (Exception ex)
      {
        throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
      }
    }

    public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
    {
      List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        foreach (DataColumn column in (InternalDataCollectionBase) dt.Columns)
          dictionary.Add(column.ColumnName, row[column.ColumnName]);
        dictionaryList.Add(dictionary);
      }
      return dictionaryList;
    }

    public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(
      DataSet ds)
    {
      Dictionary<string, List<Dictionary<string, object>>> dictionary = new Dictionary<string, List<Dictionary<string, object>>>();
      foreach (DataTable table in (InternalDataCollectionBase) ds.Tables)
        dictionary.Add(table.TableName, JsonHelper.DataTableToList(table));
      return dictionary;
    }

    public static string DataTableToJSON(DataTable dt)
    {
      return JsonHelper.ObjectToJSON((object) JsonHelper.DataTableToList(dt));
    }

    public static T JSONToObject<T>(string jsonText)
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      try
      {
        return scriptSerializer.Deserialize<T>(jsonText);
      }
      catch (Exception ex)
      {
        throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
      }
    }

    public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(
      string jsonText)
    {
      return JsonHelper.JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
    }

    public static Dictionary<string, object> DataRowFromJSON(string jsonText)
    {
      return JsonHelper.JSONToObject<Dictionary<string, object>>(jsonText);
    }
  }
}
