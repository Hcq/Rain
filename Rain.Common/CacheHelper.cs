using System;
using System.Web;
using System.Web.Caching;

namespace Rain.Common
{
  public class CacheHelper
  {
    public static void Insert(string key, object obj)
    {
      HttpContext.Current.Cache.Insert(key, obj);
    }

    public static void Remove(string key)
    {
      HttpContext.Current.Cache.Remove(key);
    }

    public static void Insert(string key, object obj, string fileName)
    {
      CacheDependency dependencies = new CacheDependency(fileName);
      HttpContext.Current.Cache.Insert(key, obj, dependencies);
    }

    public static void Insert(string key, object obj, int expires)
    {
      HttpContext.Current.Cache.Insert(key, obj, (CacheDependency) null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
    }

    public static object Get(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (object) null;
      return HttpContext.Current.Cache.Get(key);
    }

    public static T Get<T>(string key)
    {
      object obj = CacheHelper.Get(key);
      return obj == null ? default (T) : (T) obj;
    }
  }
}
