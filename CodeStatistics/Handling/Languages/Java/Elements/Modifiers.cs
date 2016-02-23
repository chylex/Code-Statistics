using System;
using CodeStatistics.Collections;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    [Flags]
    enum Modifiers{
        Public = 0x1,
        Protected = 0x2,
        Private = 0x4,
        Abstract = 0x8,
        Final = 0x10,
        Static = 0x20,
        Synchronized = 0x40,
        Transient = 0x80,
        Volatile = 0x100,
        Native = 0x200,
        Strictfp = 0x400
    }

    static class JavaModifiers{
        private static readonly BiDictionary<Modifiers,string> ModifierDict = new BiDictionary<Modifiers,string>{
            { Modifiers.Public, "public" },
            { Modifiers.Protected, "protected" },
            { Modifiers.Private, "private" },
            { Modifiers.Abstract, "abstract" },
            { Modifiers.Final, "final" },
            { Modifiers.Static, "static" },
            { Modifiers.Synchronized, "synchronized" },
            { Modifiers.Transient, "transient" },
            { Modifiers.Volatile, "volatile" },
            { Modifiers.Native, "native" },
            { Modifiers.Strictfp, "strictfp" }
        };

        public static Modifiers FromString(string str){
            return ModifierDict.GetKey(str);
        }

        public static string ToString(Modifiers modifier){
            return ModifierDict.GetValue(modifier);
        }
    }
}
