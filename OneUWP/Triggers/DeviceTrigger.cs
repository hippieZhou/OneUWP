using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace OneUWP.Triggers
{
    // http://www.cnblogs.com/h82258652/p/4508777.html



    public enum DeviceType
    {
        Unknown,
        Desktop,
        Mobile
    }

    public class DeviceTrigger: StateTriggerBase
    {
        public DeviceType Device
        {
            get { return (DeviceType)GetValue(DeviceProperty); }
            set { SetValue(DeviceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Device.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeviceProperty =
            DependencyProperty.Register("Device", typeof(DeviceType), typeof(DeviceTrigger), new PropertyMetadata(DeviceType.Desktop,new PropertyChangedCallback(PropertyChangedFunc)));

        private static void PropertyChangedFunc(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var trigger = d as DeviceTrigger;
            if (!trigger.Equals(null))
            {
                var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
                if (qualifiers.ContainsKey("DeviceFamily"))
                {
                    switch (qualifiers["DeviceFamily"])
                    {
                        case "Desktop":
                            trigger.SetActive(trigger.Device == DeviceType.Desktop);
                            break;

                        case "Mobile":
                            trigger.SetActive(trigger.Device == DeviceType.Mobile);
                            break;
                    }
                }
            }
        }
    }
}
