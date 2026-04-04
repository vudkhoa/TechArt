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
        }

        private void Update()
        {
            return;
            Object selected = Selection.activeObject;
            PrintGUID(selected.GameObject());
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
    }
}