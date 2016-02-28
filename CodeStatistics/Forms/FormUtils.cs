using System;
using System.Windows.Forms;

namespace CodeStatistics.Forms{
    public static class FormUtils{
        public static void InvokeOnUIThread(this Form form, Action func){
            if (form.InvokeRequired)form.Invoke(func);
            else func();
        }
    }
}
