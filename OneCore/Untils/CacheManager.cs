using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Https;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace OneCore.Untils
{
    public class CacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view">文件名</param>
        /// <param name="json">文件内容：JSON</param>
        public static async void CheckedCache(ViewType view,string json)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalCacheFolder.CreateFolderAsync("one_cache", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync($"{view}.json", CreationCollisionOption.OpenIfExists);
                var strJson = await FileIO.ReadTextAsync(file);
                if (!strJson.Equals(json) || string.IsNullOrWhiteSpace(strJson))
                {
                    await FileIO.WriteTextAsync(file, json);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view">文件名</param>
        /// <returns></returns>
        public static async Task<JObject> ReadObjectAsync(ViewType view)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalCacheFolder.CreateFolderAsync("one_cache", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync($"{view}.json", CreationCollisionOption.OpenIfExists);
                string strJson = await FileIO.ReadTextAsync(file);
                return !strJson.Equals(null) ? JObject.Parse(strJson) : null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        public static async Task<double> ShowCacheSize()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalCacheFolder.GetFolderAsync("one_cache");
                var files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
                double size = 0;
                BasicProperties p;
                foreach (var f in files)
                {
                    p = await f.GetBasicPropertiesAsync();
                    size += p.Size;
                }
                return size / 1024 / 1024;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static async Task DelCache()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalCacheFolder.GetFolderAsync("one_cache");
                IReadOnlyCollection<StorageFile> files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
                foreach (var item in files)
                {
                    await item.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

    }
}
