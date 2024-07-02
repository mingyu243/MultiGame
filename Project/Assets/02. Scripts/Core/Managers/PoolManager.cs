using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class PoolManager
{
    Dictionary<string, IObjectPool<GameObject>> _objPoolDict = new();

    public void Init()
    {
        // 씬이 바뀌면, 풀링한 오브젝트들의 참조가 모두 끊어지므로 초기화해준다.
        SceneManager.sceneLoaded += Clear;
    }

    void Clear(Scene scene, LoadSceneMode mode)
    {
        _objPoolDict.Clear();
    }

    /// <summary>
    /// 오브젝트를 풀링 시스템에 등록하고 싶을 때.
    /// </summary>
    public void Register(string key, GameObject prefab, int capacity = 5, int preCreateCount = 0, Transform preCreateParent = null)
    {
        if (_objPoolDict.ContainsKey(key) == false)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                                                createFunc: () => GameObject.Instantiate(prefab),
                                                actionOnGet: (go) => go.SetActive(true),
                                                actionOnRelease: (go) => go.SetActive(false),
                                                defaultCapacity: capacity
                                            );

            _objPoolDict.Add(key, pool);
        }

        PreCreate(key, preCreateCount, preCreateParent);
    }

    /// <summary>
    /// 풀링한 오브젝트를 가져올 때.
    /// </summary>
    public GameObject Get(string key, Transform parent = null)
    {
        if (_objPoolDict.ContainsKey(key) == false)
        {
            return null;
        }
        
        GameObject go = _objPoolDict[key].Get();
        go.transform.SetParent(parent);

        return go;
    }

    /// <summary>
    /// 풀링한 오브젝트를 반환할 때.
    /// </summary>
    public void Release(string key, GameObject go)
    {
        if (_objPoolDict.ContainsKey(key) == false)
        {
            return;
        }

        _objPoolDict[key].Release(go);
    }

    #region Private Methods

    void PreCreate(string key, int count, Transform parent = null)
    {
        if (count <= 0)
        {
            return;
        }

        // 미리 생성.
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject go = Get(key, parent);
            list.Add(go);
        }
        for (int i = 0; i < count; i++)
        {
            Release(key, list[i]);
        }
    }

    #endregion
}
