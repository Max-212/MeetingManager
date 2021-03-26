using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingManager.Core.Interfaces
{
    public interface IMemoryCache<T>
    {
        bool TryGetValue(object key, out T value);

        void Set(object key, T value);
    }
}
