using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class CameraController : MonoBehaviour
    {
        private GameObject m_Target;
        public float m_ScrollSpeed;
        public Vector2 m_RotateSpeed;
        public float m_OffsetDistance;
        public float m_OffsetHeight;
        public float m_Smoothing;
        private Vector3 m_Offset;
        private Quaternion m_Rotation;
        private Vector2 m_LastMousePosition;

        private float m_TargetHeight;
        private float m_RotateZ;
        private float m_Rx;
        private float m_Ry;
        private float m_MinLimitY = -70f;
        private float m_MaxLimitY = 60f;

        void Start()
        {
            m_Target = GameObject.FindGameObjectWithTag("Player");
            m_TargetHeight = m_Target.GetComponent<CapsuleCollider>().height;
            m_Offset = new Vector3(0, m_OffsetHeight, -m_OffsetDistance);
            m_Rotation = transform.rotation;
            m_LastMousePosition = new Vector2();
            m_Rx = transform.eulerAngles.x;
            m_Ry = transform.eulerAngles.y;
            m_RotateZ = -m_OffsetDistance;
        }

        void OnEnable()
        {
            EasyTouch.On_Swipe += On_Swipe;
            EasyTouch.On_PinchIn += On_PinchIn;     //挤入
            EasyTouch.On_PinchOut += On_PinchOut;   //挤出
            EasyTouch.On_PinchEnd += On_PinchEnd;
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
            EasyTouch.On_PinchEnd -= On_PinchEnd;
        }

        private void On_Swipe(Gesture gesture)
        {
            if (isPinching) return;

            Vector3 playerPos = new Vector3(m_Target.transform.position.x,
                                            m_Target.transform.position.y + m_TargetHeight,
                                            m_Target.transform.position.z);
            Vector2 delta = gesture.deltaPosition;            
            m_Rx += delta.x * m_RotateSpeed.x * Time.deltaTime;
            m_Ry -= delta.y * m_RotateSpeed.y * Time.deltaTime;
            m_Ry = ClampAngle(m_Ry, m_MinLimitY, m_MaxLimitY);
            m_Rotation = Quaternion.Euler(m_Ry, m_Rx, 0);
            transform.rotation = m_Rotation;
            //m_RotateZ = -Mathf.Sqrt(m_Offset.z * m_Offset.z + m_Offset.x * m_Offset.x);
            Vector3 mPosition = m_Rotation * new Vector3(0, 0, m_RotateZ) + playerPos;
            m_Offset = mPosition - m_Target.transform.position;
            CheckWall();
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }

        bool isPinching = false;
        private void On_PinchIn(Gesture gesture)
        {
            //print("On_PinchIn");
            isPinching = true;
            if (m_Offset.magnitude > 3)
            {
                Vector3 playerPos = new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z);
                m_Offset -= (transform.position - playerPos).normalized * 0.2f;
                m_RotateZ = -Vector3.Distance(playerPos, transform.position);
            }
                
        }

        private void On_PinchOut(Gesture gesture)
        {
            isPinching = true;
            if (m_Offset.magnitude < 10)
            {
                Vector3 playerPos = new Vector3(m_Target.transform.position.x,
                        m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z);
                m_Offset += (transform.position - playerPos).normalized * 0.2f;
                m_RotateZ = -Vector3.Distance(playerPos, transform.position);
            }
                //m_Offset += (transform.position - new Vector3(m_Target.transform.position.x,
                //    m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z)).normalized * 0.2f;
        }

        private void On_PinchEnd(Gesture gesture)
        {
            //print("On_PinchEnd");
            isPinching = false;
        }

        void Update()
        {
            
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
                /*  鼠标操作
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

        bool CheckWall()
        {
            bool isCollide = false;
            Vector3 playerPosition = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_TargetHeight, m_Target.transform.position.z);
            Quaternion cr = transform.rotation;
            Vector3 positon = playerPosition + (cr * Vector3.back * 8);
            //RaycastHit[] hits = Physics.RaycastAll(new Ray(playerPosition, (positon - playerPosition).normalized), 30, LayerMask.NameToLayer("Terrain"));
            RaycastHit hit;
            bool isHit = Physics.Raycast(new Ray(playerPosition, (positon - playerPosition).normalized), out hit, 30, LayerMask.NameToLayer("Terrain"));
            float distance = Vector3.Distance(transform.position, playerPosition) + 0.5f;
            float hitDistance = 0;
            if (isHit)
            {
                hitDistance = Vector3.Distance(hit.point, playerPosition);
                if (hitDistance > m_Offset.magnitude) return false;
                
                hitDistance -= 1;
                if ((hitDistance) <= distance)
                {
                    isCollide = true;
                }

                if (isCollide)
                {
                    Debug.DrawRay(playerPosition, positon - playerPosition, Color.red);
                    positon = playerPosition + (cr * Vector3.back * hitDistance);
                    transform.position = positon;
                }
            }
            return isCollide;
        }

        void LateUpdate()
        {
            if (!CheckWall())
            {
                Vector3 playerPosition = new Vector3(m_Target.transform.position.x, 
                                                    m_Target.transform.position.y + m_TargetHeight,
                                                    m_Target.transform.position.z);
                transform.position = m_Target.transform.position + m_Offset;
                transform.LookAt(playerPosition);
            }
        }
    }
}