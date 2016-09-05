using System;

namespace CodeStatisticsCore.Handling.Utils{
    public static class VariableUtils{
        public static void Average(this Variables.Root variables, string targetName, string totalValueName, string unitValueName, int roundingThreshold = 10){
            float avg = (float)variables.GetVariable(totalValueName, 0)/Math.Max(1, variables.GetVariable(unitValueName, 1));
            variables.SetVariable(targetName, avg >= roundingThreshold ? (int)Math.Round(avg) : avg);
        }

        public static void Percent(this Variables.Root variables, string targetName, string totalValueName, string unitValueName){
            int val = (int)Math.Round(100.0*variables.GetVariable(totalValueName, 0)/Math.Max(1, variables.GetVariable(unitValueName, 1)));
            variables.SetVariable(targetName, val);
        }

        public static void Minimum(this Variables.Root variables, string name, int nextValue){
            int currentValue = variables.GetVariable(name, int.MaxValue);
            if (nextValue < currentValue)variables.SetVariable(name, nextValue);
        }

        public static void Maximum(this Variables.Root variables, string name, int nextValue){
            int currentValue = variables.GetVariable(name, int.MinValue);
            if (nextValue > currentValue)variables.SetVariable(name, nextValue);
        }
    }
}
