using UnityEngine;
using UnityEditor;

namespace Importer
{
    public class ModelAutoImport : AssetPostprocessor
    {
        // Before Import
        private void OnPreprocessModel()
        {
            ModelImporter importer = (ModelImporter)assetImporter;
            importer.materialImportMode = ModelImporterMaterialImportMode.None;

            // Disable Camera + Light
            importer.importCameras = false;
            importer.importLights = false;

            /*if (!assetPath.Contains("@"))
            {
                importer.importAnimation = false;
            }*/

            Debug.Log($"[AutoImport] Refactor Model");
        }

        // After Import
        private void OnPostprocessModel(GameObject root)
        {

        }
    }
}