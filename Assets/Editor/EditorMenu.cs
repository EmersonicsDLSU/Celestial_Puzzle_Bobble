using UnityEngine;
using UnityEditor;

namespace MyTools
{
    public class EditorMenu
    {
        [MenuItem("MyTools/Project/Project Setup Tool")]
        public static void InitProjectSetupTool()
        {
            //Debug.Log("Project Setup Tool!");
            ProjectSetup_Window.InitWindow();
        }
    }
}
