using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private float m_VisibleDistance = 20;       
        private GameObject m_EnemyPrefab;
        private List<GameObject> m_EnemyList;
        private GameObject m_Player;

        // Use this for initialization
        void Start()
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_EnemyList = new List<GameObject>();
            m_EnemyPrefab = Resources.Load<GameObject>("Prefabs/Characters/Enemy");

            //Quaternion rotation
            for (int i = 0; i < 3; i++)
            {
                GameObject e = Instantiate(m_EnemyPrefab, transform) as GameObject;
                e.transform.localPosition = new Vector3(Random.RandomRange(-20f, 20f), 0, Random.RandomRange(-5f, 5f));
                e.transform.LookAt(m_Player.transform);
                m_EnemyList.Add(e);
            }
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < m_EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(m_Player.transform.position, m_EnemyList[i].transform.position);
                if (distance > m_VisibleDistance)
                {
                    m_EnemyList[i].SetActive(false);
                }
                else
                {
                    m_EnemyList[i].SetActive(true);
                }
            }
        }
    }
}