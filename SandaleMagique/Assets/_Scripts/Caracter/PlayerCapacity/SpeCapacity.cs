using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeCapacity : MonoBehaviour
{

    [SerializeField]
    private int m_gaugeBarMaxCapacity;
    [SerializeField]
    private int m_consomationRatePerUse;
    [SerializeField]
    private int m_refillRateModifier;
    [SerializeField]
    private float m_dashSpeedModifier;
    [SerializeField]
    private float m_waitTimeBeforeActivateDash;

    private InputManager m_manager;
    private Animator m_animator = null;
    private CharacterControler m_cControler;

    private int m_gaugeValue;
    public int GaugeValue { get { return m_gaugeValue; } }
    private bool m_dashHasBeenUsed = false;
    private Vector3 currentPos;

    private float m_characterSpeed;

    public void TriggerSpeCap()
    {
        if (!m_animator.GetBool("SpeCap"))
        {
            currentPos = transform.position;
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

        if (m_consomationRatePerUse > m_gaugeBarMaxCapacity)
            m_consomationRatePerUse = m_gaugeBarMaxCapacity;
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
        Vector3 pos = transform.position;
        if (m_animator.GetBool("SpeCap") && m_gaugeValue > 0 && !m_dashHasBeenUsed)
        {
            //remove gravity
            GetComponent<Rigidbody>().useGravity = false;

            transform.position = currentPos;
            Debug.DrawLine(pos, CalculateDashDir(pos), Color.red);
            if (Input.GetButtonUp("SpeCap"))
                m_dashHasBeenUsed = true;
        }
        else if (m_animator.GetBool("SpeCap") && m_gaugeValue > 0 && m_dashHasBeenUsed)
            UseDash(pos);
        else
        {
            Reset();
        }
    }

    private void UseDash(Vector3 pos)
    {
        m_cControler.EnableMoving(false);

        //set gravity
        if (!GetComponent<Rigidbody>().useGravity)
            GetComponent<Rigidbody>().useGravity = true;

        if (m_animator.GetBool("Jump"))
            m_animator.SetBool("Jump", false);

        if (m_gaugeValue > 0)
        {
            currentPos = pos;
            transform.position = CalculateDashDir(pos);
            m_gaugeValue -= m_consomationRatePerUse;
        }
        else if (m_gaugeValue <= 0)
        {
            Reset();
        }
    }

    private Vector3 CalculateDashDir(Vector3 pos)
    {
        Vector3 newPos = pos;
        if (m_cControler.HorizontalDir == E_Direction.RIGHT)
            newPos.x += m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime;
        else if (m_cControler.HorizontalDir == E_Direction.LEFT)
            newPos.x -= m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime;

        if (m_cControler.VerticalDir == E_Direction.UP)
            newPos.y += m_dashSpeedModifier * m_characterSpeed * Time.fixedDeltaTime;

        return newPos;
    }


    public void FillSpeCapBar(Vector3 oldPos, Vector3 currentPos)
    {
        if (oldPos.y > currentPos.y && m_gaugeValue < m_gaugeBarMaxCapacity)
        {
            m_gaugeValue += 1 * m_refillRateModifier;
        }

        if (m_gaugeValue > m_gaugeBarMaxCapacity)
        {
            m_gaugeValue = m_gaugeBarMaxCapacity;
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
        m_cControler.EnableMoving(true);
        m_dashHasBeenUsed = false;
        m_animator.SetBool("SpeCap", false);
        OnEnable();
    }
}
