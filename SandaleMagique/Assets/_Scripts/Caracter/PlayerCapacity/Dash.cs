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


    void Start () {
        m_animator = GetComponent<Animator>();
        m_inputManager = InputManager.Instance;
        m_characterController = gameObject.GetComponent<CharacterControler>();
        m_characterSpeed = GetComponent<Character>().Speed * m_dashSpeedMultiplier;
        m_energiePool = GetComponent<FallEnergie>();
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
        }
        OnDisable();
    }

}
