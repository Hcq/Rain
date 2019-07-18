// Decompiled with JetBrains decompiler
// Type: Rain.API.Payment.tenpaypc.MD5Util
// Assembly: Rain.API, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 114351D3-1A2F-4CC4-A6A6-43C59DB5A56B
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.API.dll

using System;
using System.Security.Cryptography;
using System.Text;

namespace Rain.API.Payment.tenpaypc
{
  public class MD5Util
  {
    public static string GetMD5(string encypStr, string charset)
    {
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      byte[] bytes;
      try
      {
        bytes = Encoding.GetEncoding(charset).GetBytes(encypStr);
      }
      catch (Exception ex)
      {
        bytes = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
      }
      return BitConverter.ToString(cryptoServiceProvider.ComputeHash(bytes)).Replace("-", "").ToUpper();
    }
  }
}
