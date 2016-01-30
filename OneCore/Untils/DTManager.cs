using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace OneCore.Untils
{
    public static class DTManager
    {
        public static Dictionary<string, DataTemplate> views = new Dictionary<string, DataTemplate>();
        public static DataTemplate GetTemplate(ViewModelBase param)
        {
            Type t = param.GetType();
            if (!views.ContainsKey(t.Name))
            {
                views.Add(t.Name, Application.Current.Resources[t.Name] as DataTemplate);
            }
            return views[t.Name];
        }

        public static Dictionary<string, string> tips = new Dictionary<string, string>();
        public static string GetOneTips(string key)
        {
            if (!tips.ContainsKey(key))
            {
                tips.Add(key, Application.Current.Resources[key] as string);
            }
            return tips[key];
        }
    }
}
