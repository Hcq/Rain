using System;
using System.IO;
using System.Xml.Serialization;

namespace Rain.Common
{
  public class SerializationHelper
  {
    public static object Load(Type type, string filename)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return new XmlSerializer(type).Deserialize((Stream) fileStream);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        fileStream?.Close();
      }
    }

    public static void Save(object obj, string filename)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        new XmlSerializer(obj.GetType()).Serialize((Stream) fileStream, obj);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        fileStream?.Close();
      }
    }
  }
}
