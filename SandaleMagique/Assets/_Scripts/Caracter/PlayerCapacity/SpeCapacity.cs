using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCapacity : MonoBehaviour {

    [SerializeField]
    private int m_gaugeBarMaxCapacity;
    [SerializeField]
    private int m_consomationPerUse;
    [SerializeField]
    private int m_refillRateModifier;

    private Animator m_animator = null;
    private int m_gaugeValue;
    public int GaugeValue { get { return m_gaugeValue; } }

    public void TriggerSpeCap()
    {
        if (!m_animator.GetBool("SpeCap"))
        {
            m_animator.SetBool("Jump", false);
            m_animator.SetBool("SpeCap", true);
        }
        OnDisable();
    }


    private void Start()
    {
        m_animator = GetComponent<Animator>();
        InputManager.Instance.E_xButton.AddListener(TriggerSpeCap);

        if (m_consomationPerUse > m_gaugeBarMaxCapacity)
            m_consomationPerUse = m_gaugeBarMaxCapacity;
    }

    public void OnEnable()
    {
        InputManager.Instance.E_xButton.AddListener(TriggerSpeCap);
    }

    public void OnDisable()
    {
        InputManager.Instance.E_xButton.RemoveListener(TriggerSpeCap);
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("SpeCap"))
        {
            Reset();
            return;
        }

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
        m_animator.SetBool("SpeCap", false);
    }
}
