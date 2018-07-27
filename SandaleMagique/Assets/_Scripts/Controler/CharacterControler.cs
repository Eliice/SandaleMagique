using UnityEngine;

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



    void Start () {
        m_manager = InputManager.Instance;
 
        m_character = GetComponent<Character>();
        m_animator = GetComponent<Animator>();

        OnEnable();
    }


    private void OnEnable()
    {
        m_manager.E_xAxis += Move;
        m_manager.E_yAxis += VerticalAxisMove;
        m_manager.E_noMove.AddListener(NoMove);
    }

    private void OnDisable()
    {
        m_manager.E_xAxis -= Move;
        m_manager.E_yAxis -= VerticalAxisMove;
        m_manager.E_noMove.RemoveListener(NoMove);
    }

    private void Move(float xAxis)
    {
        Rotate(xAxis);
        Vector3 pos = transform.position;
        if(xAxis > 0)
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

    private void VerticalAxisMove(float yAxis)
    {
        Vector3 pos = transform.position;
        if (yAxis > 0)
            m_verticalDir = E_Direction.UP;
        else if (yAxis < 0)
            m_verticalDir = E_Direction.DOWN;
        else
            m_verticalDir = E_Direction.CENTER;
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
}
