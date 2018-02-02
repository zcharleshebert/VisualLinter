using jwldnr.VisualLinter.ViewModels;

namespace jwldnr.VisualLinter.Views
{
    /// <summary>
    /// Interaction logic for GeneralOptionsPageControl.xaml
    /// </summary>
    public partial class GeneralOptionsPageControl
    {
        internal GeneralOptionsViewModel ViewModel = _viewModel ??
            (_viewModel = new GeneralOptionsViewModel());

        private static GeneralOptionsViewModel _viewModel;

        public GeneralOptionsPageControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
