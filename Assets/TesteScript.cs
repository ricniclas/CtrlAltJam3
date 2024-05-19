using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class TesteScript : MonoBehaviour
    {
        [SerializeField] CamsManager manager;  

        // Start is called before the first frame update
        void Start()
        {
        manager.Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("a")){
                Debug.Log("Teste");
                manager.checkCode("1AX");
            }
        }
    }
}
