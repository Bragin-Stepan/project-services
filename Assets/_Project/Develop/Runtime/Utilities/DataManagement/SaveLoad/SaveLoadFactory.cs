using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.KeysStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.Serializers;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement
{
    public class SaveLoadFactory
    {
        public SaveLoadService CreateDefaultSaveLoad()
        {
            IDataRepository dataRepository;
                
            if(RuntimePlatform.WebGLPlayer == Application.platform)
            {
                dataRepository = new PlayerPrefsDataRepository();
            }
            else
            {
                string saveFolderPath =
                    $"{(Application.isEditor ? Application.dataPath : Application.persistentDataPath)}/Saves";
        
                dataRepository = new LocalFileDataRepository(saveFolderPath, "json");
            }
            
            return new SaveLoadService(new JsonSerializer(), new MapDataKeysStorage(), dataRepository);
        }
    }
}