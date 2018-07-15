using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {


    [SerializeField]
    private UnityEvent m_eventTriggerEnter;

    [SerializeField]
    private UnityEvent m_eventTriggerStay;

    [SerializeField]
    private UnityEvent m_eventTriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            m_eventTriggerEnter.Invoke();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            m_eventTriggerStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            m_eventTriggerExit.Invoke();
    }

}
