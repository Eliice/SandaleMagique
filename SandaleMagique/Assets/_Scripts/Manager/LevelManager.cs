using UnityEngine;

public class LevelManager : MonoBehaviour {


    private static LevelManager m_instance = null;
    public static LevelManager Instance
    {
        get
        {
            if (m_instance == null)
                Debug.LogError("LevelManager not created, returning null value");
            return m_instance;
        }
    }


    private GameObject m_player = null;
    public GameObject Player { get { return m_player; } }



	void Awake () {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_instance = this;
	}
	
}
