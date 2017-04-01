using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class DrawGizmos : MonoBehaviour
    {

        public Vector3 size;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + size.y / 2, transform.position.z);
            Gizmos.DrawCube(pos, size);
        }
    }
}