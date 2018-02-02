using jwldnr.VisualLinter.ViewModels;

namespace jwldnr.VisualLinter.Views
{
    /// <summary>
    /// Interaction logic for AdvancedOptionsPageControl.xaml
    /// </summary>
    public partial class AdvancedOptionsPageControl
    {
        internal AdvancedOptionsViewModel ViewModel = _viewModel ??
            (_viewModel = new AdvancedOptionsViewModel());

        private static AdvancedOptionsViewModel _viewModel;

        public AdvancedOptionsPageControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
