using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private GameObject m_player = null;
    private Animator m_playerAnimator = null;
    private Jump m_jumpCapacity = null;



    private void Start()
    {
        m_player = LevelManager.Instance.Player;
        m_playerAnimator = m_player.GetComponent<Animator>();
        m_jumpCapacity = m_player.GetComponent<Jump>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player)
        {
            m_playerAnimator.SetBool("Jump", false);
            m_jumpCapacity.OnEnable();
        }
    }

}
