using System.Collections.Generic;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Type : Member{
        public enum DeclarationType{
            Class, Interface, Enum, Annotation
        }

        public readonly DeclarationType Declaration;
        public readonly string Identifier;
        public readonly List<Type> NestedTypes = new List<Type>(1);

        public Type(DeclarationType type, string identifier, Member info) : base(info){
            this.Declaration = type;
            this.Identifier = identifier;
        }
    }
}
