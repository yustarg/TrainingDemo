using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;
        private Animator m_Animator;

        [SerializeField]
        private float m_MoveSpeed = 20;
        [SerializeField]
        private float m_MovingTurnSpeed = 360;
        [SerializeField]
        private float m_StationaryTurnSpeed = 180;

        float m_TurnAmount;
        float m_ForwardAmount;


        // Use this for initialization
        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 move)
        {
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);

            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();
            transform.Translate(move * Time.deltaTime * m_MoveSpeed);
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }
}