using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class DroneFixedRotation : MonoBehaviour
    {

        void Update()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
