using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Type{
        public enum DeclarationType{
            Class, Interface, Enum, Annotation
        }

        public readonly ReadOnlyCollection<Annotation> Annotations;
        public readonly Modifiers Modifiers = Modifiers.None;
        public readonly DeclarationType Declaration;
        public readonly string Identifier;

        public readonly List<Type> NestedTypes = new List<Type>(1);

        public Type(List<Annotation> annotations, List<Modifiers> modifiers, DeclarationType type, string identifier){
            this.Annotations = annotations.AsReadOnly();
            this.Modifiers = modifiers.Aggregate(Modifiers.None, (acc, modifier) => acc | modifier);
            this.Declaration = type;
            this.Identifier = identifier;
        }
    }
}
