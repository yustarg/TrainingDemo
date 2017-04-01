using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class CameraController : MonoBehaviour
    {
        private GameObject m_Target;

        public float m_RotateSpeed;
        public float m_OffsetDistance;
        public float m_OffsetHeight;
        public float m_Smoothing;
        private Vector3 m_Offset;
        private Vector3 m_LastPosition;

        private float m_TargetHeight;

        void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("Player");
            m_TargetHeight = m_Target.GetComponent<CapsuleCollider>().height;
            m_LastPosition = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_OffsetHeight, m_Target.transform.position.z - m_OffsetDistance);
            m_Offset = new Vector3(0, m_OffsetHeight, - m_OffsetDistance);
        }

        void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Translate((m_Target.transform.position - transform.position) * Time.deltaTime * 10, Space.World);
                m_Offset = transform.position - m_Target.transform.position;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Translate((transform.position - m_Target.transform.position) * Time.deltaTime * 10, Space.World);
                m_Offset = transform.position - m_Target.transform.position;
            } 
            if (Input.GetKey(KeyCode.Q))
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, -m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position; 
            }
            else
            {
                transform.position = m_Target.transform.position + m_Offset;
                transform.position = new Vector3(Mathf.Lerp(m_LastPosition.x, m_Target.transform.position.x + m_Offset.x, m_Smoothing * Time.deltaTime),
                                            Mathf.Lerp(m_LastPosition.y, m_Target.transform.position.y + m_Offset.y, m_Smoothing * Time.deltaTime),
                                            Mathf.Lerp(m_LastPosition.z, m_Target.transform.position.z + m_Offset.z, m_Smoothing * Time.deltaTime));
            }
        }

        void LateUpdate()
        {
            m_LastPosition = transform.position;
            transform.LookAt(m_Target.transform.position);
        }
    }
}