using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OneUWP.Views
{
    public sealed partial class AboutView : UserControl
    {
        public AboutView()
        {
            this.InitializeComponent();
            this.Loaded += AboutView_Loaded;
        }

        private void AboutView_Loaded(object sender, RoutedEventArgs e)
        {
            PackageVersion version = Package.Current.Id.Version;
            this.tbVersion.Text = $"版本：{version.Build}.{version.Major}.{version.Minor}.{version.Revision}";

        }
    }
}
