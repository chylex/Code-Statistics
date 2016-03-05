using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Method : Member{
        public const string ConstructorIdentifier = "<explinit>"; // explicit constructor

        public readonly string Identifier;
        public readonly TypeOf ReturnType;
        public readonly ReadOnlyCollection<TypeOf> ParameterTypes;

        public bool IsConstructor { get { return Identifier == ConstructorIdentifier; } }

        public Method(string identifier, TypeOf returnType, List<TypeOf> parameterTypes, Member info) : base(info){
            this.Identifier = identifier;
            this.ReturnType = returnType;
            this.ParameterTypes = parameterTypes.AsReadOnly();
        }

        public Method(string identifier, TypeOf returnType, Member info) : base(info){
            this.Identifier = identifier;
            this.ReturnType = returnType;
            this.ParameterTypes = TypeOf.EmptyList();
        }

        public override string ToString(){
            return base.ToString()+ReturnType+' '+Identifier+'('+string.Join(", ",ParameterTypes)+')';
        }
    }
}
