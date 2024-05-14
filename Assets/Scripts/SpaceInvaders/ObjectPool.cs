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

        Debug.Log(pool);


    }

    GameObject NewObjct()
    {
        return Instantiate(normalShootprefab);
    }
    public GameObject GetNormalShootPool()
    {


        if (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            //var newObj = NewObjct();
            //newObj.SetActive(true);
            return null;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}