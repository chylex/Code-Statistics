using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms.Utils{
    static class FormExtensions{
        public static void InvokeOnUIThread(this Form form, Action func){
            if (form.InvokeRequired)form.Invoke(func);
            else func();
        }
    }
}
