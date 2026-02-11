using System.Collections;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.DataManagement
{
    public static class DataUtils
    {
        public static IEnumerator LoadProviderAsync<T>(DataProvider<T> data) where T : ISaveData
        {
            bool isDataSaveExists = false;

            yield return data.ExistsAsync(result => isDataSaveExists = result);

            if (isDataSaveExists)
                yield return data.LoadAsync();
            else
            {
                data.Reset();
                yield return data.SaveAsync();
            }
            
            Debug.Log($"Data {typeof(T).Name} loaded");
        }
    }
}