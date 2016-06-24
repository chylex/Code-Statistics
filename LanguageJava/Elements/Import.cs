namespace LanguageJava.Elements{
    public struct Import{
        public static bool operator ==(Import obj1, Import obj2){
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Import obj1, Import obj2){
            return !obj1.Equals(obj2);
        }

        public readonly string FullType;
        public readonly bool IsStatic;

        public Import(string fullType, bool isStatic){
            this.FullType = fullType;
            this.IsStatic = isStatic;
        }

        public override bool Equals(object obj){
            if (obj is Import){
                var statement = (Import)obj;
                return IsStatic == statement.IsStatic && FullType == statement.FullType;
            }
            else return false;
        }

        public override int GetHashCode(){
            return FullType.GetHashCode() * (IsStatic ? 1 : -1);
        }

        public override string ToString(){
            return "import "+(IsStatic ? "static " : "")+FullType;
        }
    }
}
