namespace LanguageJava.Elements{
    public struct TypeIdentifier{
        public static bool operator ==(TypeIdentifier obj1, TypeIdentifier obj2){
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypeIdentifier obj1, TypeIdentifier obj2){
            return !obj1.Equals(obj2);
        }

        public readonly string Prefix;
        public readonly string Name;

        public string Package { get { return Prefix.Length == 0 ? string.Empty : Prefix.Substring(0, Prefix.Length-1); } }
        public string FullName { get { return Prefix+Name; } }

        public TypeIdentifier(string prefix, string name){
            this.Prefix = prefix.Length == 0 ? string.Empty : prefix+".";
            this.Name = name;
        }

        public override string ToString(){
            return FullName;
        }

        public override int GetHashCode(){
            return FullName.GetHashCode();
        }

        public override bool Equals(object obj){
            return obj is TypeIdentifier && FullName.Equals(((TypeIdentifier)obj).FullName);
        }
    }
}
