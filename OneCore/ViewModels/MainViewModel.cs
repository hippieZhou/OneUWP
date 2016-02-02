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
    public partial class MainViewModel: ViewModelBase
    {
        public readonly static PictureViewModel pictureViewModel = new PictureViewModel();
        public readonly static ArticleViewModel articleViewModel = new ArticleViewModel();
        public readonly static QuestionViewModel quesstionViewModel = new QuestionViewModel();
        public readonly static ThingViewModel thingViewModel = new ThingViewModel();
        public readonly static AboutViewModel aboutViewModel = new AboutViewModel();

        public DataTemplate CurrentTemplate => Equals(null, this.CurrentViewModel) ? DTManager.GetTemplate(pictureViewModel) : DTManager.GetTemplate(CurrentViewModel);

        private OneViewModelBase _currentViewModel;
        public OneViewModelBase CurrentViewModel
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

                  #region 视图切换
                  await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(() =>
                   {
                       switch (obj)
                       {
                           case "图片":
                               this.OneHeader = Application.Current.Resources["PictureTip"] as string;
                               this.CurrentViewModel = pictureViewModel;
                               break;
                           case "文章":
                               this.OneHeader = Application.Current.Resources["ArticleTip"] as string;
                               this.CurrentViewModel = articleViewModel;
                               break;
                           case "问题":
                               this.OneHeader = Application.Current.Resources["QuestionTip"] as string;
                               this.CurrentViewModel = quesstionViewModel;
                               break;
                           case "东西":
                               this.OneHeader = Application.Current.Resources["ThingTip"] as string;
                               this.CurrentViewModel = thingViewModel;
                               break;
                           case "关于":
                               this.OneHeader = Application.Current.Resources["AboutTip"] as string;
                               this.CurrentViewModel = aboutViewModel;
                               break;
                           default:
                               return;
                       }
                   }));
                  #endregion

                  RaisePropertyChanged("BackCmd");
                  RaisePropertyChanged("ForwardCmd");
              }));
            }
        }
    }

    public partial class MainViewModel : ViewModelBase
    {
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
                    bool isCanBack = false;
                    switch (this.CurrentViewModel.GetType().Name)
                    {
                        case "PictureViewModel":
                            isCanBack = !Equals(pictureViewModel.CurrentTime, DateTime.Parse(ServiceURL.strToday));
                            break;
                        case "ArticleViewModel":
                            isCanBack = !Equals(articleViewModel.CurrentTime, DateTime.Parse(ServiceURL.strToday));
                            break;
                        case "QuestionViewModel":
                            isCanBack = !Equals(quesstionViewModel.CurrentTime, DateTime.Parse(ServiceURL.strToday));
                            break;
                        case "ThingViewModel":
                            isCanBack = !Equals(thingViewModel.CurrentTime, DateTime.Parse(ServiceURL.strToday));
                            break;
                        default:
                            break ;
                    }
                    return isCanBack;
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
            switch (this.CurrentViewModel.GetType().Name)
            {
                case "PictureViewModel":
                    pictureViewModel.CurrentTime = pictureViewModel.CurrentTime.AddDays(v);
                    pictureViewModel.UpdatePicture(pictureViewModel.CurrentTime.ToString("yyyy-MM-dd"));
                    break;
                case "ArticleViewModel":
                    articleViewModel.CurrentTime = articleViewModel.CurrentTime.AddDays(v);
                    articleViewModel.UpdateArticle(articleViewModel.CurrentTime.ToString("yyyy-MM-dd"));
                    break;
                case "QuestionViewModel":
                    quesstionViewModel.CurrentTime = quesstionViewModel.CurrentTime.AddDays(v);
                    quesstionViewModel.UpdateQuestion(quesstionViewModel.CurrentTime.ToString("yyyy-MM-dd"));
                    break;
                case "ThingViewModel":
                    thingViewModel.CurrentTime = thingViewModel.CurrentTime.AddDays(v);
                    thingViewModel.UpdateThing(quesstionViewModel.CurrentTime.ToString("yyyy-MM-dd"));
                    break;
                default:
                    break;
            }
            RaisePropertyChanged("BackCmd");
            RaisePropertyChanged("ForwardCmd");
        }
    }
}
