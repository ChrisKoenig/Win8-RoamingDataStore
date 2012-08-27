using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mastermind.Helpers
{
    public class StorageHelper
    {
        public async static Task<T> GetObjectFromRoamingFolder<T>(ApplicationData appData, string filename)
        {
            StorageFile sampleFile = await appData.RoamingFolder.GetFileAsync(filename);
            String jsonData = await FileIO.ReadTextAsync(sampleFile);
            return await JsonConvert.DeserializeObjectAsync<T>(jsonData);
        }

        public async static void SaveObjectToRoamingFolder(ApplicationData appData, string filename, object o)
        {
            string jsonData = await JsonConvert.SerializeObjectAsync(o);
            StorageFile sampleFile = await appData.RoamingFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, jsonData);
        }

        public async static Task<T> GetObjectFromSetting<T>(ApplicationData appData, string setting)
        {
            var raw = appData.RoamingSettings.Values[setting];
            T obj = (T)raw;
            return obj;
        }
    }
}
