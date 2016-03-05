namespace CodeStatistics.Handling.Languages.Java.Elements{
    public class Field : Member{
        public readonly string Identifier;
        public readonly TypeOf Type;

        public Field(string identifier, TypeOf type, Member info) : base(info){
            this.Identifier = identifier;
            this.Type = type;
        }

        public override string ToString(){
            return base.ToString()+Type+' '+Identifier;
        }
    }
}
