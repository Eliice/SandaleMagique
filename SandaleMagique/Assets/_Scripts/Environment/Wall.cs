using UnityEngine;

public class Wall : MonoBehaviour {


    private GameObject m_player = null;
    private Animator m_playerAnimator = null;
    private WallJump m_wallJumpCapacity = null;



    private void Start()
    {
        m_player = LevelManager.Instance.Player;
        m_playerAnimator = m_player.GetComponent<Animator>();
        m_wallJumpCapacity = m_player.GetComponent<WallJump>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_wallJumpCapacity.TriggerWallSlide();
    }

    private void OnTriggerExit(Collider other)
    {
        m_wallJumpCapacity.DisableWallSlide();
    }
}
