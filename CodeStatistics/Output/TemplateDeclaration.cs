using System;

namespace CodeStatistics.Output{
    struct TemplateDeclaration{
        private const string DeclarationStart = "<!--";
        private const string DeclarationEnd = "-->";
        private static readonly char[] DeclarationSplit = { ' ' };

        private static readonly TemplateDeclaration Default = new TemplateDeclaration();

        public static bool IsDeclaration(string line){
            return line.StartsWith(DeclarationStart, StringComparison.Ordinal) && line.EndsWith(DeclarationEnd, StringComparison.Ordinal);
        }

        public static bool TryReadLine(string line, out TemplateDeclaration declaration){
            if (IsDeclaration(line)){
                string stripped = line.Substring(DeclarationStart.Length, line.Length-DeclarationEnd.Length-DeclarationStart.Length);
                string[] data = stripped.Trim().Split(DeclarationSplit, 2);

                if (data.Length == 2){
                    TemplateDeclarationType type = GetDeclarationType(data[0]);
                    declaration = new TemplateDeclaration(type, data[1]);

                    return type != TemplateDeclarationType.Invalid;
                }
            }

            declaration = Default;
            return false;
        }

        private static TemplateDeclarationType GetDeclarationType(string str){
            switch(str.ToUpperInvariant()){
                case "TEMPLATE": return TemplateDeclarationType.Dynamic;
                case "LITERALTEMPLATE": return TemplateDeclarationType.Literal;
                case "INCLUDE": return TemplateDeclarationType.Include;
                default: return TemplateDeclarationType.Invalid;
            }
        }

        public enum TemplateDeclarationType{
            Invalid, Literal, Dynamic, Include
        }

        public readonly TemplateDeclarationType Type;
        public readonly string Name;

        public bool DefinesTemplate{
            get{
                return Type == TemplateDeclarationType.Dynamic || Type == TemplateDeclarationType.Literal;
            }
        }

        private TemplateDeclaration(TemplateDeclarationType type, string name){
            this.Type = type;
            this.Name = name;
        }

        public Template CreateTemplate(string text){
            switch(Type){
                case TemplateDeclarationType.Literal: return new Template.Literal(text);
                case TemplateDeclarationType.Dynamic: return new Template.Dynamic(text);
                default: throw new InvalidOperationException("Cannot create a template using an invalid declaration type.");
            }
        }
    }
}
