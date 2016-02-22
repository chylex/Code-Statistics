namespace CodeStatistics.Handling.Languages.Java.Elements{
    public struct ImportStatement{
        public readonly string FullType;
        public readonly bool IsStatic;

        public ImportStatement(string fullType, bool isStatic = false){
            this.FullType = fullType;
            this.IsStatic = isStatic;
        }
    }
}
