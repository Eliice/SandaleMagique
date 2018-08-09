﻿using System.Collections;
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
        if (Input.GetButtonUp("SpeCap"))
        {
            m_energiePool.ShoudRemove = true;
            m_dashHasBeenUsed = true;
        }
    }

    private void FixedUpdate()
    {
        if(m_animator.GetBool("SpeCap") && !m_dashHasBeenUsed)
        {
            transform.position = m_freezePosition;
            return;
        }
        if (m_dashHasBeenUsed && m_energiePool.getEnergie() > 0 )
        {
            transform.position = CalculateDashDir(transform.position);
            
        }
        else if(m_dashHasBeenUsed && !(m_energiePool.getEnergie() > 0))
        {
            m_body.useGravity = true;
            m_characterController.EnableMoving(true);
        }
    }

    private Vector3 CalculateDashDir(Vector3 pos)
    {
        Vector3 newPos = pos;
        if (m_characterController.HorizontalDir == E_Direction.RIGHT)
            newPos.x += m_characterSpeed * Time.fixedDeltaTime;
        else if (m_characterController.HorizontalDir == E_Direction.LEFT)
            newPos.x -= m_characterSpeed * Time.fixedDeltaTime;
        if (m_characterController.VerticalDir == E_Direction.UP)
            newPos.y += m_characterSpeed * Time.fixedDeltaTime;

        return newPos;
    }

    public void Reset()
    {
        m_animator.SetBool("SpeCap", false);
        m_dashHasBeenUsed = false;
        OnEnable();
    }

}