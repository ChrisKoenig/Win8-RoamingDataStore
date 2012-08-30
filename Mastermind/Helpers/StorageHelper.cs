using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastermind.Helpers
{
    public class StorageHelper
    {
        public async static Task<T> GetObjectFromRoamingFolder<T>(string filename)
        {
            return await GetObjectFromRoamingFolder<T>(ApplicationData.Current, filename);
        }

        public async static Task<T> GetObjectFromRoamingFolder<T>(ApplicationData appData, string filename)
        {
            StorageFile sampleFile = await appData.RoamingFolder.GetFileAsync(filename);
            String jsonData = await FileIO.ReadTextAsync(sampleFile);
            return await JsonConvert.DeserializeObjectAsync<T>(jsonData);
        }

        public async static void SaveObjectToRoamingFolder(string filename, object o)
        {
            var appData = ApplicationData.Current;
            string jsonData = await JsonConvert.SerializeObjectAsync(o);
            StorageFile sampleFile = await appData.RoamingFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, jsonData);
        }

        public static T GetObjectFromSetting<T>(string setting)
        {
            var appData = ApplicationData.Current;
            return GetObjectFromSetting<T>(appData, setting);
        }

        public static T GetObjectFromSetting<T>(ApplicationData appData, string setting)
        {
            var raw = appData.RoamingSettings.Values[setting];
            T obj = (T)raw;
            return obj;
        }

        public static void PutObjectToSetting<T>(string key, T value)
        {
            var appData = ApplicationData.Current;
            appData.RoamingSettings.Values[key] = value;
        }

        public static async void ClearGameState()
        {
            var appData = ApplicationData.Current;
            try
            {
                foreach (var item in appData.RoamingSettings.Values)
                {
                    appData.RoamingSettings.Values.Remove(item.Key);
                }
                var files = await appData.RoamingFolder.GetFilesAsync();
                foreach (var file in files)
                {
                    await file.DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static bool GameInProgress
        {
            get
            {
                var appData = ApplicationData.Current;
                var roamingSettings = appData.RoamingSettings;
                var roamingFolder = appData.RoamingFolder;
                return true;
            }
        }
    }
}
