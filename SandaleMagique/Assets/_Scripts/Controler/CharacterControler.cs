﻿using UnityEngine;

public enum E_Direction
{
    RIGHT,
    LEFT,
    UP,
    DOWN,
    CENTER
};


public class CharacterControler : MonoBehaviour {


    private InputManager m_manager;

    private E_Direction m_horizontalDir = E_Direction.RIGHT;
    private E_Direction m_verticalDir = E_Direction.UP;
    public E_Direction HorizontalDir { get { return m_horizontalDir; } }
    public E_Direction VerticalDir { get { return m_verticalDir; } }

    private Character m_character = null;
    private Animator m_animator = null;

    private bool m_enableMoving = true;

    void Start () {
        m_manager = InputManager.Instance;
 
        m_character = GetComponent<Character>();
        m_animator = GetComponent<Animator>();

        OnEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject == this)
        {
            m_animator.SetBool("Jump", false);
        }
    }

    private void OnEnable()
    {
        m_manager.E_xAxis += Move;
        m_manager.E_yAxis += VerticalAxisMove;
        m_manager.E_noMove.AddListener(NoMove);
        m_manager.E_noVerticalMove.AddListener(NoVerticalMove);
    }

    private void OnDisable()
    {
        m_manager.E_xAxis -= Move;
        m_manager.E_yAxis -= VerticalAxisMove;
        m_manager.E_noMove.RemoveListener(NoMove);
        m_manager.E_noVerticalMove.RemoveListener(NoVerticalMove);
    }

    private void Move(float xAxis)
    {
        Rotate(xAxis);
        if (m_enableMoving)
        {
            Vector3 pos = transform.position;
            if (xAxis > 0)
            {
                pos.x += 1 * m_character.Speed * Time.deltaTime;
                m_horizontalDir = E_Direction.RIGHT;
            }
            else
            {
                pos.x += -1 * m_character.Speed * Time.deltaTime;
                m_horizontalDir = E_Direction.LEFT;
            }
            transform.position = pos;
        }  
    }

    public void EnableMoving(bool value)
    {
        m_enableMoving = value;
    }

    private void VerticalAxisMove(float yAxis)
    {
        if (m_enableMoving)
        {
            Vector3 pos = transform.position;
            if (yAxis < 0)
                m_verticalDir = E_Direction.UP;
            else if (yAxis > 0)
                m_verticalDir = E_Direction.DOWN;
        }
    }

    private void Rotate(float xAxis)
    {
        if(xAxis > 0 && m_horizontalDir == E_Direction.LEFT)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (xAxis <0 && m_horizontalDir == E_Direction.RIGHT)
        {
            transform.Rotate(0,-180, 0);
        }
    }

    private void NoMove()
    {
        m_horizontalDir = E_Direction.CENTER;
        m_animator.SetBool("Idle", true);
    }

    private void NoVerticalMove()
    {
        m_verticalDir = E_Direction.CENTER;
    }
}
