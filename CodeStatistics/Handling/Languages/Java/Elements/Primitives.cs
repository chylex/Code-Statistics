using System.Collections.Generic;
using CodeStatistics.Collections;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public enum Primitives{
        Boolean,
        Byte,
        Short,
        Int,
        Long,
        Char,
        Float,
        Double
    }

    public static class JavaPrimitives{
        private static readonly BiDictionary<Primitives,string> PrimitiveDict = new BiDictionary<Primitives,string>{
            { Primitives.Boolean, "boolean" },
            { Primitives.Byte, "byte" },
            { Primitives.Short, "short" },
            { Primitives.Int, "int" },
            { Primitives.Long, "long" },
            { Primitives.Char, "char" },
            { Primitives.Float, "float" },
            { Primitives.Double, "double" }
        };

        public static IEnumerable<Primitives> Values { get { return PrimitiveDict.Keys; } }
        public static IEnumerable<string> Strings { get { return PrimitiveDict.Values; } }

        public static Primitives FromString(string str){
            return PrimitiveDict.GetKey(str);
        }

        public static string ToString(Primitives primitive){
            return PrimitiveDict.GetValue(primitive);
        }
    }
}
