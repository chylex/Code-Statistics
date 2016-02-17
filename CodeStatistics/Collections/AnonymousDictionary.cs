using System.Collections.Generic;
using System.ComponentModel;

namespace CodeStatistics.Collections{
    static class AnonymousDictionary{
        public static Dictionary<string,TValue> Create<TValue>(object obj){
            var dict = new Dictionary<string,TValue>();

            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)){
                dict.Add(descriptor.Name,(TValue)descriptor.GetValue(obj));
            }

            return dict;
        }

        public static Dictionary<string,string> Create(object obj){
            var dict = new Dictionary<string,string>();

            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)){
                dict.Add(descriptor.Name,descriptor.GetValue(obj).ToString());
            }

            return dict;
        }
    }
}
