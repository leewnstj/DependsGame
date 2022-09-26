using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //뭘 preload 할 것인지
    [SerializeField] private int poolSize = 10; //pooler 크기

    private List<GameObject> _pool; //배열로 만들어서 오브젝트 관리
    private GameObject _poolContainer; //pool에서 만든 오브젝트 구조화

    private void Awake()
    {
        _pool = new List<GameObject>();

        _poolContainer = new GameObject(name: $"Pool-{prefab.name}");

        CreatePooler();
    }

    private void CreatePooler()
    {
        for(int i = 0; i<poolSize; i++)
        {
            _pool.Add(item: createInstance());
        }
    }

    private GameObject createInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);

        newInstance.SetActive(false);

        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for(int i = 0; i<_pool.Count; i++)
        {
            if(!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return createInstance();
    }

    //Enemy를 Pool로 되돌리는 매소드
    public  static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }
}