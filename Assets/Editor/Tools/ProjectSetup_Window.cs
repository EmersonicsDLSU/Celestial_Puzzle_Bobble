using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using GUILayout = UnityEngine.GUILayout;

namespace MyTools
{
    public class ProjectSetup_Window : EditorWindow
    {
        #region Variables

        private static ProjectSetup_Window win;
        private string _gameName = "Game";

        private int currentSelectionCount = 0;
        private GameObject wantedObject;

        #endregion

        #region Main Methods

        public static void InitWindow()
        {
            win = EditorWindow.GetWindow<ProjectSetup_Window>("Project setup");
            win.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Project Setup");
            _gameName = EditorGUILayout.TextField("Game Name: ", _gameName);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create Project Structure", GUILayout.Height(35), GUILayout.ExpandWidth(true)))
            {
                CreateProjectFolders();
            }

            if (win != null)
            {
                win.Repaint();
            }

            #region OR
            
            GetSelection();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();

            wantedObject = (GameObject)EditorGUILayout.ObjectField("Replace Objects: ", wantedObject, typeof(GameObject), true);

            EditorGUILayout.LabelField("Selection Count: " + currentSelectionCount.ToString(), EditorStyles.boldLabel);
            if (GUILayout.Button("Replace Selected Obj"))

            EditorGUILayout.Space();

            EditorGUILayout.EndVertical();

            if (win != null)
            {
                win.Repaint();
            }
            #endregion
        }

        #endregion

        #region ObjectReplacer

        void GetSelection()
        {
            currentSelectionCount = 0;
            currentSelectionCount = Selection.gameObjects.Length;
        }

        #endregion

        #region Custom Methods

        void CreateProjectFolders()
        {
            if (string.IsNullOrEmpty(_gameName)) return;

            if (_gameName == "Game")
            {
                if (!EditorUtility.DisplayDialog("Project Setup Warning",
                        "Do you really want to call your project 'Game'", "Yes", "No"))
                {
                    return;
                }
            }

            string assetPath = Application.dataPath;
            string rootPath = assetPath + "/" + _gameName;

            DirectoryInfo rootInfo = Directory.CreateDirectory(rootPath);
            AssetDatabase.Refresh();

            if (!rootInfo.Exists)
            {
                return;
            }
            CreateSubFolders(rootPath);

            CloseWindow();
        }

        void CreateSubFolders(string rootPath)
        {
            DirectoryInfo rootInfo = null;
            List<string> folderNames = new List<string>();

            rootInfo = Directory.CreateDirectory(rootPath + "/Art");

            if (rootInfo.Exists)
            {
                folderNames.Clear();
                folderNames.Add("Animation");
                folderNames.Add("Objects");
                folderNames.Add("Materials");
                folderNames.Add("Prefabs");

                CreateFolders(rootPath + "/Art", folderNames);
            }

            // Create Scenes
            DirectoryInfo sceneInfo = Directory.CreateDirectory(rootPath + "/Scenes");
            if (sceneInfo.Exists)
            {
                CreateScene(rootPath + "/Scenes", _gameName + "_Main");
                CreateScene(rootPath + "/Scenes", _gameName + "_Frontend");
                CreateScene(rootPath + "/Scenes", _gameName + "_Startup");
            }
        }

        void CreateFolders(string aPath, List<string> folders)
        {
            foreach (var folder in folders)
            {
                Directory.CreateDirectory(aPath + "/" + folder);
            }
        }

        void CreateScene(string aPath, string aName)
        {
            Scene curScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(curScene, aPath + "/" + aName + ".unity" , true);
        }

        void CloseWindow()
        {
            if (win)
            {
                win.Close();
            }
        }
        #endregion
    }
}
