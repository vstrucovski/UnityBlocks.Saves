using System;
using System.IO;
using UnityBlocks.SaveSystem.Data;
using UnityEditor;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Editor
{
    public class SavesHelpers
    {
        private static readonly string SavesFolderPath = Application.persistentDataPath + "/Saves/";
        private static readonly string BackupRootPath = Application.persistentDataPath + "/";

        [MenuItem(Constants.HELPER_MENU_PATH + "Open saves folder", priority = 0)]
        private static void OpenPersistentDataFolder()
        {
            OpenPersistentDataFolder(SavesFolderPath);
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

        [MenuItem(Constants.HELPER_MENU_PATH + "Delete all files", priority = 3)]
        private static void DeleteAllSaveFiles()
        {
            if (Directory.Exists(SavesFolderPath))
            {
                try
                {
                    var files = Directory.GetFiles(SavesFolderPath);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }

                    Debug.Log("All save files have been deleted.");
                }
                catch (IOException e)
                {
                    Debug.LogError("Error deleting save files: " + e.Message);
                }
            }
            else
            {
                Debug.LogWarning("Saves folder does not exist.");
            }
        }

        [MenuItem(Constants.HELPER_MENU_PATH + "Backup saves", priority = 2)]
        private static void BackupSaveFiles()
        {
            if (!Directory.Exists(SavesFolderPath))
            {
                Debug.LogWarning("Saves folder does not exist, nothing to backup.");
                return;
            }

            try
            {
                var timestamp = DateTime.Now.ToString("HH-mm-ss_dd-MM-yyyy");
                var backupFolderPath = Path.Combine(BackupRootPath, $"SaveBackup_{timestamp}");

                Directory.CreateDirectory(backupFolderPath);

                foreach (var file in Directory.GetFiles(SavesFolderPath))
                {
                    var destFile = Path.Combine(backupFolderPath, Path.GetFileName(file));
                    File.Copy(file, destFile, true);
                }

                Debug.Log($"Save files backed up successfully to: {backupFolderPath}");
            }
            catch (Exception e)
            {
                Debug.LogError("Error backing up save files: " + e.Message);
            }
        }
    }
}