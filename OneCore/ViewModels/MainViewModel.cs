using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OneCore.Https;
using OneCore.Models;
using OneCore.Untils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneCore.ViewModels
{
    public partial class MainViewModel:ViewModelBase
    {
        public readonly static PictureViewModel pictureViewModel = new PictureViewModel();
        public readonly static ArticleViewModel articleViewModel = new ArticleViewModel();
        public readonly static QuestionViewModel quesstionViewModel = new QuestionViewModel();
        public readonly static ThingViewModel thingViewModel = new ThingViewModel();

        public readonly static AboutViewModel aboutViewModel = new AboutViewModel();

        public DataTemplate CurrentTemplate => Equals(null, this.CurrentViewModel) ? DTManager.GetTemplate(pictureViewModel) : DTManager.GetTemplate(CurrentViewModel);

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel ?? (_currentViewModel = pictureViewModel); }
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged(() => CurrentViewModel);
                RaisePropertyChanged(() => CurrentTemplate);
            }
        }

        private string _oneHeader;
        public string OneHeader
        {
            get { return _oneHeader??(_oneHeader = Application.Current.Resources["PictureTip"] as string); }
            set { Set(ref _oneHeader, value); }
        }

        private string _oneDate;

        public string OneDate
        {
            get { return _oneDate ?? (_oneDate = DateTime.Now.ToString("yyyy-MM-dd")); }
            set { Set(ref _oneDate, value); }
        }

        private RelayCommand<string> _menuCmd;

        public RelayCommand<string> MenuCmd
        {
            get {
                return _menuCmd ?? (_menuCmd = new RelayCommand<string>(
              async (obj) =>
              {
                  Messenger.Default.Send(false, "ColoseMenu");
                  Messenger.Default.Send(obj, "HidenCommandBar");
                  this.CurrentTime = DateTime.Parse(ServiceURL.strToday);
                  RaisePropertyChanged("BackCmd");
                  RaisePropertyChanged("ForwardCmd");

                  await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(() =>
                   {
                       switch (obj)
                       {
                           case "图片":
                               SwitchViewModel(ViewType.Picture);
                               break;
                           case "文章":
                               SwitchViewModel(ViewType.Article);
                               break;
                           case "问题":
                               SwitchViewModel(ViewType.Question);
                               break;
                           case "东西":
                               SwitchViewModel(ViewType.Thing);
                               break;
                           case "关于":
                               SwitchViewModel(ViewType.About);
                               break;
                           default:
                               return;
                       }
                   }));
              }));
            }
        }

        private void SwitchViewModel(ViewType type)
        {
            switch (type)
            {
                case ViewType.Picture:
                    this.OneHeader = Application.Current.Resources["PictureTip"] as string;
                    this.CurrentViewModel = pictureViewModel;
                    break;
                case ViewType.Article:
                    this.OneHeader = Application.Current.Resources["ArticleTip"] as string;
                    this.CurrentViewModel = articleViewModel;
                    break;
                case ViewType.Question:
                    this.OneHeader = Application.Current.Resources["QuestionTip"] as string;
                    this.CurrentViewModel = quesstionViewModel;
                    break;
                case ViewType.Thing:
                    this.OneHeader = Application.Current.Resources["ThingTip"] as string;
                    this.CurrentViewModel = thingViewModel;
                    break;
                case ViewType.About:
                    this.OneHeader = Application.Current.Resources["AboutTip"] as string;
                    this.CurrentViewModel = aboutViewModel;
                    break;
                default:
                    return;
            }
        }
    }

    public partial class MainViewModel : ViewModelBase
    {
        private DateTime _currentTime = DateTime.Parse(ServiceURL.strToday);
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set { Set(ref _currentTime, value); }
        }

        private RelayCommand _shareContentsCmd;
        public RelayCommand ShareContentsCmd
        {
            get
            {
                return _shareContentsCmd ?? (_shareContentsCmd = new RelayCommand(() =>
                {
                    ShareManager.Current.ShowShareUI(this.CurrentViewModel);
                }, () => { return null != this.CurrentViewModel; }));
            }
        }

        private RelayCommand _backCmd;
        /// <summary>
        /// 日期+1()
        /// </summary>
        public RelayCommand BackCmd
        {
            get
            {
                return _backCmd ?? (_backCmd = new RelayCommand(() =>
                {
                    GoToOneDay(1);
                }, () =>
                {
                    return !Equals(this.CurrentTime, DateTime.Parse(ServiceURL.strToday));
                }));
            }
        }
        private RelayCommand _forwardCmd;
        /// <summary>
        /// 日期-1
        /// </summary>
        public RelayCommand ForwardCmd
        {
            get
            {
                return _forwardCmd ?? (_forwardCmd = new RelayCommand(() =>
                {
                    GoToOneDay(-1);
                }));
            }
        }

        private void GoToOneDay(int v)
        {
            this.CurrentTime = this.CurrentTime.AddDays(v);
            UpdateOne(this.CurrentViewModel, this.CurrentTime.ToString("yyyy-MM-dd"));
            RaisePropertyChanged("BackCmd");
            RaisePropertyChanged("ForwardCmd");
            Debug.WriteLine(this.CurrentTime.ToString("yyyy-MM-dd"));
        }

        private void UpdateOne(ViewModelBase currentViewModel, string v)
        {
            var res = currentViewModel.GetType().Name;
            switch (currentViewModel.GetType().Name)
            {
                case "PictureViewModel":
                    pictureViewModel.UpdatePicture(v);
                    break;
                case "ArticleViewModel":
                    articleViewModel.UpdateArticle(v);
                    break;
                case "QuestionViewModel":
                    quesstionViewModel.UpdateQuestion(v);
                    break;
                case "ThingViewModel":
                    thingViewModel.UpdateThing(v);
                    break;
                default:
                    return;
            }
            RaisePropertyChanged("CurrentViewModel");
        }
    }
}
