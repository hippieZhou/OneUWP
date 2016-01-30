using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace OneCore.Untils
{
    public class BackgroundTaskManager
    {
        private const string LIVETILETASK = "LIVETILETAKS";
        /// <summary>
        /// 屏幕点亮时执行该任务:UserPresent
        /// http://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.systemtriggertype.aspx
        /// </summary>
        /// <param name="taskEntryPoint">后台任务名</param>
        public static async void Register(string taskEntryPoint)
        {
            BackgroundExecutionManager.RemoveAccess();
            var status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Unspecified || status == BackgroundAccessStatus.Denied)
            {
                await new MessageDialog("系统关闭了后台运行，请前往‘系统设置’进行设置").ShowAsync();
                return;
            }
            else
            {
                var taskReg = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(t => t.Name.Equals(LIVETILETASK)) as BackgroundTaskRegistration;

                if (taskReg == null)
                {
                    //注册任务
                    var taskBuilder = new BackgroundTaskBuilder
                    {
                        Name = LIVETILETASK,
                        TaskEntryPoint = taskEntryPoint
                    };

                    var trigger = new SystemTrigger(SystemTriggerType.UserPresent, false);
                    taskBuilder.SetTrigger(trigger);
                    taskBuilder.Register();
                }
                else
                {
                    return;
                }
            }
        }

        public static void UpdateTopOnes()
        {
            try
            {
                JObject obj_Picture = CacheManager.ReadObjectAsync(ViewType.Picture).Result;
                OnePicture picture = Equals(obj_Picture["result"].ToString(), "SUCCESS") ?
                      JsonConvert.DeserializeObject<OnePicture>(obj_Picture["hpEntity"].ToString()) : new OnePicture();

                JObject obj_Article = CacheManager.ReadObjectAsync(ViewType.Article).Result;
                OneArticle article = Equals(obj_Article["result"].ToString(), "SUCCESS") ?
                JsonConvert.DeserializeObject<OneArticle>(obj_Article["contentEntity"].ToString()) : new OneArticle();

                JObject obj_Question = CacheManager.ReadObjectAsync(ViewType.Question).Result;
                OneQuestion question = Equals(obj_Question["result"].ToString(), "SUCCESS") ?
                    JsonConvert.DeserializeObject<OneQuestion>(obj_Question["questionAdEntity"].ToString()) : new OneQuestion();

                if (!picture.Equals(null) && !article.Equals(null) && !question.Equals(null))
                {
                    UpdateBadge(picture, article, question);
                    //ShowToast(picture, article, question);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 更新动态磁贴
        ///https://msdn.microsoft.com/zh-cn/library/windows/apps/mt186446.aspx
        ///https://msdn.microsoft.com/zh-cn/library/windows/apps/mt590880.aspx
        ///10586: TileMedium:hint-crop="circle"
        ///</summary>
        private static void UpdateBadge(OnePicture picture, OneArticle article, OneQuestion question)
        {
            try
            {
                string TileTemplateXml = $@"<tile>
                                    <visual branding='nameAndLogo'>
                                      <binding template = 'TileMedium' hint-textStacking='center'>
                                        <image src = '{picture.OriginalImgUrl}' placement='peek'/>
                                        <text hint-style='base' hint-align='center'>{picture.Author.Split(new char[] { '&' }).FirstOrDefault()}</text>
                                        <text hint-style='captionSubtle' hint-align='center'>{picture.Author.Split(new char[] { '&' }).LastOrDefault()}</text>
                                      </binding>
                                      <binding template='TileWide'>
                                         <image src='Assets/Wide310x150Logo.scale-200.png' placement='peek'/>
                                        <group>
                                          <subgroup>
                                            <text hint-style='subtitle'>One 问题</text>
                                            <text hint-style='captionsubtle'>{question.QuestionTitle}</text>
                                            <text hint-style='captionsubtle' hint-wrap='true' hint-maxLines='3'>{question.QuestionContent}</text>
                                          </subgroup>
                                        </group>
                                      </binding>
                                      <binding template='TileLarge' hint-textStacking='center'>
                                        <group>
                                          <subgroup hint-weight='1'/>
                                          <subgroup hint-weight='2'>
                                            <image src = 'Assets/Square150x150Logo.scale-200.png' hint-crop='circle'/>
                                          </subgroup>
                                          <subgroup hint-weight='1'/>
                                        </group>
                                        <text hint-style='title' hint-align='center'>{article.ContTitle}</text>
                                        <text hint-style='subtitleSubtle' hint-align='center'>{article.GW}</text>
                                      </binding>
                                    </visual>
                                   </tile>";

                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.EnableNotificationQueueForSquare150x150(true);
                updater.EnableNotificationQueueForSquare310x310(true);
                updater.EnableNotificationQueueForWide310x150(true);
                updater.EnableNotificationQueue(true);
                updater.Clear();


                var doc = new XmlDocument();
                doc.LoadXml(System.Net.WebUtility.HtmlDecode(TileTemplateXml), new XmlLoadSettings()
                {
                    ProhibitDtd = false,
                    ValidateOnParse = false,
                    ElementContentWhiteSpace = false,
                    ResolveExternals = false
                });
                updater.Update(new TileNotification(doc));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 更新Toast通知
        /// </summary>
        private static void ShowToast(OnePicture picture, OneArticle article, OneQuestion question)
        {
            
        }


    }
}
