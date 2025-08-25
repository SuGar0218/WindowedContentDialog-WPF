using SuGarToolkit.Sample.Dialogs.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuGarToolkit.WPF.Samples.Dialogs.ViewModels;

internal class SampleViewModel
{
    public MessageBoxSettings MessageBoxSettings { get; } = new();
    public ContentDialogSettings ContentDialogSettings { get; } = new();
}
