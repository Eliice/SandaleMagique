using UnityEngine;

public class Jump : MonoBehaviour {

    [SerializeField]
    private float m_horizontalSpeed;
    [SerializeField]
    private float m_gravity;
    [SerializeField]
    private float m_jumpMaxHeigt;
    [SerializeField]
    private float m_jumpAngle;

    private Animator m_animator = null;
    
    private float m_timer = 0f;

    

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
        InputManager.Instance.E_aButton.AddListener(TriggerJump);
    }

    public void OnEnable()
    {
        InputManager.Instance.E_aButton.AddListener(TriggerJump);
    }

    public void OnDisable()
    {
        InputManager.Instance.E_aButton.RemoveListener(TriggerJump);
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("Jump"))
        {
            m_timer = 0;
            m_animator.SetBool("Jump", false);
            return;
        }

        Vector3 pos = transform.position;
        pos.y = -(m_gravity / 2) * Mathf.Pow(m_timer ,2) + m_horizontalSpeed * Mathf.Sin(m_jumpAngle) * m_timer + m_initY;
        transform.position = pos;

        m_timer += Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (InputManager.Instance.CheckRegisterAButton())
        {
            OnEnable();
            enabled = false;
        }

    }

    public void Reset()
    {
        m_timer = 0;
        m_animator.SetBool("Jump", false);
        OnEnable();
    }
}
