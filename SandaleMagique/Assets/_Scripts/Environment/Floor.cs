using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private GameObject m_player = null;
    private Animator m_playerAnimator = null;



    private void Start()
    {
        m_player = LevelManager.Instance.Player;
        m_playerAnimator = m_player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player)
        {
            m_playerAnimator.SetBool("Jump", false);
        }
    }

}
