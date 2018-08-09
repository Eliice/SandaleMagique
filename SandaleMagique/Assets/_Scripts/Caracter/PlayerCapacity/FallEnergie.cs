﻿using UnityEngine;
using UnityEngine.UI;


public class FallEnergie : MonoBehaviour {

    [SerializeField]
    private Slider m_energiSlider = null;
    [SerializeField, Range(0, 50)]
    private float m_fillMultiplier = 1;
    [SerializeField, Range(0, 50)]
    private float m_consumptionPerSecond = 1;

    private Vector3 m_previousPos = new Vector3();


    private bool m_shoudRemove = false;
    public bool ShoudRemove { set { m_shoudRemove = value; m_ShoudAdd = !value; } }

    private bool m_ShoudAdd = true;

    public float getEnergie()
    {
        return m_energiSlider.value;
    }

     private void FixedUpdate()
     {
        if(m_previousPos.y > transform.position.y && m_ShoudAdd)
        {
            m_energiSlider.value += (m_previousPos.y - transform.position.y) * m_fillMultiplier;
        }
        if (m_shoudRemove)
        {
            m_energiSlider.value -= m_consumptionPerSecond * Time.deltaTime;
        }
        m_previousPos = transform.position;

	}


    public void ResetEnergie()
    {
        m_energiSlider.value = 0;
        m_ShoudAdd = true;
        m_shoudRemove = false;
    }


}