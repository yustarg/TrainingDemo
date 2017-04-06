using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class GameMgr : MonoBehaviour
    {
        public string[] m_EnemyNames;
        public PlayerSpawner m_PlayerSpawner;
        public EnemySpawner m_EnemySpawner;
        
        // Use this for initialization
        void Start()
        {
            m_PlayerSpawner.LoadPlayer();
            m_EnemySpawner.LoadEnemy(m_EnemyNames);
            UIMgr.Instance.Init(m_EnemyNames);
        }
    }
}
