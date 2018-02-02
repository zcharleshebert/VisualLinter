using Microsoft.VisualStudio.Shell;
using System.Windows;

namespace jwldnr.VisualLinter.Views
{
    internal class AdvancedOptions : UIElementDialogPage
    {
        internal const string PageName = "Advanced";

        private AdvancedOptionsControl _advancedOptionsControl;

        protected override UIElement Child => _advancedOptionsControl ??
            (_advancedOptionsControl = new AdvancedOptionsControl());

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            _advancedOptionsControl.ViewModel.Load();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            _advancedOptionsControl.ViewModel.Save();
        }
    }
}
