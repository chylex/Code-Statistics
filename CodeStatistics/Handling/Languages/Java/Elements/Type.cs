using System.Collections.Generic;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Type : Member{
        public enum DeclarationType{
            Class, Interface, Enum, Annotation
        }

        public readonly DeclarationType Declaration;
        public readonly string Identifier;
        public readonly List<Type> NestedTypes = new List<Type>(1);

        private readonly TypeData data;

        public Type(DeclarationType type, string identifier, Member info) : base(info){
            this.Declaration = type;
            this.Identifier = identifier;

            switch(type){
                case DeclarationType.Enum: data = new DataEnum(); break;
                default: data = new TypeData(); break;
            }
        }

        public TypeData GetData(){
            return data;
        }

        public T GetData<T>() where T : TypeData{
            return data as T;
        }

        public class TypeData{
            public readonly List<Method> Methods = new List<Method>(8);
            public readonly List<Field> Fields = new List<Field>(8);
        }

        public class DataEnum : TypeData{
            // TODO values
        }
    }
}
