using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    private GameObject m_PlayerPrefab;
    private GameObject m_PlayerGO;

	// Use this for initialization
	void Start () {
        
	}

    public void LoadPlayer()
    {
        m_PlayerPrefab = Resources.Load<GameObject>("Prefabs/Characters/Player");
        m_PlayerGO = Instantiate(m_PlayerPrefab, transform) as GameObject;
        m_PlayerGO.transform.SetParent(transform);
        m_PlayerGO.transform.localPosition = new Vector3(0, 0, 0);
        m_PlayerGO.transform.rotation = Quaternion.identity;
    }
}
