﻿using UnityEngine;

public enum E_Direction
{
    RIGHT,
    LEFT
};


public class CharacterControler : MonoBehaviour {


    private InputManager m_manager;
    private E_Direction m_direction = E_Direction.RIGHT;
    private Character m_character = null;
    private Animator m_animator = null;



    // Use this for initialization
    void Start () {
        m_manager = InputManager.Instance;
 
        m_character = GetComponent<Character>();
        m_animator = GetComponent<Animator>();

        OnEnable();
    }


    private void OnEnable()
    {
        m_manager.xAxis += Move;
        m_manager.noMove += NoMove;
    }

    private void OnDisable()
    {
        m_manager.xAxis -= Move;
        m_manager.noMove -= NoMove;
    }

    private void Move(float xAxis)
    {
        Debug.Log(xAxis);
        Rotate(xAxis);
        Vector3 pos = transform.position;
        if(xAxis > 0)
        {
            pos.x += 1 * m_character.Speed * Time.deltaTime;
        }
        else
        {
            pos.x += -1 * m_character.Speed * Time.deltaTime;
        }
        transform.position = pos;
    }



    private void Rotate(float xAxis)
    {
        if(xAxis > 0 && m_direction == E_Direction.LEFT)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (xAxis <0 && m_direction == E_Direction.RIGHT)
        {
            transform.Rotate(0,-180, 0);
        }
    }


    private void NoMove()
    {
        m_animator.SetBool("Idle", true);
    }

}
