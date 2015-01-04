using System;
using Windows.Data.Json;

namespace Conflux.Connectivity.JsonExtensions
{
    public static class JsonExtensions
    {
        public static T GetNamedValue<T>(this JsonObject jsonObject, string name) 
        {
            if (typeof(T) == typeof(string))
            {
                return (T)GetString(jsonObject, name);
            }

            if (typeof (T) == typeof (double))
            {
                return (T)GetDouble(jsonObject, name);
            }

            if (typeof(T) == typeof(DateTime?))
            {
                return (T)GetDateTime(jsonObject, name);
            }

            if (typeof(T) == typeof(JsonObject))
            {
                return (T) GeJsontObject(jsonObject, name);
            }
            return default(T);
        }

        private static object GetDouble(JsonObject jsonObject, string name)
        {
            try
            {
                return jsonObject.GetNamedNumber(name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static object GetString(JsonObject jsonObject, string name)
        {
            try
            {
                return jsonObject.GetNamedString(name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static object GetDateTime(JsonObject jsonObject, string name)
        {
            try
            {
                return DateTime.Parse(jsonObject.GetNamedString(name));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static object GeJsontObject(JsonObject jsonObject, string name)
        {
            try
            {
                return jsonObject.GetNamedObject(name);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
