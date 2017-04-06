using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Training
{
    public class PlayerSpawner : MonoBehaviour
    {
        private GameObject m_PlayerGO;

        public void LoadPlayer()
        {
            GameObject playerPrefab = (GameObject)ResMgr.Instance.GetRes("Prefabs/Characters/Player");
            m_PlayerGO = Instantiate(playerPrefab) as GameObject;
            m_PlayerGO.transform.SetParent(transform);
            m_PlayerGO.transform.localPosition = new Vector3(0, 0, 0);
            m_PlayerGO.transform.rotation = Quaternion.identity;
        }
    }
}