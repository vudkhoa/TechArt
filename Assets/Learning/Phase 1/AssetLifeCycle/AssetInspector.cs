using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Learning.Phase1
{
    public class AssetInspector : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField] private GameObject go;

        // Runtime
        private static AssetInspector instance;

        private void Start()
        { 
            instance = this;
            PrintGUID();
            FindDependencies();
        }

        private void Update()
        {
            // return;
            Object selected = Selection.activeObject;
            // PrintGUID(selected.GameObject());
            // FindDependencies();
            // FindReferencesToThis();
        }

        // Find Guid
        [MenuItem("Tools/Print GUID")]
        static void PrintGUID(GameObject go = null)
        {
            if (go == null)
            {
                go = instance.go;
            }
            else
            {
                Debug.Log("Update");
            }

            string path = AssetDatabase.GetAssetPath(instance.go);
            string guid = AssetDatabase.AssetPathToGUID(path);

            Debug.Log($"Path: {path}");
            Debug.Log($"Guid: {guid}");


            string foundPath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log($"Found path from GUID: {foundPath}");
        }

        [MenuItem("Tools/Find Dependencies")]
        static void FindDependencies()
        {
            Object selected = Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(selected);
            
            string[] dependencies = AssetDatabase.GetDependencies(path);
            Debug.Log($"=== {path} depends on ===");
            foreach (string dep in dependencies)
                Debug.Log($"  -> {dep}");
        }

        [MenuItem("Tools/Find References To This")]
        static void FindReferencesToThis()
        {
            Object selected = Selection.activeObject;
            string selectedPath = AssetDatabase.GetAssetPath(selected);
            string selectedGUID = AssetDatabase.AssetPathToGUID(selectedPath);

            string[] allPaths = AssetDatabase.GetAllAssetPaths();
            foreach (string path in allPaths)
            {
                if (path == selectedPath) continue;

                string[] deps = AssetDatabase.GetDependencies(path, false);
                foreach (string dep in deps)
                {
                    if (dep == selectedPath)
                    {
                        Debug.Log($"Referenced by: {path}");
                        break;
                    }
                }
            }
        }

        /*Prefab: GameObject chứa con là TestObj:
            - Depen của GameObject: GameObject; TestObj.
            - Depen của TestObj:    TestObj.*/
    }
}