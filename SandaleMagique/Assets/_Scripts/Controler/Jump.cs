using UnityEngine;

public class Jump : MonoBehaviour {

    [SerializeField]
    private AnimationCurve m_jumpCurve;

    private Animator m_animator = null;
    
    private float m_timer = 0f;


    private Keyframe[] m_curveKeys;
    private float m_LastKeyTimer;


    private float m_initY = 0f;
    public void TriggerJump()
    {
        if(!m_animator.GetBool("Jump"))
        {
            m_animator.SetBool("Jump", true);
            m_initY = transform.position.y;
        }
        OnDisable();
    }


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_curveKeys = m_jumpCurve.keys;
        m_LastKeyTimer = m_curveKeys[m_curveKeys.Length-1].time;
        OnEnable();
    }

    public void OnEnable()
    {
        InputManager.Instance.AButton += TriggerJump;
    }

    public void OnDisable()
    {
        InputManager.Instance.AButton -= TriggerJump;
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("Jump"))
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.y = m_jumpCurve.Evaluate(m_timer) + m_initY;
        transform.position = pos;
        m_timer += Time.fixedDeltaTime;

        if (m_timer > m_LastKeyTimer)
        {
            m_timer = 0;
        }
    }

    


}
