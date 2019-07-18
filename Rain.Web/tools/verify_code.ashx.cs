// Decompiled with JetBrains decompiler
// Type: Rain.Web.tools.verify_code
// Assembly: Rain.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C74D3EC-D223-4A13-9304-5BE7CB5803E5
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Web.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace Rain.Web.tools
{
  public class verify_code : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      int num1 = 80;
      int num2 = 22;
      int num3 = 16;
      string empty = string.Empty;
      Color[] colorArray = new Color[8]
      {
        Color.Black,
        Color.Red,
        Color.Blue,
        Color.Green,
        Color.Orange,
        Color.Brown,
        Color.Brown,
        Color.DarkBlue
      };
      string[] strArray = new string[5]
      {
        "Times New Roman",
        "Verdana",
        "Arial",
        "Gungsuh",
        "Impact"
      };
      char[] chArray = new char[39]
      {
        '2',
        '3',
        '4',
        '5',
        '6',
        '8',
        '9',
        'a',
        'b',
        'd',
        'e',
        'f',
        'h',
        'k',
        'm',
        'n',
        'r',
        'x',
        'y',
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'J',
        'K',
        'L',
        'M',
        'N',
        'P',
        'R',
        'S',
        'T',
        'W',
        'X',
        'Y'
      };
      Random random = new Random();
      for (int index = 0; index < 4; ++index)
        empty += (string) (object) chArray[random.Next(chArray.Length)];
      context.Session["dt_session_code"] = (object) empty;
      Bitmap bitmap = new Bitmap(num1, num2);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.Clear(Color.White);
      for (int index = 0; index < 1; ++index)
      {
        int x1 = random.Next(num1);
        int y1 = random.Next(num2);
        int x2 = random.Next(num1);
        int y2 = random.Next(num2);
        Color color = colorArray[random.Next(colorArray.Length)];
        graphics.DrawLine(new Pen(color), x1, y1, x2, y2);
      }
      for (int index = 0; index < empty.Length; ++index)
      {
        Font font = new Font(strArray[random.Next(strArray.Length)], (float) num3);
        Color color = colorArray[random.Next(colorArray.Length)];
        graphics.DrawString(empty[index].ToString(), font, (Brush) new SolidBrush(color), (float) ((double) index * 18.0 + 2.0), 0.0f);
      }
      for (int index = 0; index < 100; ++index)
      {
        int x = random.Next(bitmap.Width);
        int y = random.Next(bitmap.Height);
        Color color = colorArray[random.Next(colorArray.Length)];
        bitmap.SetPixel(x, y, color);
      }
      context.Response.Buffer = true;
      context.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(0.0);
      context.Response.Expires = 0;
      context.Response.CacheControl = "no-cache";
      context.Response.AppendHeader("Pragma", "No-Cache");
      MemoryStream memoryStream = new MemoryStream();
      try
      {
        bitmap.Save((Stream) memoryStream, ImageFormat.Png);
        context.Response.ClearContent();
        context.Response.ContentType = "image/Png";
        context.Response.BinaryWrite(memoryStream.ToArray());
      }
      finally
      {
        bitmap.Dispose();
        graphics.Dispose();
      }
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }
  }
}
