using UnityEngine;

public class CameraControler : MonoBehaviour {

    private Vector3 m_smoothCamera;
    [SerializeField, Range(0, 1)]
    private float m_smoothTime;


    private GameObject m_player = null;
    private float m_initZ;

	// Use this for initialization
	void Start () {
        m_player = LevelManager.Instance.Player;
        m_initZ = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
	}


    void UpdatePosition()
    {
        Vector3 pos = m_player.transform.position;
        pos = Vector3.SmoothDamp(transform.position, pos, ref m_smoothCamera, m_smoothTime);
        pos.z = m_initZ;
        transform.position = pos;
    }

}
