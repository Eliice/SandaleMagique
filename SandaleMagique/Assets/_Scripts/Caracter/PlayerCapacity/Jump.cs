using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField]
    private float m_gravity;
    [SerializeField]
    private float m_jumpMaxHeigt;
    [SerializeField]
    private float m_gravityModifier;

    private Animator m_animator = null;

    private float m_timer = 0f;
    private float m_characterSpeed;
    private Vector3 m_oldPosition;

    private SpeCapacity m_speCap;
    private bool jumpAllowed = true;

    private float m_initY = 0f;
    public void TriggerJump()
    {
        if (!m_animator.GetBool("Jump") && jumpAllowed)
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
        m_characterSpeed = gameObject.GetComponent<Character>().Speed;
        m_speCap = GetComponent<SpeCapacity>();
    }

    public void OnEnable()
    {
        InputManager.Instance.E_aButton.AddListener(TriggerJump);
    }

    public void OnDisable()
    {
        jumpAllowed = false;
        InputManager.Instance.E_aButton.RemoveListener(TriggerJump);
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("Jump") && jumpAllowed)
        {
            Reset();
            return;
        }

        OnDisable();
        Vector3 pos = transform.position;
        m_timer += Time.fixedDeltaTime;

        pos.y = CalculateDrop(m_timer) + m_initY;
        if (pos.y < m_jumpMaxHeigt + m_initY && !m_animator.GetBool("SpeCap"))
        {
            transform.position = pos;
        }

        m_speCap.FillSpeCapBar(m_oldPosition, pos);
        m_oldPosition = pos;
    }

    public float CalculateDrop(float timer)
    {
        return timer * ((-m_gravity / m_gravityModifier * timer ) + m_characterSpeed);
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
        jumpAllowed = true;
        m_timer = 0;
        m_animator.SetBool("Jump", false);
        OnEnable();
    }
}
