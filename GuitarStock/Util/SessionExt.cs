using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Util
{
    public static class SessionExt
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var options = new JsonSerializerOptions { IncludeFields = true };
            string sVal =  JsonSerializer.Serialize(value, options);
            session.SetString(key, sVal);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }

    }
}
