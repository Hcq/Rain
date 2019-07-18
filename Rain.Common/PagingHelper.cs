using System;
using System.Text;

namespace Rain.Common
{
  public static class PagingHelper
  {
    public static string CreatePagingSql(
      int _recordCount,
      int _pageSize,
      int _pageIndex,
      string _safeSql,
      string _orderField)
    {
      string[] strArray = _orderField.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      for (int index = 0; index < strArray.Length; ++index)
      {
        strArray[index] = strArray[index].Trim();
        if (index != 0)
        {
          stringBuilder1.Append(", ");
          stringBuilder2.Append(", ");
        }
        stringBuilder1.Append(strArray[index]);
        int startIndex = strArray[index].IndexOf(" ");
        if (startIndex > 0)
        {
          bool flag = strArray[index].IndexOf(" DESC", StringComparison.OrdinalIgnoreCase) != -1;
          stringBuilder2.AppendFormat("{0} {1}", (object) strArray[index].Remove(startIndex), flag ? (object) "ASC" : (object) "DESC");
        }
        else
          stringBuilder2.AppendFormat("{0} DESC", (object) strArray[index]);
      }
      _pageSize = _pageSize == 0 ? _recordCount : _pageSize;
      int num = (_recordCount + _pageSize - 1) / _pageSize;
      if (_pageIndex < 1)
        _pageIndex = 1;
      else if (_pageIndex > num)
        _pageIndex = num;
      StringBuilder stringBuilder3 = new StringBuilder();
      if (_pageIndex == 1)
      {
        stringBuilder3.AppendFormat(" SELECT TOP {0} * ", (object) _pageSize);
        stringBuilder3.AppendFormat(" FROM ({0}) AS T ", (object) _safeSql);
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder1.ToString());
      }
      else if (_pageIndex == num)
      {
        stringBuilder3.Append(" SELECT * FROM ");
        stringBuilder3.Append(" ( ");
        stringBuilder3.AppendFormat(" SELECT TOP {0} * ", (object) (_recordCount - _pageSize * (_pageIndex - 1)));
        stringBuilder3.AppendFormat(" FROM ({0}) AS T ", (object) _safeSql);
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder2.ToString());
        stringBuilder3.Append(" ) AS T ");
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder1.ToString());
      }
      else if (_pageIndex < num / 2 + num % 2)
      {
        stringBuilder3.Append(" SELECT * FROM ");
        stringBuilder3.Append(" ( ");
        stringBuilder3.AppendFormat(" SELECT TOP {0} * FROM ", (object) _pageSize);
        stringBuilder3.Append(" ( ");
        stringBuilder3.AppendFormat(" SELECT TOP {0} * ", (object) (_pageSize * _pageIndex));
        stringBuilder3.AppendFormat(" FROM ({0}) AS T ", (object) _safeSql);
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder1.ToString());
        stringBuilder3.Append(" ) AS T ");
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder2.ToString());
        stringBuilder3.Append(" ) AS T ");
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder1.ToString());
      }
      else
      {
        stringBuilder3.AppendFormat(" SELECT TOP {0} * FROM ", (object) _pageSize);
        stringBuilder3.Append(" ( ");
        stringBuilder3.AppendFormat(" SELECT TOP {0} * ", (object) (_recordCount - (_pageIndex - 1) * _pageSize));
        stringBuilder3.AppendFormat(" FROM ({0}) AS T ", (object) _safeSql);
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder2.ToString());
        stringBuilder3.Append(" ) AS T ");
        stringBuilder3.AppendFormat(" ORDER BY {0} ", (object) stringBuilder1.ToString());
      }
      return stringBuilder3.ToString();
    }

    public static string CreateTopnSql(int _n, string _safeSql)
    {
      return string.Format(" SELECT TOP {0} * FROM ({1}) AS T ", (object) _n, (object) _safeSql);
    }

    public static string CreateCountingSql(string _safeSql)
    {
      return string.Format(" SELECT COUNT(1) AS RecordCount FROM ({0}) AS T ", (object) _safeSql);
    }
  }
}
