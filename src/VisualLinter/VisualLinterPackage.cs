using jwldnr.VisualLinter.Views;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace jwldnr.VisualLinter
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, GeneralOptions.PageName, 0, 0, true)]
    [ProvideOptionPage(typeof(AdvancedOptions), Vsix.Name, AdvancedOptions.PageName, 0, 0, true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version, IconResourceID = 400)]
    [Guid(PackageGuids.GuidVisualLinterPackageString)]
    internal sealed class VisualLinterPackage : AsyncPackage
    {
    }
}
