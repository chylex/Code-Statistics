namespace LanguageJava.Elements{
    public struct Annotation{
        public static bool operator ==(Annotation obj1, Annotation obj2){
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Annotation obj1, Annotation obj2){
            return !obj1.Equals(obj2);
        }

        public readonly string SimpleName;

        public Annotation(string simpleName){
            this.SimpleName = simpleName;
        }

        public override bool Equals(object obj){
            return obj is Annotation && SimpleName == ((Annotation)obj).SimpleName;
        }

        public override int GetHashCode(){
            return SimpleName.GetHashCode();
        }

        public override string ToString(){
            return '@'+SimpleName;
        }
    }
}
