using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class InputController : MonoBehaviour
    {

        private PlayerController m_Character;
        private Vector3 m_Move;

        // Use this for initialization
        void Start()
        {
            m_Character = GetComponent<PlayerController>();
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            m_Move = v * Vector3.forward + h * Vector3.right;
            m_Character.Move(m_Move);
        }
    }
}

