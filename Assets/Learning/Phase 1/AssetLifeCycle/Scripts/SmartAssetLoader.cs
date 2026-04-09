using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SmartAssetLoader : MonoBehaviour
{
    // Load With Scene, Unload When Switch Scene
    [Serializable]
    public class SceneAssetManifest
    {
        public string[] preloadAddresses;
        public string[] lazyAddresses;
    }

    [SerializeField] private SceneAssetManifest _manifest;
    private List<AsyncOperationHandle> _sceneHandles = new List<AsyncOperationHandle>();

    private async Task Start()
    {
        foreach (string address in _manifest.preloadAddresses)
        {
            var handler = Addressables.LoadAssetAsync<System.Object>(address);
            await handler.Task;
            _sceneHandles.Add(handler);
        }
    }

    private void OnDestroy()
    {
        foreach (var handler in _sceneHandles) 
        { 
            if (handler.IsValid())
            {
                Addressables.Release(handler);
            }
        }

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    // Asset Pooling With Addressable
    private Dictionary<string, Queue<GameObject>> _pool = new();

    public async void SpawnFromPool(string address, Vector3 pos, Action<GameObject> onSpawn)
    {
        if (_pool.TryGetValue(address, out var queue) && queue.Count > 0)
        {
            GameObject pooled = queue.Dequeue();
            pooled.transform.position = pos;
            pooled.SetActive(true);
            onSpawn?.Invoke(pooled);
            return;
        }

        var handler = Addressables.InstantiateAsync(address, pos, Quaternion.identity);
        GameObject instance = await handler.Task;
        onSpawn?.Invoke(instance);
    }

    public void ReturnToPool(string address, GameObject instance)
    {
        instance.SetActive(false);

        if (!_pool.ContainsKey(address))
            _pool[address] = new Queue<GameObject>();

        _pool[address].Enqueue(instance);
    }
}
