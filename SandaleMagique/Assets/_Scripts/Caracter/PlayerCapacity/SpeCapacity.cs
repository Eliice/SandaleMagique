using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCapacity : MonoBehaviour
{

    [SerializeField]
    private int m_gaugeBarMaxCapacity;
    [SerializeField]
    private int m_consomationPerUse;
    [SerializeField]
    private int m_refillRateModifier;
    [SerializeField]
    private float m_dashSpeedModifier;

    private InputManager m_manager;
    private Animator m_animator = null;
    private CharacterControler m_cControler;

    private int m_gaugeValue;
    public int GaugeValue { get { return m_gaugeValue; } }

    private float m_characterSpeed;

    public void TriggerSpeCap()
    {
        if (!m_animator.GetBool("SpeCap"))
        {
            m_animator.SetBool("SpeCap", true);
        }
        OnDisable();
    }


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_manager = InputManager.Instance;
        OnEnable();
        m_cControler = gameObject.GetComponent<CharacterControler>();
        m_characterSpeed = GetComponent<Character>().Speed;

        if (m_consomationPerUse > m_gaugeBarMaxCapacity)
            m_consomationPerUse = m_gaugeBarMaxCapacity;
    }

    public void OnEnable()
    {
        m_manager.E_xButton.AddListener(TriggerSpeCap);
    }

    public void OnDisable()
    {
        m_manager.E_xButton.RemoveListener(TriggerSpeCap);
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("SpeCap"))
        {
            Reset();
            return;
        }
        else
        {
            //do movement
            Vector3 pos = transform.position;
            switch (m_cControler.HorizontalDir)
            {
                case E_Direction.RIGHT: pos.x += m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime; break;
                case E_Direction.LEFT: pos.x -= m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime; break;
            }

            if (m_cControler.VerticalDir > 0)
            {
                pos.y += m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime;
            }

            transform.position = pos;
        }
    }

    private void Update()
    {
        if (m_manager.CheckRegisterXButton())
        {
            OnEnable();
            enabled = false;
        }
    }

    public void Reset()
    {
        m_animator.SetBool("SpeCap", false);
        //OnEnable();
    }
}
