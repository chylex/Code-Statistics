using System.Collections.Generic;
using CodeStatisticsCore.Collections;
using LanguageJava.Elements;

namespace LanguageJava.Handling{
    public class JavaGlobalInfo{
        public readonly CounterDictionary<string> AnnotationUses = new CounterDictionary<string>(8);
        public readonly CounterDictionary<string> FieldTypes = new CounterDictionary<string>(10);
        public readonly CounterDictionary<string> MethodReturnTypes = new CounterDictionary<string>(10);
        public readonly CounterDictionary<string> MethodParameterTypes = new CounterDictionary<string>(10);

        public readonly Dictionary<FlowStatement,int> Statements = EnumDictionary.Create<FlowStatement,int>(0);
        public int MinSwitchCases, MaxSwitchCases, MinCatchBlocks, MaxCatchBlocks, TryCatchWithFinally, TryWithResourcesWithFinally;

        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.Name.Length-x.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersSimpleBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.Name.Length-y.Name.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullTop = new TopElementList<TypeIdentifier>(10,(x,y) => y.FullName.Length-x.FullName.Length);
        public readonly TopElementList<TypeIdentifier> IdentifiersFullBottom = new TopElementList<TypeIdentifier>(10,(x,y) => x.FullName.Length-y.FullName.Length);
    }
}
