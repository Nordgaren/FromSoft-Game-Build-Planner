using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace FromSoft_Game_Build_Planner
{
    static class ExtensionMethods
    {

        public static int GetIndexByProperty<T>(this ItemCollection source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");
            var index = 0;
            foreach (T item in source)
            {
                if (predicate(item)) return index;

                index++;
            }
            return 0;
        }

        public static T DeepCopy<T>(T other)
        {
            var json = JsonConvert.SerializeObject(other);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
