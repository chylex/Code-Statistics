using System;
using CodeStatistics.Forms.Project;

namespace CodeStatistics.Input{
    interface IInputMethod{
        void BeginProcess(ProjectLoadForm.UpdateCallbacks callbacks);
        void CancelProcess(Action onCancelFinish);
    }
}
