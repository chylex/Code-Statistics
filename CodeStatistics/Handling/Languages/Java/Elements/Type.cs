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
                case DeclarationType.Interface:
                case DeclarationType.Annotation: data = new DataInterface(); break;
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

            public virtual bool CanHaveConstructors { get { return true; } }

            public virtual Member UpdateFieldInfo(Member info){
                return info;
            }

            public virtual Member UpdateMethodInfo(Member info){
                return info;
            }
        }

        public class DataEnum : TypeData{
            public readonly List<string> EnumValues = new List<string>(4);
        }

        public class DataInterface : TypeData{
            public override bool CanHaveConstructors { get { return false; } }

            public override Member UpdateFieldInfo(Member info){
                return new Member(info,(info.Modifiers | Modifiers.Public | Modifiers.Static | Modifiers.Final) & ~(Modifiers.Abstract | Modifiers.Protected | Modifiers.Private));
            }

            public override Member UpdateMethodInfo(Member info){
                Modifiers newModifiers = (info.Modifiers | Modifiers.Public) & ~(Modifiers.Protected | Modifiers.Private);

                if (!(info.Modifiers.HasFlag(Modifiers.Static) || info.Modifiers.HasFlag(Modifiers.Default))){
                    newModifiers = (newModifiers | Modifiers.Abstract) & ~Modifiers.Final;
                }

                return new Member(info,newModifiers);
            }
        }
    }
}
