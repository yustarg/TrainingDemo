using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            m_Move = v * Vector3.forward + h * Vector3.right;
            m_Character.Move(m_Move);
        }
    }
}

