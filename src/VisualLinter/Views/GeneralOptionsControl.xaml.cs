using jwldnr.VisualLinter.ViewModels;

namespace jwldnr.VisualLinter.Views
{
    /// <summary>
    /// Interaction logic for GeneralOptionsControl.xaml
    /// </summary>
    public partial class GeneralOptionsControl
    {
        internal GeneralOptionsViewModel ViewModel = _viewModel ??
            (_viewModel = new GeneralOptionsViewModel());

        private static GeneralOptionsViewModel _viewModel;

        public GeneralOptionsControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
