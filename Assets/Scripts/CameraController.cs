using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class CameraController : MonoBehaviour
    {
        private GameObject m_Target;
        public float m_ScrollSpeed;
        public float m_RotateSpeed;
        public float m_OffsetDistance;
        public float m_OffsetHeight;
        public float m_Smoothing;
        private Vector3 m_Offset;
        private Vector3 m_LastPosition;
        private Quaternion m_Rotation;

        private float m_TargetHeight;

        void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("Player");
            m_TargetHeight = m_Target.GetComponent<CapsuleCollider>().height;
            m_LastPosition = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_OffsetHeight, m_Target.transform.position.z - m_OffsetDistance);
            m_Offset = new Vector3(0, m_OffsetHeight, - m_OffsetDistance);
            m_Rotation = transform.rotation;
        }

        void Update()
        {
            //transform.position = new Vector3(Mathf.Lerp(m_LastPosition.x, m_Target.transform.position.x + m_Offset.x, m_Smoothing * Time.deltaTime),
            //                            Mathf.Lerp(m_LastPosition.y, m_Target.transform.position.y + m_Offset.y, m_Smoothing * Time.deltaTime),
            //                            Mathf.Lerp(m_LastPosition.z, m_Target.transform.position.z + m_Offset.z, m_Smoothing * Time.deltaTime));
            transform.position = m_Target.transform.position + m_Offset;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //transform.Translate((m_Target.transform.position - transform.position) * Time.deltaTime * m_ScrollSpeed, Space.World);
                transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_ScrollSpeed, Space.World);
                if (m_Offset.magnitude > 3)
                    m_Offset -= (transform.position - new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //transform.Translate((transform.position - m_Target.transform.position) * Time.deltaTime * m_ScrollSpeed, Space.World);
                transform.Translate(transform.TransformDirection(-Vector3.forward) * Time.deltaTime * m_ScrollSpeed, Space.World);
                if (m_Offset.magnitude < 10)
                    m_Offset += (transform.position - new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized;
                //m_Offset = transform.position - m_Target.transform.position;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, -m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position; 
            }

            //transform.position = m_Target.transform.position + m_Offset;
        }

        void LateUpdate()
        {
            m_LastPosition = transform.position;
            transform.LookAt(new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z));
        }
    }
}