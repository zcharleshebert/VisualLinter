using jwldnr.VisualLinter.Helpers;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace jwldnr.VisualLinter.ViewModels
{
    internal class AdvancedOptionsViewModel : OptionsViewModelBase
    {
        internal static readonly DependencyProperty EslintConfigOverridePathProperty =
            DependencyProperty.Register(
                nameof(EslintConfigOverridePath),
                typeof(string),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(string.Empty));

        internal static readonly DependencyProperty EslintIgnoreOverridePathProperty =
            DependencyProperty.Register(
                nameof(EslintIgnoreOverridePath),
                typeof(string),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(string.Empty));

        internal static readonly DependencyProperty EslintOverridePathProperty =
            DependencyProperty.Register(
                nameof(EslintOverridePath),
                typeof(string),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(string.Empty));

        internal static readonly DependencyProperty ShouldOverrideEslintConfigProperty =
            DependencyProperty.Register(
                nameof(ShouldOverrideEslintConfig),
                typeof(bool),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty ShouldOverrideEslintIgnoreProperty =
            DependencyProperty.Register(
                nameof(ShouldOverrideEslintIgnore),
                typeof(bool),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty ShouldOverrideEslintProperty =
            DependencyProperty.Register(
                nameof(ShouldOverrideEslint),
                typeof(bool),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty ShowDebugInformationProperty =
            DependencyProperty.Register(
                nameof(ShowDebugInformation),
                typeof(bool),
                typeof(AdvancedOptionsViewModel),
                new PropertyMetadata(false));

        private readonly IVisualLinterOptions _options;
        private ICommand _browseEslintConfigFileCommand;
        private ICommand _browseEslintFileCommand;
        private ICommand _browseEslintIgnoreConfigFileCommand;

        public ICommand BrowseEslintConfigFileCommand
        {
            get => _browseEslintConfigFileCommand ??
                (_browseEslintConfigFileCommand = new RelayCommand(BrowseEslintConfigFile));
            set => _browseEslintConfigFileCommand = value;
        }

        public ICommand BrowseEslintFileCommand
        {
            get => _browseEslintFileCommand ??
                (_browseEslintFileCommand = new RelayCommand(BrowseEslintFile));
            set => _browseEslintFileCommand = value;
        }

        public ICommand BrowseEslintIgnoreFileCommand
        {
            get => _browseEslintIgnoreConfigFileCommand ??
                (_browseEslintIgnoreConfigFileCommand = new RelayCommand(BrowseEslintIgnoreFile));
            set => _browseEslintIgnoreConfigFileCommand = value;
        }

        internal string EslintConfigOverridePath
        {
            get => GetPropertyValue<string>(EslintConfigOverridePathProperty);
            set => SetPropertyValue(EslintConfigOverridePathProperty, value);
        }

        internal string EslintIgnoreOverridePath
        {
            get => GetPropertyValue<string>(EslintIgnoreOverridePathProperty);
            set => SetPropertyValue(EslintIgnoreOverridePathProperty, value);
        }

        internal string EslintOverridePath
        {
            get => GetPropertyValue<string>(EslintOverridePathProperty);
            set => SetPropertyValue(EslintOverridePathProperty, value);
        }

        internal bool ShouldOverrideEslint
        {
            get => GetPropertyValue<bool>(ShouldOverrideEslintProperty);
            set => SetPropertyValue(ShouldOverrideEslintProperty, value);
        }

        internal bool ShouldOverrideEslintConfig
        {
            get => GetPropertyValue<bool>(ShouldOverrideEslintConfigProperty);
            set => SetPropertyValue(ShouldOverrideEslintConfigProperty, value);
        }

        internal bool ShouldOverrideEslintIgnore
        {
            get => GetPropertyValue<bool>(ShouldOverrideEslintIgnoreProperty);
            set => SetPropertyValue(ShouldOverrideEslintIgnoreProperty, value);
        }

        internal bool ShowDebugInformation
        {
            get => GetPropertyValue<bool>(ShowDebugInformationProperty);
            set => SetPropertyValue(ShowDebugInformationProperty, value);
        }

        internal AdvancedOptionsViewModel()
        {
            _options = ServiceProvider.GlobalProvider.GetMefService<IVisualLinterOptions>() ??
                throw new Exception("exception: unable to retrieve options");
        }

        internal void Load()
        {
            ShouldOverrideEslint = _options.ShouldOverrideEslint;
            EslintOverridePath = _options.EslintOverridePath;
            ShouldOverrideEslintConfig = _options.ShouldOverrideEslintConfig;
            EslintConfigOverridePath = _options.EslintConfigOverridePath;
            ShouldOverrideEslintIgnore = _options.ShouldOverrideEslintIgnore;
            EslintIgnoreOverridePath = _options.EslintIgnoreOverridePath;
            ShowDebugInformation = _options.ShowDebugInformation;
        }

        internal void Save()
        {
            _options.ShouldOverrideEslint = ShouldOverrideEslint;
            _options.EslintOverridePath = EslintOverridePath;
            _options.ShouldOverrideEslintConfig = ShouldOverrideEslintConfig;
            _options.EslintConfigOverridePath = EslintConfigOverridePath;
            _options.ShouldOverrideEslintIgnore = ShouldOverrideEslintIgnore;
            _options.EslintIgnoreOverridePath = EslintIgnoreOverridePath;
            _options.ShowDebugInformation = ShowDebugInformation;
        }

        private static string GetDialogValue(string filter, string initialDirectory)
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    Filter = filter,
                    InitialDirectory = initialDirectory
                };

                var result = dialog.ShowDialog() ?? false;

                return false == result
                    ? null
                    : dialog.FileName;
            }
            catch (Exception e)
            {
                OutputWindowHelper.WriteLine("exception: unable to open file browse dialog");
                OutputWindowHelper.WriteLine(e.Message);
            }

            return null;
        }

        private void BrowseEslintConfigFile()
        {
            const string filter = "(*.js, *.yaml, *.yml, *.json, *.eslintrc)|*.cmd;*.exe;*.yaml;*.yml;*.json;*.eslintrc";

            var initialDirectory = string.IsNullOrEmpty(EslintConfigOverridePath)
                ? EnvironmentHelper.GetUserDirectoryPath()
                : Path.GetDirectoryName(EslintConfigOverridePath);

            var value = GetDialogValue(filter, initialDirectory);
            if (null == value)
                return;

            EslintConfigOverridePath = value;
        }

        private void BrowseEslintFile()
        {
            const string filter = "(*.cmd, *.exe)|*.cmd;*.exe";

            var initialDirectory = string.IsNullOrEmpty(EslintOverridePath)
                ? EnvironmentHelper.GetUserDirectoryPath()
                : Path.GetDirectoryName(EslintOverridePath);

            var value = GetDialogValue(filter, initialDirectory);
            if (null == value)
                return;

            EslintOverridePath = value;
        }

        private void BrowseEslintIgnoreFile()
        {
            const string filter = "(*.eslintignore)|*.eslintignore";

            var initialDirectory = string.IsNullOrEmpty(EslintIgnoreOverridePath)
                ? EnvironmentHelper.GetUserDirectoryPath()
                : Path.GetDirectoryName(EslintIgnoreOverridePath);

            var value = GetDialogValue(filter, initialDirectory);
            if (null == value)
                return;

            EslintIgnoreOverridePath = value;
        }
    }
}
