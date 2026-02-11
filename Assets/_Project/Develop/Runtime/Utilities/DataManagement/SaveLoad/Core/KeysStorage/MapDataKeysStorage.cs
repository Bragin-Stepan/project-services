using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.KeysStorage
{
    public class MapDataKeysStorage : IDataKeysStorage
    {
        private readonly Dictionary<Type, string> Keys = new (MapDataKeys.Dictionary);

        public string GetKeyFor<TData>() where TData : ISaveData => Keys[typeof(TData)];
    }
}
