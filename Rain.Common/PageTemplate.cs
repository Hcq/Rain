using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Rain.Common
{
  public abstract class PageTemplate
  {
    public static Regex[] r = new Regex[23];

    static PageTemplate()
    {
      RegexOptions options = RegexOptions.None;
      PageTemplate.r[0] = new Regex("<%template ((skin=\\\\\"([^\\[\\]\\{\\}\\s]+)\\\\\"(?:\\s+))?)src=(?:\\/|\\\\\")([^\\[\\]\\{\\}\\s]+)(?:\\/|\\\\\")(?:\\s*)%>", options);
      PageTemplate.r[1] = new Regex("<%templateskin((=(?:\\\\\")([^\\[\\]\\{\\}\\s]+)(?:\\\\\"))?)(?:\\s*)%>", options);
      PageTemplate.r[2] = new Regex("<%namespace (?:\"?)([\\s\\S]+?)(?:\"?)%>", options);
      PageTemplate.r[3] = new Regex("<%csharp%>([\\s\\S]+?)<%/csharp%>", options);
      PageTemplate.r[4] = new Regex("<%loop ((\\(([^\\[\\]\\{\\}\\s]+)\\) )?)([^\\[\\]\\{\\}\\s]+) ([^\\[\\]\\{\\}\\s]+)%>", options);
      PageTemplate.r[5] = new Regex("<%foreach(?:\\s*)\\(([^\\[\\]\\{\\}\\s]+) ([^\\[\\]\\{\\}\\s]+) in ([^\\[\\]\\{\\}\\s]+)\\)(?:\\s*)%>", options);
      PageTemplate.r[6] = new Regex("<%for\\(([^\\(\\)\\[\\]\\{\\}]+)\\)(?:\\s*)%>", options);
      PageTemplate.r[7] = new Regex("<%if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?)(?:\\s*)%>", options);
      PageTemplate.r[8] = new Regex("<%else(( (?:\\s*)if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))*))?)(?:\\s*)%>", options);
      PageTemplate.r[9] = new Regex("<%if\\((([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?)\\)(?:\\s*)%>", options);
      PageTemplate.r[10] = new Regex("<%else(( (?:\\s*)if\\((([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?))?\\))(?:\\s*)%>", options);
      PageTemplate.r[11] = new Regex("<%\\/(?:loop|foreach|for|if)(?:\\s*)%>", options);
      PageTemplate.r[12] = new Regex("<%continue(?:\\s*)%>");
      PageTemplate.r[13] = new Regex("<%break(?:\\s*)%>");
      PageTemplate.r[14] = new Regex("(\\{request\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", options);
      PageTemplate.r[15] = new Regex("(<%cutstring\\(([^\\s]+?),(.\\d*?)\\)%>)", options);
      PageTemplate.r[16] = new Regex("(<%linkurl\\(([^\\s]*?)\\)%>)", options);
      PageTemplate.r[17] = new Regex("<%set ((\\(?([\\w\\.<>]+)(?:\\)| ))?)(?:\\s*)\\{?([^\\s\\{\\}]+)\\}?(?:\\s*)=(?:\\s*)(.*?)(?:\\s*)%>", options);
      PageTemplate.r[18] = new Regex("(\\{([^\\[\\]\\{\\}\\s]+)\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", options);
      PageTemplate.r[19] = new Regex("({([^\\[\\]/\\{\\}=:'\\s]+)})", options);
      PageTemplate.r[20] = new Regex("(<%datetostr\\(([^\\s]+?),(.*?)\\)%>)", options);
      PageTemplate.r[21] = new Regex("(\\{strtoint\\(([^\\s]+?)\\)\\})", options);
      PageTemplate.r[22] = new Regex("<%(?:write |=)(?:\\s*)(.*?)(?:\\s*)%>", options);
    }

    public static string GetTemplate(
      string sitePath,
      string tempPath,
      string skinName,
      string templet,
      string fromPage,
      string inherit,
      string buildPath,
      string channelName,
      string pageSize,
      int nest)
    {
      StringBuilder stringBuilder1 = new StringBuilder(220000);
      string mapPath1 = Utils.GetMapPath(string.Format("{0}{1}/{2}/{3}", (object) sitePath, (object) tempPath, (object) skinName, (object) templet));
      if (nest < 1)
        nest = 1;
      else if (nest > 5)
        return "";
      if (!File.Exists(mapPath1))
        return "";
      using (StreamReader streamReader = new StreamReader(mapPath1, Encoding.UTF8))
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        StringBuilder stringBuilder3 = new StringBuilder(70000);
        stringBuilder3.Append(streamReader.ReadToEnd());
        streamReader.Close();
        foreach (Match match in PageTemplate.r[3].Matches(stringBuilder3.ToString()))
          stringBuilder3.Replace(match.Groups[0].ToString(), match.Groups[0].ToString().Replace("\r\n", "\r\t\r"));
        foreach (Match match in PageTemplate.r[2].Matches(stringBuilder3.ToString()))
        {
          stringBuilder2.Append("\r\n<%@ Import namespace=\"" + (object) match.Groups[1] + "\" %>");
          stringBuilder3.Replace(match.Groups[0].ToString(), string.Empty);
        }
        stringBuilder3.Replace("\r\n", "\r\r\r");
        stringBuilder3.Replace("<%", "\r\r\n<%");
        stringBuilder3.Replace("%>", "%>\r\r\n");
        stringBuilder3.Replace("<%csharp%>\r\r\n", "<%csharp%>").Replace("\r\r\n<%/csharp%>", "<%/csharp%>");
        string[] strArray = Utils.SplitString(stringBuilder3.ToString(), "\r\r\n");
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (!(strArray[index] == ""))
            stringBuilder1.Append(PageTemplate.ConvertTags(nest, channelName, pageSize, sitePath, tempPath, skinName, strArray[index]));
        }
        if (nest == 1)
        {
          StringBuilder stringBuilder4 = new StringBuilder();
          if (channelName != string.Empty)
            stringBuilder4.Append("\r\n\tconst string channel = \"" + channelName + "\";");
          if (pageSize != string.Empty && Utils.StrToInt(pageSize, 0) > 0)
            stringBuilder4.Append("\r\n\tconst int pagesize = " + pageSize + ";");
          string contents = string.Format("<%@ Page Language=\"C#\" AutoEventWireup=\"true\" Inherits=\"{0}\" ValidateRequest=\"false\" %>\r\n<%@ Import namespace=\"System.Collections.Generic\" %>\r\n<%@ Import namespace=\"System.Text\" %>\r\n<%@ Import namespace=\"System.Data\" %>\r\n<%@ Import namespace=\"UScms.Common\" %>{1}\r\n\r\n<script runat=\"server\">\r\noverride protected void OnInit(EventArgs e)\r\n{{\r\n\r\n\t/* \r\n\t\tThis page was created by UScms Template Engine at {2}.\r\n\t\t本页面代码由UScms模板引擎生成于 {2}. \r\n\t*/\r\n\r\n\tbase.OnInit(e);\r\n\tStringBuilder templateBuilder = new StringBuilder({3});{4}\r\n{5}\r\n\tResponse.Write(templateBuilder.ToString());\r\n}}\r\n</script>\r\n", (object) inherit, (object) stringBuilder2.ToString(), (object) DateTime.Now, (object) stringBuilder1.Capacity, (object) stringBuilder4.ToString(), (object) Regex.Replace(stringBuilder1.ToString(), "\\r\\n\\s*templateBuilder\\.Append\\(\"\"\\);", ""));
          string mapPath2 = Utils.GetMapPath(string.Format("{0}aspx/{1}/", (object) sitePath, (object) buildPath));
          string path = mapPath2 + fromPage;
          if (!Directory.Exists(mapPath2))
            Directory.CreateDirectory(mapPath2);
          File.WriteAllText(path, contents, Encoding.UTF8);
        }
      }
      return stringBuilder1.ToString();
    }

    private static string ConvertTags(
      int nest,
      string channelName,
      string pageSize,
      string sitePath,
      string tempPath,
      string skinName,
      string inputStr)
    {
      string str1 = "";
      string empty = string.Empty;
      string input = inputStr.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("</script>", "</\");\r\n\ttemplateBuilder.Append(\"script>");
      bool flag = false;
      foreach (Match match in PageTemplate.r[0].Matches(input))
      {
        flag = true;
        input = !(match.Groups[3].ToString() != string.Empty) ? input.Replace(match.Groups[0].ToString(), "\r\n" + PageTemplate.GetTemplate(sitePath, tempPath, skinName, match.Groups[4].ToString(), string.Empty, string.Empty, string.Empty, channelName, pageSize, nest + 1) + "\r\n") : input.Replace(match.Groups[0].ToString(), "\r\n" + PageTemplate.GetTemplate(sitePath, "templates", match.Groups[3].ToString(), match.Groups[4].ToString(), string.Empty, string.Empty, string.Empty, channelName, pageSize, nest + 1) + "\r\n");
      }
      foreach (Match match in PageTemplate.r[1].Matches(input))
        input = !(match.Groups[3].ToString() != string.Empty) ? input.Replace(match.Groups[0].ToString(), string.Format("{0}{1}/{2}", (object) sitePath, (object) tempPath, (object) skinName)) : input.Replace(match.Groups[0].ToString(), string.Format("{0}{1}/{2}", (object) sitePath, (object) "templates", (object) match.Groups[3].ToString()));
      foreach (Match match in PageTemplate.r[3].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), match.Groups[1].ToString().Replace("\r\t\r", "\r\n\t").Replace("\\\"", "\""));
      }
      foreach (Match match in PageTemplate.r[4].Matches(input))
      {
        flag = true;
        input = !(match.Groups[3].ToString() == "") ? input.Replace(match.Groups[0].ToString(), string.Format("\r\n\tint {1}__loop__id=0;\r\n\tforeach({0} {1} in {2})\r\n\t{{\r\n\t\t{1}__loop__id++;\r\n", (object) match.Groups[3], (object) match.Groups[4], (object) match.Groups[5])) : input.Replace(match.Groups[0].ToString(), string.Format("\r\n\tint {0}__loop__id=0;\r\n\tforeach(DataRow {0} in {1}.Rows)\r\n\t{{\r\n\t\t{0}__loop__id++;\r\n", (object) match.Groups[4], (object) match.Groups[5]));
      }
      foreach (Match match in PageTemplate.r[5].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\tforeach({0} {1} in {2})\r\n\t{{", (object) match.Groups[1], (object) match.Groups[2], (object) match.Groups[3]));
      }
      foreach (Match match in PageTemplate.r[6].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\tfor({0})\r\n\t{{", (object) match.Groups[1]));
      }
      foreach (Match match in PageTemplate.r[7].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "\r\n\tif (" + match.Groups[1].ToString().Replace("\\\"", "\"") + ")\r\n\t{");
      }
      foreach (Match match in PageTemplate.r[8].Matches(input))
      {
        flag = true;
        input = !(match.Groups[1].ToString() == string.Empty) ? input.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse if (" + match.Groups[3].ToString().Replace("\\\"", "\"") + ")\r\n\t{") : input.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse\r\n\t{");
      }
      foreach (Match match in PageTemplate.r[9].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "\r\n\tif (" + match.Groups[1].ToString().Replace("\\\"", "\"") + ")\r\n\t{");
      }
      foreach (Match match in PageTemplate.r[10].Matches(input))
      {
        flag = true;
        input = !(match.Groups[1].ToString() == string.Empty) ? input.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse if (" + match.Groups[3].ToString().Replace("\\\"", "\"") + ")\r\n\t{") : input.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse\r\n\t{");
      }
      foreach (Match match in PageTemplate.r[11].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "\r\n\t}\t//end for if");
      }
      foreach (Match match in PageTemplate.r[12].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "\tcontinue;\r\n");
      }
      foreach (Match match in PageTemplate.r[13].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "\tbreak;\r\n");
      }
      foreach (Match match in PageTemplate.r[15].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\ttemplateBuilder.Append(Utils.DropHTML({0},{1}));", (object) match.Groups[2], (object) match.Groups[3].ToString().Trim()));
      }
      foreach (Match match in PageTemplate.r[20].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\ttemplateBuilder.Append(Utils.ObjectToDateTime({0}).ToString(\"{1}\"));", (object) match.Groups[2], (object) match.Groups[3].ToString().Replace("\\\"", string.Empty)));
      }
      foreach (Match match in PageTemplate.r[21].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), "Utils.StrToInt(" + (object) match.Groups[2] + ", 0)");
      }
      foreach (Match match in PageTemplate.r[16].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\ttemplateBuilder.Append(linkurl({0}));", (object) match.Groups[2]).Replace("\\\"", "\""));
      }
      foreach (Match match in PageTemplate.r[17].Matches(input))
      {
        flag = true;
        string str2 = "";
        if (match.Groups[3].ToString() != string.Empty)
          str2 = match.Groups[3].ToString();
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\t{0} {1} = {2};", (object) str2, (object) match.Groups[4], (object) match.Groups[5]).Replace("\\\"", "\""));
      }
      foreach (Match match in PageTemplate.r[14].Matches(input))
        input = !flag ? input.Replace(match.Groups[0].ToString(), string.Format("\" + DTRequest.GetString(\"{0}\") + \"", (object) match.Groups[2])) : input.Replace(match.Groups[0].ToString(), "DTRequest.GetString(\"" + (object) match.Groups[2] + "\")");
      foreach (Match match in PageTemplate.r[22].Matches(input))
      {
        flag = true;
        input = input.Replace(match.Groups[0].ToString(), string.Format("\r\n\ttemplateBuilder.Append({0}{1}.ToString());", (object) match.Groups[1], (object) match.Groups[2]).Replace("\\\"", "\""));
      }
      foreach (Match match in PageTemplate.r[18].Matches(input))
      {
        if (flag)
        {
          if (Utils.IsNumeric(match.Groups[3].ToString()))
            input = input.Replace(match.Groups[0].ToString(), "Utils.ObjectToStr(" + (object) match.Groups[2] + "[" + (object) match.Groups[3] + "])");
          else if (match.Groups[3].ToString() == "_id")
            input = input.Replace(match.Groups[0].ToString(), match.Groups[2].ToString() + "__loop__id");
          else
            input = input.Replace(match.Groups[0].ToString(), "Utils.ObjectToStr(" + (object) match.Groups[2] + "[\"" + (object) match.Groups[3] + "\"])");
        }
        else
          input = !Utils.IsNumeric(match.Groups[3].ToString()) ? (!(match.Groups[3].ToString() == "_id") ? input.Replace(match.Groups[0].ToString(), string.Format("\" + Utils.ObjectToStr({0}[\"{1}\"]) + \"", (object) match.Groups[2], (object) match.Groups[3])) : input.Replace(match.Groups[0].ToString(), string.Format("\" + {0}__loop__id.ToString() + \"", (object) match.Groups[2]))) : input.Replace(match.Groups[0].ToString(), string.Format("\" + Utils.ObjectToStr({0}[{1}]) + \"", (object) match.Groups[2], (object) match.Groups[3]));
      }
      foreach (Match match in PageTemplate.r[19].Matches(input))
        input = !flag ? input.Replace(match.Groups[0].ToString(), string.Format("\");\r\n\ttemplateBuilder.Append(Utils.ObjectToStr({0}));\r\n\ttemplateBuilder.Append(\"", (object) match.Groups[2].ToString().Trim())) : input.Replace(match.Groups[0].ToString(), match.Groups[2].ToString());
      if (flag)
        str1 = input + "\r\n";
      else if (input.Trim() != "")
        str1 = ("\r\n\ttemplateBuilder.Append(\"" + input.Replace("\r\r\r", "\\r\\n") + "\");").Replace("\\r\\n<?xml", "<?xml").Replace("\\r\\n<!DOCTYPE", "<!DOCTYPE");
      return str1;
    }
  }
}
