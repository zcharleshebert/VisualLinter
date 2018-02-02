using Microsoft.VisualStudio.Shell;
using System.Windows;

namespace jwldnr.VisualLinter.Views
{
    internal class GeneralOptions : UIElementDialogPage
    {
        internal const string PageName = "General";

        private GeneralOptionsControl _generalOptionsControl;

        protected override UIElement Child => _generalOptionsControl ??
            (_generalOptionsControl = new GeneralOptionsControl());

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            _generalOptionsControl.ViewModel.Load();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            _generalOptionsControl.ViewModel.Save();
        }
    }
}
