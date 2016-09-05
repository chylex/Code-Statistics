using System.Collections.Generic;
using System.ComponentModel;

namespace CodeStatisticsCore.Collections{
    public static class AnonymousDictionary{
        public delegate string ToStringFunction(object obj);

        public static Dictionary<string, TValue> Create<TValue>(object obj){
            var dict = new Dictionary<string, TValue>();

            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)){
                dict.Add(descriptor.Name, (TValue)descriptor.GetValue(obj));
            }

            return dict;
        }

        public static Dictionary<string, string> Create(object obj){
            return Create(obj, o => o.ToString());
        }

        public static Dictionary<string, string> Create(object obj, ToStringFunction toString){
            var dict = new Dictionary<string, string>();

            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)){
                dict.Add(descriptor.Name, toString(descriptor.GetValue(obj)));
            }

            return dict;
        }
    }
}
