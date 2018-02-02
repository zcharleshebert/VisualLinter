using jwldnr.VisualLinter.ViewModels;

namespace jwldnr.VisualLinter.Views
{
    /// <summary>
    /// Interaction logic for AdvancedOptionsControl.xaml
    /// </summary>
    public partial class AdvancedOptionsControl
    {
        internal AdvancedOptionsViewModel ViewModel = _viewModel ??
            (_viewModel = new AdvancedOptionsViewModel());

        private static AdvancedOptionsViewModel _viewModel;

        public AdvancedOptionsControl()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
