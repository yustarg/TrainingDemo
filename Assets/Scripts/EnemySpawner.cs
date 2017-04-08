using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float m_VisibleDistance = 35;       
        private GameObject m_EnemyPrefab;
        private List<GEEnemy> m_EnemyList;
        private GameObject m_Player;

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < m_EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(m_Player.transform.position, m_EnemyList[i].transform.position);
                if (distance > m_VisibleDistance)
                {
                    m_EnemyList[i].gameObject.SetActive(false);
                }
                else
                {
                    m_EnemyList[i].gameObject.SetActive(true);
                }
            }
        }

        public void LoadEnemy(string[] enemyNames)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_EnemyList = new List<GEEnemy>();
            m_EnemyPrefab = (GameObject)ResMgr.Instance.GetRes("Prefabs/Characters/Enemy.prefab");

            //Quaternion rotation
            for (int i = 0; i < enemyNames.Length; i++)
            {
                GameObject e = Instantiate(m_EnemyPrefab) as GameObject;
                e.transform.SetParent(transform);
                e.transform.name = enemyNames[i].GetHashCode().ToString();
                e.transform.localPosition = new Vector3(Random.RandomRange(-20f, 20f), 0, Random.RandomRange(-5f, 5f));
                e.transform.LookAt(m_Player.transform);
                GEEnemy enemy = e.GetComponent<GEEnemy>();
                enemy.Name = enemyNames[i];
                m_EnemyList.Add(enemy);
            }
        }

        public GEEnemy GetEnemyByName(string name)
        {
            for (int i = 0; i < m_EnemyList.Count; i++)
            {
                if (m_EnemyList[i].Name == name)
                {
                    return m_EnemyList[i];
                }
            }
            return null;
        }
    }
}