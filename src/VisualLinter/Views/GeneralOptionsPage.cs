using Microsoft.VisualStudio.Shell;
using System.Windows;

namespace jwldnr.VisualLinter.Views
{
    internal class GeneralOptionsPage : UIElementDialogPage
    {
        internal const string PageName = "General";

        private GeneralOptionsPageControl _generalOptionsPageControl;

        protected override UIElement Child => _generalOptionsPageControl ??
            (_generalOptionsPageControl = new GeneralOptionsPageControl());

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            _generalOptionsPageControl.ViewModel.Load();
        }

        public override void SaveSettingsToStorage()
        {
            base.SaveSettingsToStorage();

            _generalOptionsPageControl.ViewModel.Save();
        }
    }
}
