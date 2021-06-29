using MeetingManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingManager.Core.Cache
{
    public class InMemotyCache<T> : IMemoryCache<T>
    {
        private IDictionary<object, T> memoryCache = new Dictionary<object, T>();

        public bool TryGetValue(object key, out T value)
        {
            return memoryCache.TryGetValue(key, out value);
        }

        public void Set(object key, T value)
        {
            if(memoryCache.ContainsKey(key))
            {
                memoryCache.Remove(key);
            }
            memoryCache.Add(key, value);
        }
    }
}
