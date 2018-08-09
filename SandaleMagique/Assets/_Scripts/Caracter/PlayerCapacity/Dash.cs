using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    [SerializeField]
    private float m_dashSpeedMultiplier = 1.5f;

    private InputManager m_inputManager = null;
    private Animator m_animator = null;
    private CharacterControler m_characterController = null;
    private float m_characterSpeed = 1;
    private FallEnergie m_energiePool = null;
    private Rigidbody m_body = null;

    private Vector3 m_freezePosition = new Vector3();
    private bool m_dashHasBeenUsed = false;

    void Start () {
        m_animator = GetComponent<Animator>();
        m_inputManager = InputManager.Instance;
        m_characterController = gameObject.GetComponent<CharacterControler>();
        m_characterSpeed = GetComponent<Character>().Speed * m_dashSpeedMultiplier;
        m_energiePool = GetComponent<FallEnergie>();
        m_body = GetComponent<Rigidbody>();
        OnEnable();
    }


    public void OnEnable()
    {
        m_inputManager.E_xButton.AddListener(TriggerSpeCap);
    }

    public void OnDisable()
    {
        m_inputManager.E_xButton.RemoveListener(TriggerSpeCap);
    }

    public void TriggerSpeCap()
    {
        if (!m_animator.GetBool("SpeCap") && m_energiePool.getEnergie() > 0)
        {
            m_animator.SetBool("SpeCap", true);
            m_body.useGravity = false;
            m_animator.SetBool("Jump", false);
            m_characterController.EnableMoving(false);
            m_freezePosition = transform.position;
        }
        OnDisable();
    }

    private void Update()
    {
        if (Input.GetButtonUp("SpeCap") && !m_dashHasBeenUsed)
        {
            m_dashHasBeenUsed = true;
            ExecDash();
        }
    }

    private void FixedUpdate()
    {
        if(m_animator.GetBool("SpeCap") && !m_dashHasBeenUsed)
        {
            transform.position = m_freezePosition;
            return;
        }
    }

    private void ExecDash()
    {
        m_body.useGravity = true;
        Vector3 dashDirection = CalculateDashDir(transform.position);
        m_body.AddForce(dashDirection* m_energiePool.getEnergie() * m_energiePool.ForceMultiplicator, ForceMode.Impulse);
    }


    private Vector3 CalculateDashDir(Vector3 pos)
    {
        Vector3 newPos = pos;

            int x=0;
            int y = 0;
            if (Input.GetAxis("Horizontal") > 0)
                x = 1;
            else if (Input.GetAxis("Horizontal") < 0)
                x = -1;

            if (Input.GetAxis("Vertical") > 0)
                y = 1;
            else if (Input.GetAxis("Vertical") < 0)
                y = -1;
            newPos.x = m_characterSpeed * Time.fixedDeltaTime * x;
            newPos.y = -1 * m_characterSpeed * Time.fixedDeltaTime *y;
        return newPos;
    }

    public void Reset()
    {
        m_animator.SetBool("SpeCap", false);
        m_dashHasBeenUsed = false;
        m_characterController.EnableMoving(true);
        OnEnable();
    }

}
