using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("NormalShoot")]
    public int amountToPoolnormalShot = 3;
    public static ObjectPool instance;
    public Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] public GameObject normalShootprefab;


    //chargeShoot
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        for (int i = 0; i < amountToPoolnormalShot; i++)
        {
            var newObj = NewObjct();
            newObj.SetActive(false);
            pool.Enqueue(newObj);

        }
        


    }

    GameObject NewObjct()
    {
        return Instantiate(normalShootprefab);
    }
    public GameObject GetNormalShootPool()
    {

        int initialCount = pool.Count;
        for (int i = 0; i < initialCount; i++)
        {
            GameObject obj = pool.Dequeue();
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
            pool.Enqueue(obj);
        }
        return null;
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    void CreatePool()
    {
        for (int i = 0; i < amountToPoolnormalShot; i++)
        {
            var newObj = NewObjct();
            newObj.SetActive(false);
            pool.Enqueue(newObj);

        }
    }

}