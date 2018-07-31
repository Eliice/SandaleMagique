﻿using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField]
    private float m_gravity;
    [SerializeField]
    private float m_jumpMaxHeigt;

    private Animator m_animator = null;

    private float m_timer = 0f;
    private float m_characterSpeed;
    private Vector3 m_oldPosition;

    private float m_initY = 0f;
    public void TriggerJump()
    {
        if (!m_animator.GetBool("Jump"))
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
            Reset();
            return;
        }

        Vector3 pos = transform.position;
        m_timer += Time.fixedDeltaTime;

        pos.y = CalculateDrop(m_timer) + m_initY;
        if (pos.y + m_initY <= m_jumpMaxHeigt)
        {
            transform.position = pos;
        }


        GetComponent<SpeCapacity>().FillSpeCapBar(m_oldPosition, pos);
        m_oldPosition = pos;
    }

    public float CalculateDrop(float timer)
    {
        return timer * ((-m_gravity * timer / 2) + m_characterSpeed);
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
