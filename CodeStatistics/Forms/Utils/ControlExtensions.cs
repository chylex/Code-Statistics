using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms.Utils{
    static class ControlExtensions{
        public static void InvokeSafe(this Control control, Action func){
            if (control.InvokeRequired){
                control.Invoke(func);
            }
            else{
                func();
            }
        }

        public static void SetValueInstant(this ProgressBar bar, int value){
            if (value == bar.Maximum){
                bar.Value = value;
                bar.Value = value-1;
                bar.Value = value;
            }
            else{
                bar.Value = value+1;
                bar.Value = value;
            }
        }
    }
}
