using UnityEngine;
using UnityEngine.UI;


public class FallEnergie : MonoBehaviour {

    [SerializeField]
    private Slider m_energiSlider = null;
    [SerializeField, Range(0, 50)]
    private float m_fillMultiplier = 1;
    [SerializeField, Range(0, 50)]
    private float m_forceMultiplicator = 5;
    public float ForceMultiplicator { get { return m_forceMultiplicator; } }

    private Vector3 m_previousPos = new Vector3();


    public float getEnergie()
    {
        return m_energiSlider.value;
    }

     private void FixedUpdate()
     {
        m_energiSlider.value += (m_previousPos.y - transform.position.y) * m_fillMultiplier;
        m_previousPos = transform.position;

	}


    public void ResetEnergie()
    {
        m_energiSlider.value = 0;
    }


}
