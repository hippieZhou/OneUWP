using GalaSoft.MvvmLight;
using OneCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace OneCore.Untils
{
    public class ShareManager
    {
        private static ShareManager _current;

        public static ShareManager Current
        {
            get { return _current??(_current = new ShareManager()); }
        }
        public ShareManager()
        {
            var transfer = DataTransferManager.GetForCurrentView();
            transfer.DataRequested += Transfer_DataRequested;
        }

        private ViewModelBase VM = null;

        private void Transfer_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (!Equals(null, VM))
            {
                DataPackage dp = new DataPackage();
                switch (VM.GetType().Name)
                {
                    case "PictureViewModel":
                        dp.Properties.Title = "分享图片";
                        dp.SetText( $"一张图片，千言万语[来自ONE-UWP的分享]：{(VM as PictureViewModel).Picture.WebLk}");
                        break;
                    case "ArticleViewModel":
                        dp.Properties.Title = "分享文章";
                        dp.SetText($"世间风情，字里行间[来自ONE-UWP的分享]：{(VM as ArticleViewModel).Article.WebLk}");
                        break;
                    case "QuestionViewModel":
                        dp.Properties.Title = "分享问题";
                        dp.SetText($"每天一问，有问必答[来自ONE-UWP的分享]：{(VM as QuestionViewModel).Question.WebLk}");
                        break;
                    case "ThingViewModel":
                        dp.Properties.Title = "分享东西";
                        dp.SetText($"友好世界，有好东西[来自ONE-UWP的分享]：{(VM as ThingViewModel).Thing.Wu}");
                        break;
                }
                args.Request.Data = dp;
            }
            else
            {
                return;
            }
        }

        public void ShowShareUI(ViewModelBase vm)
        {
            DataTransferManager.ShowShareUI();
            this.VM = vm;
        }
    }
}
