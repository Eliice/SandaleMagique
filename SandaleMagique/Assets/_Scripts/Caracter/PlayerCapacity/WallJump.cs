using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {


    [SerializeField]
    private AnimationCurve m_FallCurve;

    private Animator m_animator = null;

    private float m_timer = 0f;


    private Keyframe[] m_curveKeys;
    private float m_LastKeyTimer;

    private float m_initY = 0f;
    private Jump m_jumpCapacity = null;

    private bool m_againWall = false;


    [SerializeField]
    private float m_wallJumpCD = 0.2f;
    private float m_cdTimer = 0f;
    private bool m_wallTrigger = false;

    // Use this for initialization
    void Start () {
        m_jumpCapacity = GetComponent<Jump>();
        m_animator = GetComponent<Animator>();
    }

    public void TriggerWallSlide()
    {
        if (m_wallTrigger || m_againWall)
            return;
        m_againWall = true;
        m_initY = transform.position.y;
        //m_jumpCapacity.Reset();
        //m_animator.SetBool("WallJump", true);
    }

    public void DisableWallSlide()
    {
        m_againWall = false;
        //m_jumpCapacity.OnDisable();
        //m_animator.SetBool("WallJump", false);
        m_wallTrigger = true;
    }


    private void Update()
    {
        if(m_wallTrigger)
        {
            m_cdTimer += Time.deltaTime;
            if(m_cdTimer > m_wallJumpCD)
            {
                m_wallTrigger = false;
                m_cdTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if(m_againWall)
        {
            Vector3 pos = transform.position;
            pos.y = m_initY + m_FallCurve.Evaluate(m_timer);
            m_timer += Time.fixedDeltaTime;
        }
    }

}
