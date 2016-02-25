using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Method : Member{
        public readonly string Identifier;
        public readonly TypeOf ReturnType;
        public readonly ReadOnlyCollection<TypeOf> ParameterTypes;

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
    }
}
