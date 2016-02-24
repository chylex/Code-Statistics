using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Member{
        public readonly ReadOnlyCollection<Annotation> Annotations;
        public readonly Modifiers Modifiers = Modifiers.None;

        public Member(List<Annotation> annotations, IEnumerable<Modifiers> modifiers){
            this.Annotations = annotations.AsReadOnly();
            this.Modifiers = modifiers.Aggregate(Modifiers.None, (acc, modifier) => acc | modifier);
        }

        public Member(Member source){
            this.Annotations = source.Annotations;
            this.Modifiers = source.Modifiers;
        }
    }
}
