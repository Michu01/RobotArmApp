using System;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RobotArmApp.ViewModels
{
    public class ViewModel : ObservableObject, IDisposable
    {
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
