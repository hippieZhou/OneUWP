using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Models;
using OneCore.Untils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Notifications;

namespace OneBackgroundTask
{
    public sealed class LiveTileTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            BackgroundTaskManager.UpdateTopOnes();

            deferral.Complete();
        }
    }
}
