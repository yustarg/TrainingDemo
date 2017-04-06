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
        private Vector2 m_LastMousePosition;

        private float m_TargetHeight;

        void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("Player");
            m_TargetHeight = m_Target.GetComponent<CapsuleCollider>().height;
            m_LastPosition = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_OffsetHeight, m_Target.transform.position.z - m_OffsetDistance);
            m_Offset = new Vector3(0, m_OffsetHeight, - m_OffsetDistance);
            m_Rotation = transform.rotation;
            m_LastMousePosition = new Vector2();
        }

        void OnEnable()
        {
            EasyTouch.On_Swipe += On_Swipe;
            EasyTouch.On_PinchIn += On_PinchIn;     //挤入
            EasyTouch.On_PinchOut += On_PinchOut;   //挤出
        }

        void OnDisable()
        {
            UnsubscribeEvent();
        }

        void UnsubscribeEvent()
        {
            EasyTouch.On_Swipe -= On_Swipe;
            EasyTouch.On_PinchIn -= On_PinchIn;
            EasyTouch.On_PinchOut -= On_PinchOut;
        }

        private void On_Swipe(Gesture gesture)
        {
            // the world coordinate from touch for z=5
            transform.position = m_Target.transform.position + m_Offset;            
            Vector2 delta = gesture.deltaPosition;
            transform.RotateAround(m_Target.transform.position, Vector3.up, delta.x * m_RotateSpeed * Time.deltaTime);
            m_Offset = transform.position - m_Target.transform.position;
        }

        private void On_PinchIn(Gesture gesture)
        {
            transform.position = m_Target.transform.position + m_Offset;
            if (m_Offset.magnitude > 3)
                m_Offset -= (transform.position - new Vector3(m_Target.transform.position.x,
                    m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized * 0.1f;
        }

        private void On_PinchOut(Gesture gesture)
        {
            transform.position = m_Target.transform.position + m_Offset;
            if (m_Offset.magnitude < 10)
                m_Offset += (transform.position - new Vector3(m_Target.transform.position.x,
                    m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized * 0.1f;
        }

        void Update()
        {
            /*  鼠标操作
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //transform.Translate(transform.TransformDirection(Vector3.forward) * Time.deltaTime * m_ScrollSpeed, Space.World);
                if (m_Offset.magnitude > 3)
                    m_Offset -= (transform.position - new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //transform.Translate(transform.TransformDirection(-Vector3.forward) * Time.deltaTime * m_ScrollSpeed, Space.World);
                if (m_Offset.magnitude < 10)
                    m_Offset += (transform.position - new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized;
            }
            else if (Input.mousePosition.x - m_LastMousePosition.x < 0)
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, -m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position;
            }
            else if (Input.mousePosition.x - m_LastMousePosition.x > 0)
            {
                transform.RotateAround(m_Target.transform.position, Vector3.up, m_RotateSpeed * Time.deltaTime);
                m_Offset = transform.position - m_Target.transform.position;
            }
            m_LastMousePosition = Input.mousePosition;
            */
        }

        void LateUpdate()
        {
            //m_LastPosition = transform.position;
            transform.position = m_Target.transform.position + m_Offset;
            transform.LookAt(new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z));
        }
    }
}