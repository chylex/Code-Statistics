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
            public readonly Visibility Visibility;
            public readonly Scope Scope;
            public readonly Finality Finality;

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

        public static string GetName(this Visibility visibility){
            switch(visibility){
                case Visibility.Public: return "public";
                case Visibility.Default: return "default";
                case Visibility.Protected: return "protected";
                case Visibility.Private: return "private";
                default: return "";
            }
        }

        public static string GetName(this Scope scope){
            switch(scope){
                case Scope.Static: return "static";
                case Scope.Instance: return "instance";
                default: return "";
            }
        }

        public static string GetName(this Finality finality){
            switch(finality){
                case Finality.Mutable: return "non-final";
                case Finality.Final: return "final";
                case Finality.Abstract: return "abstract";
                default: return "";
            }
        }
    }
}
