using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastermind.Helpers
{
    public class StorageHelper
    {
        private static string GAME_IN_PROGRESS = "GAME_IN_PROGRESS";

        public async static Task<T> GetObjectFromRoamingFolder<T>(string filename)
        {
            return await GetObjectFromRoamingFolder<T>(ApplicationData.Current, filename);
        }

        public async static Task<T> GetObjectFromRoamingFolder<T>(ApplicationData appData, string filename)
        {
            StorageFile sampleFile = await appData.RoamingFolder.GetFileAsync(filename);
            String jsonData = await FileIO.ReadTextAsync(sampleFile);
            var o = await JsonConvert.DeserializeObjectAsync<T>(jsonData);
            return o;
        }

        public async static Task<bool> SaveObjectToRoamingFolder(string filename, object o)
        {
            var appData = ApplicationData.Current;
            string jsonData = await JsonConvert.SerializeObjectAsync(o);
            StorageFile sampleFile = await appData.RoamingFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, jsonData);
            return true;
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
            SetGameOver();
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
                return roamingSettings.Values.ContainsKey(GAME_IN_PROGRESS);
            }
        }

        internal static void SetGameOver()
        {
            var appData = ApplicationData.Current;
            var roamingSettings = appData.RoamingSettings;
            roamingSettings.Values.Remove(GAME_IN_PROGRESS);
        }

        internal static void SetGameInProgress()
        {
            var appData = ApplicationData.Current;
            var roamingSettings = appData.RoamingSettings;
            roamingSettings.Values[GAME_IN_PROGRESS] = DateTime.Now.ToString();
        }
    }
}
