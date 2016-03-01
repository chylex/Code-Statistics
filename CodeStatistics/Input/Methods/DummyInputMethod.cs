using System;
using CodeStatistics.Forms;

namespace CodeStatistics.Input.Methods{
    class DummyInputMethod : IInputMethod{
        public void BeginProcess(ProjectLoadForm.UpdateCallbacks callbacks){
            callbacks.OnReady(new FileSearch(new string[0]));
        }

        public void CancelProcess(Action onCancelFinish){}
    }
}
