using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Method : Member{
        public readonly string Identifier;
        public readonly Primitives? ReturnType;
        public readonly ReadOnlyCollection<Primitives?> ParameterTypes;

        public Method(string identifier, Primitives? returnType, List<Primitives?> parameterTypes, Member info) : base(info){
            this.Identifier = identifier;
            this.ReturnType = returnType;
            this.ParameterTypes = parameterTypes.AsReadOnly();
        }
    }
}
