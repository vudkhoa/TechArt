using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    private List<AsyncOperationHandle> _loadedHandles = new List<AsyncOperationHandle>();

    async void LoadSingleAsset()
    {
        AsyncOperationHandle<Texture2D> handle = 
            Addressables.LoadAssetAsync<Texture2D>("hero_portrait");

        Texture2D texture = await handle.Task;

        _loadedHandles.Add(handle);

        GetComponent<Renderer>().material.mainTexture = texture;
    }

    async void LoadAndSpawnCharacter()
    {
        AsyncOperationHandle<GameObject> handle =
            Addressables.InstantiateAsync("character-hero", transform.position, Quaternion.identity);

        GameObject instance = await handle.Task;

        _loadedHandles.Add(handle);

        // Not Using
        // Addressables.ReleaseInstance(instance);
        // Reduce Ref Count, if count = 0 --> Ram Unload.
    }

    // Addressable không biết sẽ trả về gì --> Chỉ đảm bảo trả về 1 dạng list có thể truy cập bằng Index
    // --> Dùng IList
    async void LoadMultipleAssets()
    {
        AsyncOperationHandle<IList<GameObject>> handle =
            Addressables.LoadAssetsAsync<GameObject>(
                "level_1", // Label
                (GameObject prefab) => 
                {
                    // Callback When Done load one Asset
                    Debug.Log($"Loaded: {prefab.name}");
                });

        IList<GameObject> allPrefabs = await handle.Task;
        _loadedHandles.Add(handle);

        Debug.Log($"Total loaded: {allPrefabs.Count}");
    }

    private void OnDestroy()
    {
        foreach (var handle in _loadedHandles) 
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }
        _loadedHandles.Clear();
    }
}

/*### 3.5 Reference Counting — Co che chong Memory Leak

```
Buoc 1: LoadAssetAsync("hero_texture")
  ->Bundle "characters" duoc load vao RAM
  -> hero_texture RefCount = 1

Buoc 2: LoadAssetAsync("hero_texture")(load lai lan nua)
  ->Bundle DA CO trong RAM, khong load lai
  -> hero_texture RefCount = 2

Buoc 3: Release(handle1)
  ->hero_texture RefCount = 1
  ->Bundle VAN CON trong RAM (vi con ref)

Buoc 4: Release(handle2)
  ->hero_texture RefCount = 0
  ->Bundle duoc UNLOAD khoi RAM
```

**Loi thuong gap:**Quen Release->RefCount khong bao gio ve 0 -> Memory leak
**Cach phat hien:**Addressables Event Viewer
*(Window > Asset Management > Addressables > Event Viewer)*/