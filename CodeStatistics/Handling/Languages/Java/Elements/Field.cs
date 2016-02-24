namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Field : Member{
        public readonly string Identifier;
        public readonly Primitives? Type;

        public Field(string identifier, Primitives? type, Member info) : base(info){
            this.Identifier = identifier;
            this.Type = type;
        }
    }
}
