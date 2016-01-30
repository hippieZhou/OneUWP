using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

namespace OneCore.Untils
{
    class DownloadManager
    {
        /// <summary>
        /// http://image.wufazhuce.com/FrI0HJi0nofr1kXIv_u7SF0IJXVm
        /// </summary>
        /// <param name="url"></param>
        public async void Init(string url)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.SuggestedFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".jpg";
            picker.FileTypeChoices.Add("图片文件", new List<string>() { ".jpg" });
            StorageFile file = await picker.PickSaveFileAsync();
            if (null != file)
            {
                CachedFileManager.DeferUpdates(file);
                using (HttpClient client = new HttpClient())
                {
                    byte[] fileContent = Task.Run(() => { return client.GetByteArrayAsync(url); }).Result;
                    await FileIO.WriteBytesAsync(file, fileContent);
                }

                // 当完成更改时，其他应用程序才可以对该文件进行更改。
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    System.Diagnostics.Debug.WriteLine(" 已经保存好了。");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" 保存失败了。");
                }
            }
            else
            {
                return;
            }
        }
    }
}
