namespace CodeStatistics.Handling.Languages.Java.Elements{
    public struct ImportStatement{
        public static bool operator ==(ImportStatement obj1, ImportStatement obj2){
            return obj1.Equals(obj2);
        }

        public static bool operator !=(ImportStatement obj1, ImportStatement obj2){
            return !obj1.Equals(obj2);
        }

        public readonly string FullType;
        public readonly bool IsStatic;

        public ImportStatement(string fullType, bool isStatic){
            this.FullType = fullType;
            this.IsStatic = isStatic;
        }

        public override bool Equals(object obj){
            if (obj is ImportStatement){
                var statement = (ImportStatement)obj;
                return IsStatic == statement.IsStatic && FullType == statement.FullType;
            }
            else return false;
        }

        public override int GetHashCode(){
            return FullType.GetHashCode() * (IsStatic ? 1 : -1);
        }
    }
}
