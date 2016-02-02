using GalaSoft.MvvmLight;
using OneCore.Https;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCore.ViewModels
{
    public class OneViewModelBase:ViewModelBase
    {
        private DateTime _currentTime = DateTime.Parse(ServiceURL.strToday);
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set { Set(ref _currentTime, value); }
        }
    }
}
