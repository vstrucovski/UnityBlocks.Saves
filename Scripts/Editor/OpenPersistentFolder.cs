using System.IO;
using UnityBlocks.SaveSystem.Data;
using UnityEditor;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Editor
{
    public class OpenPersistentFolder
    {
        [MenuItem(Constants.CONFIG_MENU_PATH + "Saves/Open saves folder")]
        private static void OpenPersistentDataFolder()
        {
            var folderPath = Application.persistentDataPath + "/Saves/";
            OpenPersistentDataFolder(folderPath);
        }

        private static void OpenPersistentDataFolder(string path)
        {
            if (Directory.Exists(path))
            {
                EditorUtility.RevealInFinder(path);
            }
            else
            {
                Debug.LogError("Persistent data folder does not exist: " + path);
            }
        }
    }
}