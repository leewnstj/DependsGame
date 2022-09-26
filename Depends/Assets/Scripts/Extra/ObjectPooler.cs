using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //�� preload �� ������
    [SerializeField] private int poolSize = 10; //pooler ũ��

    private List<GameObject> _pool; //�迭�� ���� ������Ʈ ����
    private GameObject _poolContainer; //pool���� ���� ������Ʈ ����ȭ

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

    //Enemy�� Pool�� �ǵ����� �żҵ�
    public  static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }
}