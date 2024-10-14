using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketIOWSClient
{
    public class MessageData
    {
        public string? Source;
        public string[]? Values;

        public object? GetValue<T>(int index = 0)
        {
            if (Values?[index] == null)
            {
                return null;
            }

            if (typeof (T) == typeof(string))
            {
                return Values[index];
            }

            return JsonSerializer.Deserialize<T>(Values[index]);
        }
    }
}
