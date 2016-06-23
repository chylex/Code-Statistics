using System;
using CodeStatistics.Forms;

namespace CodeStatistics.Input.Methods{
    interface IInputMethod{
        void BeginProcess(ProjectLoadForm.UpdateCallbacks callbacks);
        void CancelProcess(Action onCancelFinish);
    }
}
