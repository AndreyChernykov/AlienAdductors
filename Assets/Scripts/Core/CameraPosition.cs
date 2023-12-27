using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class CameraPosition : MonoBehaviour
    {
        private void OnEnable()
        {
            Camera.main.transform.position = transform.position;
        }
    }
}

