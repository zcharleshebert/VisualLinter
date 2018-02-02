using Microsoft.VisualStudio.Shell;
using System.Windows;

namespace jwldnr.VisualLinter.Views
{
    internal class AdvancedOptionsPage : UIElementDialogPage
    {
        internal const string PageName = "Advanced";

        private AdvancedOptionsPageControl _advancedOptionsPageControl;

        protected override UIElement Child => _advancedOptionsPageControl ??
            (_advancedOptionsPageControl = new AdvancedOptionsPageControl());

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            _advancedOptionsPageControl.ViewModel.Load();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            _advancedOptionsPageControl.ViewModel.Save();
        }
    }
}
