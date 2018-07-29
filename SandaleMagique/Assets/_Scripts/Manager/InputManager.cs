using UnityEngine;
using UnityEngine.Events;


public class InputManager : MonoBehaviour {

    private static InputManager m_instance = null;
    public static InputManager Instance
    {
        get
        {
            if (m_instance == null)
                Debug.LogError("InputManager not created, returning null value");
            return m_instance;
        }
    }

    public UnityEvent E_aButton;
    public UnityEvent E_xButton;
    public UnityEvent E_noMove;
    public delegate void D_AxisEvent(float axisValue);
    public D_AxisEvent E_xAxis;
    public D_AxisEvent E_yAxis;

    [SerializeField, Range(0, 1)]
    private float m_xSensitivity = 0;


    private Animator m_animator = null;
    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        ProcessEvent();
    }



    private void ProcessEvent()
    {
        bool moving = ProcessXAxis();
        ProcessYAxis();

        ProcessAButton();
        ProcessXButton();

        if (!moving)
        {
            E_noMove.Invoke();
        }
    }

    private void ProcessAButton()
    {
        if (Input.GetButton("Jump") && E_aButton != null)
            E_aButton.Invoke();
    }

    private void ProcessXButton()
    {
        if (Input.GetButton("SpeCap") && E_xButton != null)
            E_xButton.Invoke();
    }


    private bool ProcessXAxis()
    {
        float xValue = Input.GetAxis("Horizontal");
        if (xValue >= m_xSensitivity || xValue <= -m_xSensitivity)
        {
            E_xAxis.Invoke(xValue);
            return true;
        }
        return false;
    }

    private void ProcessYAxis()
    {
        float yValue = Input.GetAxis("Vertical");
        if (yValue >= m_xSensitivity || yValue <= -m_xSensitivity)
            E_yAxis.Invoke(yValue);
    }

    public bool CheckRegisterAButton()
    {
        return E_aButton == null;
    }

    public bool CheckRegisterXButton()
    {
        return E_xButton == null;
    }
}
