namespace CodeStatistics.Collections{
    struct Triple<A, B, C>{
        public readonly A First;
        public readonly B Second;
        public readonly C Third;

        public Triple(A first, B second, C third){
            this.First = first;
            this.Second = second;
            this.Third = third;
        }
    }
}
