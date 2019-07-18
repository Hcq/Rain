using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Rain.Common
{
  public class DESEncrypt
  {
    public static string Encrypt(string Text)
    {
      return DESEncrypt.Encrypt(Text, "UScms");
    }

    public static string Encrypt(string Text, string sKey)
    {
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      byte[] bytes = Encoding.Default.GetBytes(Text);
      cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
      cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(bytes, 0, bytes.Length);
      cryptoStream.FlushFinalBlock();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in memoryStream.ToArray())
        stringBuilder.AppendFormat("{0:X2}", (object) num);
      return stringBuilder.ToString();
    }

    public static string Decrypt(string Text)
    {
      return DESEncrypt.Decrypt(Text, "UScms");
    }

    public static string Decrypt(string Text, string sKey)
    {
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      int length = Text.Length / 2;
      byte[] buffer = new byte[length];
      for (int index = 0; index < length; ++index)
      {
        int int32 = Convert.ToInt32(Text.Substring(index * 2, 2), 16);
        buffer[index] = (byte) int32;
      }
      cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
      cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(buffer, 0, buffer.Length);
      cryptoStream.FlushFinalBlock();
      return Encoding.Default.GetString(memoryStream.ToArray());
    }
  }
}
