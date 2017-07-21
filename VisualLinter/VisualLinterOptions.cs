﻿using System;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

namespace jwldnr.VisualLinter
{
    internal interface IVisualLinterOptions
    {
        bool UseLocalConfig { get; set; }
    }

    [Export(typeof(IVisualLinterOptions))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class VisualLinterOptions : IVisualLinterOptions
    {
        public bool UseLocalConfig
        {
            get => _writableSettingsStore.GetBoolean(CollectionPath, nameof(UseLocalConfig), false);
            set => _writableSettingsStore.SetBoolean(CollectionPath, nameof(UseLocalConfig), value);
        }

        private const string CollectionPath = "jwldnr.VisualLinter";

        private readonly WritableSettingsStore _writableSettingsStore;

        [ImportingConstructor]
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        internal VisualLinterOptions([Import] SVsServiceProvider serviceProvider)
            : this(new ShellSettingsManager(serviceProvider))
        {
        }

        internal VisualLinterOptions(SettingsManager settingsManager)
        {
            _writableSettingsStore = settingsManager?.GetWritableSettingsStore(SettingsScope.UserSettings)
                ?? throw new ArgumentNullException(nameof(settingsManager));

            if (_writableSettingsStore == null || _writableSettingsStore.CollectionExists(CollectionPath))
                return;

            _writableSettingsStore.CreateCollection(CollectionPath);
        }
    }
}