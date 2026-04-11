using UnityEngine;
using UnityEditor;

namespace Learning.Phase1
{
    public class AudioAutoImporter : AssetPostprocessor
    {
        private void OnPreprocessAudio()
        {
            // Get Import
            AudioImporter importer = (AudioImporter)assetImporter;
            string path = AssetDatabase.GetAssetPath(importer);

            // Get Size
            long fileSize = new System.IO.FileInfo(path).Length;

            // Get Settings
            AudioImporterSampleSettings settings = importer.defaultSampleSettings;

            // Rule 1: File < 200KB --> SFX
            if (fileSize < 200 * 1024)
            {
                settings.loadType = AudioClipLoadType.DecompressOnLoad;
            }
            else if (fileSize >= 200 * 1024)
            {
                settings.loadType = AudioClipLoadType.Streaming;
            }

            importer.forceToMono = true;
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            settings.quality = 0.7f;

            importer.defaultSampleSettings = settings;
            importer.SaveAndReimport();
        }
    }
}