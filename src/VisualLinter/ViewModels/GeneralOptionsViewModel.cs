using jwldnr.VisualLinter.Helpers;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace jwldnr.VisualLinter.ViewModels
{
    internal sealed class GeneralOptionsViewModel : OptionsViewModelBase
    {
        internal static readonly DependencyProperty DisableEslintIgnoreProperty =
            DependencyProperty.Register(
                nameof(DisableEslintIgnore),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty EnableHtmlLanguageSupportProperty =
            DependencyProperty.Register(
                nameof(EnableHtmlLanguageSupport),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty EnableJavaScriptLanguageSupportProperty =
            DependencyProperty.Register(
                nameof(EnableJavaScriptLanguageSupport),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(true));

        internal static readonly DependencyProperty EnableReactLanguageSupportProperty =
            DependencyProperty.Register(
                nameof(EnableReactLanguageSupport),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty EnableVueLanguageSupportProperty =
            DependencyProperty.Register(
                nameof(EnableVueLanguageSupport),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty UseGlobalEslintProperty =
            DependencyProperty.Register(
                nameof(UseGlobalEslint),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        internal static readonly DependencyProperty UsePersonalConfigProperty =
            DependencyProperty.Register(
                nameof(UsePersonalConfig),
                typeof(bool),
                typeof(GeneralOptionsViewModel),
                new PropertyMetadata(false));

        private readonly IVisualLinterOptions _options;
        private ICommand _suggestNewFeaturesCommand;

        public ICommand SuggestNewFeaturesCommand
        {
            get => _suggestNewFeaturesCommand ??
                (_suggestNewFeaturesCommand = new RelayCommand<string>(SuggestNewFeatures));
            set => _suggestNewFeaturesCommand = value;
        }

        internal bool DisableEslintIgnore
        {
            get => GetPropertyValue<bool>(DisableEslintIgnoreProperty);
            set => SetPropertyValue(DisableEslintIgnoreProperty, value);
        }

        internal bool EnableHtmlLanguageSupport
        {
            get => GetPropertyValue<bool>(EnableHtmlLanguageSupportProperty);
            set => SetPropertyValue(EnableHtmlLanguageSupportProperty, value);
        }

        internal bool EnableJavaScriptLanguageSupport
        {
            get => GetPropertyValue<bool>(EnableJavaScriptLanguageSupportProperty);
            set => SetPropertyValue(EnableJavaScriptLanguageSupportProperty, value);
        }

        internal bool EnableReactLanguageSupport
        {
            get => GetPropertyValue<bool>(EnableReactLanguageSupportProperty);
            set => SetPropertyValue(EnableReactLanguageSupportProperty, value);
        }

        internal bool EnableVueLanguageSupport
        {
            get => GetPropertyValue<bool>(EnableVueLanguageSupportProperty);
            set => SetPropertyValue(EnableVueLanguageSupportProperty, value);
        }

        internal bool UseGlobalEslint
        {
            get => GetPropertyValue<bool>(UseGlobalEslintProperty);
            set => SetPropertyValue(UseGlobalEslintProperty, value);
        }

        internal bool UsePersonalConfig
        {
            get => GetPropertyValue<bool>(UsePersonalConfigProperty);
            set => SetPropertyValue(UsePersonalConfigProperty, value);
        }

        internal GeneralOptionsViewModel()
        {
            _options = ServiceProvider.GlobalProvider.GetMefService<IVisualLinterOptions>() ??
                throw new Exception("exception: unable to retrieve options");
        }

        internal void Load()
        {
            UseGlobalEslint = _options.UseGlobalEslint;
            EnableHtmlLanguageSupport = _options.EnableHtmlLanguageSupport;
            EnableJavaScriptLanguageSupport = _options.EnableJavaScriptLanguageSupport;
            EnableReactLanguageSupport = _options.EnableReactLanguageSupport;
            EnableVueLanguageSupport = _options.EnableVueLanguageSupport;
            UsePersonalConfig = _options.UsePersonalConfig;
            DisableEslintIgnore = _options.DisableIgnorePath;
        }

        internal void Save()
        {
            _options.DisableIgnorePath = DisableEslintIgnore;
            _options.EnableHtmlLanguageSupport = EnableHtmlLanguageSupport;
            _options.EnableJavaScriptLanguageSupport = EnableJavaScriptLanguageSupport;
            _options.EnableReactLanguageSupport = EnableReactLanguageSupport;
            _options.EnableVueLanguageSupport = EnableVueLanguageSupport;
            _options.UseGlobalEslint = UseGlobalEslint;
            _options.UsePersonalConfig = UsePersonalConfig;
        }

        private static void SuggestNewFeatures(object url)
        {
            try
            {
                Process.Start(new ProcessStartInfo($"{url}"));
            }
            catch (Exception e)
            {
                OutputWindowHelper.WriteLine($"exception: could not open {url}");
                OutputWindowHelper.WriteLine(e.Message);
            }
        }
    }
}
