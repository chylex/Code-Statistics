using System.Linq;

namespace CodeStatistics.Handlers.Objects.Java.Enums{
    static class JavaModifiers{
        public enum Visibility{
            Public, Default, Protected, Private
        }

        public enum Scope{
            Static, Instance
        }

        public enum Finality{
            Mutable, Final, Abstract
        }

        public struct Info{
            public Visibility Visibility;
            public Scope Scope;
            public Finality Finality;

            public Info(Visibility visibility, Scope scope, Finality finality){
                this.Visibility = visibility;
                this.Scope = scope;
                this.Finality = finality;
            }

            public Info(string[] modifiers){
                this.Visibility = modifiers.Contains("public") ? Visibility.Public : modifiers.Contains("protected") ? Visibility.Protected : modifiers.Contains("private") ? Visibility.Private : Visibility.Default;
                this.Scope = modifiers.Contains("static") ? Scope.Static : Scope.Instance;
                this.Finality = modifiers.Contains("abstract") ? Finality.Abstract : modifiers.Contains("final") ? Finality.Final : Finality.Mutable;
            }
        }
    }
}
