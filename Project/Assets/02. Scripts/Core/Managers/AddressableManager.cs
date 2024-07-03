using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableManager
{
    public async static UniTask LoadAssetAsync(string path, Action<GameObject> onLoaded = null)
    {
        GameObject go = await Addressables.LoadAssetAsync<GameObject>(path);
        onLoaded?.Invoke(go);
    }
}
