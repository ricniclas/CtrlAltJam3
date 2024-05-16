using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {

        public static T instance;
        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
}
