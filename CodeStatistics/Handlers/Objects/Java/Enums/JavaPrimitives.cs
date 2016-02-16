using System;

namespace CodeStatistics.Handlers.Objects.Java.Enums{
    public enum JavaPrimitives{
        Invalid,
        Boolean,
        Byte,
        Short,
        Char,
        Int,
        Float,
        Long,
        Double
    }

    static class JavaPrimitivesFunc{
        public static readonly JavaPrimitives[] Values = new JavaPrimitives[]{
            JavaPrimitives.Boolean, JavaPrimitives.Byte, JavaPrimitives.Short, JavaPrimitives.Char, JavaPrimitives.Int, JavaPrimitives.Float, JavaPrimitives.Long, JavaPrimitives.Double
        };

        public static readonly string[] Strings = new string[]{ "boolean", "byte", "short", "char", "int", "float", "long", "double" };

        public static JavaPrimitives FromString(string primitive){
            return Values[Array.FindIndex(Strings,str => str.Equals(primitive))];
        }

        public static string ToString(JavaPrimitives primitive){
            return Strings[Array.FindIndex(Values,ele => ele == primitive)];
        }
    }
}
