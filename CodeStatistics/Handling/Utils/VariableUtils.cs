﻿using System;

namespace CodeStatistics.Handling.Utils{
    static class VariableUtils{
        public static void Average(this Variables.Root variables, string targetName, string totalValueName, string unitValueName){
            float avg = (float)variables.GetVariable(totalValueName,0)/Math.Max(1,variables.GetVariable(unitValueName,1));
            variables.SetVariable(targetName,(int)Math.Floor(avg));
        }

        public static void Minimum(this Variables.Root variables, string name, int nextValue){
            int currentValue = variables.GetVariable(name,int.MaxValue);
            if (nextValue < currentValue)variables.SetVariable(name,nextValue);
        }

        public static void Maximum(this Variables.Root variables, string name, int nextValue){
            int currentValue = variables.GetVariable(name,int.MinValue);
            if (nextValue > currentValue)variables.SetVariable(name,nextValue);
        }
    }
}