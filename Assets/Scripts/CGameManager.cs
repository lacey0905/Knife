using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour {

    public GameObject m_Knife = null;
    public GameObject pos;
    //Rigidbody2D m_KnifeRid = null;
    
    public float m_fPower = 10.0f;

    Vector3 m_startMousePoint = Vector3.zero;
    Vector3 m_currentMousePoint = Vector3.zero;
    float m_fDistance = 0.0f;
    float m_fMaxDistance = 2.0f;

    bool isTouch = false;

    float fTouchTimer = 0.0f;
    float fMaxTouchTimer = 1.0f;

    Vector3 m_Direction = Vector3.zero;

    public List<GameObject> m_KnifeList = new List<GameObject>();

    private void Awake()
    {
    }

    private void Start()
    {
        for (int i = 0; i < 20; i++) {
            GameObject Temp = Instantiate(m_Knife, pos.transform.position, Quaternion.identity) as GameObject;
            Temp.transform.parent = pos.transform;
            m_KnifeList.Add(Temp);
        }
        m_KnifeList[0].SetActive(true);
    }


    public int count = 0;

    void Update ()
    {
        if (!isTouch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_startMousePoint = this.getMousePoint();
                isTouch = true;
            }
        }
        else
        {
            fTouchTimer += Time.smoothDeltaTime;
            if (fTouchTimer < fMaxTouchTimer)
            {
                if (Input.GetMouseButton(0))
                {
                    m_currentMousePoint = this.getMousePoint();
                    m_fDistance = Vector3.Distance(m_startMousePoint, m_currentMousePoint);
                    if (m_fDistance > m_fMaxDistance)
                    {
                        m_Direction = getDirection(m_currentMousePoint, m_startMousePoint);
                        Rigidbody2D t = m_KnifeList[count].GetComponent<Rigidbody2D>();
                        t.velocity = m_Direction * m_fPower;

                        t.transform.up = m_Direction;

                        count++;
                        //StartCoroutine("setKnife");
                        m_KnifeList[count].SetActive(true);
                        resetTouchEvent();
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    resetTouchEvent();
                }
            }
            else
            {
                resetTouchEvent();
            }
        }
    }

    IEnumerator setKnife()
    {
        yield return new WaitForSeconds(0.2f);
        m_KnifeList[count].SetActive(true);
    }

    Vector2 getDirection(Vector3 _head, Vector3 _tail)
    {
        Vector2 dir = (_head - _tail).normalized;
        dir.x = Mathf.Clamp(dir.x, -0.5f, 0.5f);
        dir.y = Mathf.Clamp(dir.y, -0.5f, 0.5f);
        return dir;
    }

    void resetTouchEvent()
    {
        //Debug.Log("터치 해제");
        fTouchTimer = 0.0f;
        isTouch = false;
        m_startMousePoint = Vector3.zero;
        m_currentMousePoint = Vector3.zero;
        m_fDistance = 0.0f;
    }

    void resetGame()
    {

    }

    Vector3 getMousePoint()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
            )
        );
        return mousePoint;
    }


}
