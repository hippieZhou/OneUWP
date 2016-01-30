using GalaSoft.MvvmLight.Messaging;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace OneUWP.Views
{
    /*
            ShowMessage.Begin();
            await Task.Delay(2000);
            HiddenMessage.Begin();
    
        */
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.btnHamburger.Click += (sender, e) =>
            {
                this.svMenu.IsPaneOpen = !this.svMenu.IsPaneOpen;
            };
            if (Equals("WindowsPhone", new EasClientDeviceInformation().OperatingSystem))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            else
            {
                if (popTips.IsOpen)  //第二次按back键
                {
                    Application.Current.Exit();
                }
                else
                {
                    popTips.IsOpen = true;  //提示再按一次
                    e.Handled = true;
                    await Task.Delay(1000);  //1000ms后关闭提示
                    popTips.IsOpen = false;
                }
            }
            e.Handled = true;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<bool>(this.svMenu, "ColoseMenu", (obj) => 
            {
                if (Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().VisibleBounds.Width <= 1200)
                {
                    this.svMenu.IsPaneOpen = false;
                }
            });

            Messenger.Default.Register<string>(this.cb, "HidenCommandBar", (obj) => 
            {
                this.cb.Visibility = Equals("关于", obj) ? Visibility.Collapsed : Visibility.Visible;
            });
        }
    }
}
