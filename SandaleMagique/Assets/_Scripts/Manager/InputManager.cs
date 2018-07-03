using UnityEngine;


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

    public delegate void InputHandler();
    public delegate void AxisHandler(float axisValue);
    public event AxisHandler xAxis;
    public event InputHandler AButton;

    [SerializeField, Range(0, 1)]
    private float m_xSensitivity = 0;


    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
            Destroy(gameObject);
    }

	void Update () {
        ProcessEvent();
	}



    private void ProcessEvent()
    {
        bool moving = ProcessXAxis();
        ProcessAButton();

        if(!moving)
        {
            ///TODO
            ///Handle back to idle state
        }
    }

    private void ProcessAButton()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            AButton();
    }

    private bool ProcessXAxis()
    {
        float xValue = Input.GetAxis("Horizontal");
        if (xValue >= m_xSensitivity || xValue <= -m_xSensitivity)
        {
            xAxis(xValue);
            return true;
        }
        return false;

    }
}
