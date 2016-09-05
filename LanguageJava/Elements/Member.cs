using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LanguageJava.Elements{
    public class Member{
        public readonly ReadOnlyCollection<Annotation> Annotations;
        public readonly Modifiers Modifiers;

        public Member(List<Annotation> annotations, IEnumerable<Modifiers> modifiers){
            this.Annotations = annotations.AsReadOnly();
            this.Modifiers = modifiers.Aggregate(Modifiers.None, (acc, modifier) => acc | modifier);
        }

        public Member(Member source, Modifiers newModifiers){
            this.Annotations = source.Annotations;
            this.Modifiers = newModifiers;
        }

        public Member(Member source){
            this.Annotations = source.Annotations;
            this.Modifiers = source.Modifiers;
        }

        public override string ToString(){
            string memberStr = Annotations.Aggregate("", (str, annotation) => str+annotation.ToString()+' ');

            foreach(Modifiers modifier in JavaModifiers.Values){
                if (Modifiers.HasFlag(modifier))memberStr += JavaModifiers.ToString(modifier)+' ';
            }

            return memberStr;
        }
    }
}
