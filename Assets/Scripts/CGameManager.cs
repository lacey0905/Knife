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
    float m_fMaxDistance = 1.0f;

    bool isTouch = false;

    float fTouchTimer = 0.0f;
    float fMaxTouchTimer = 1.0f;

    Vector3 m_Direction = Vector3.zero;

    public List<GameObject> m_KnifeList = new List<GameObject>();

    private LineRenderer lineRenderer;

    public List<CObject> m_Object = new List<CObject>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++) {
            GameObject Temp = Instantiate(m_Knife, pos.transform.position, Quaternion.identity) as GameObject;
            Temp.transform.parent = pos.transform;
            m_KnifeList.Add(Temp);
        }
        m_KnifeList[0].SetActive(true);

        StartCoroutine(StartObject());
    }

    public int count = 0;

    void FixedUpdate()
    {
        
    }

    int objCount = 0;

    IEnumerator StartObject()
    {
        for (int i = 0; i < 50; i++)
        {
            if (objCount >= 10) {
                objCount = 0;
            }
            if (i > 3)
            {
                m_Object[objCount].gameObject.SetActive(true);
                objCount++;
                m_Object[objCount].gameObject.SetActive(true);
            }
            else
            {
                m_Object[objCount].gameObject.SetActive(true);
            }
            objCount++;
            yield return new WaitForSeconds(2.0f);
        }
    }

    void Update ()
    {
        if (!isTouch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (count >= m_KnifeList.Count-1)
                {
                    for (int i = 0; i < m_KnifeList.Count-1; i++)
                    {
                        m_KnifeList[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        m_KnifeList[i].transform.position = pos.transform.position;
                        m_KnifeList[i].SetActive(false);
                    }
                    m_KnifeList[0].SetActive(false);
                    count = 0;
                }
                else
                {
                    int layerMask = 1 << LayerMask.NameToLayer("Knife"); 
                    Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Ray2D ray = new Ray2D(wp, Vector2.zero);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
                    if (hit.collider != null && hit.collider.tag == "Start")
                    {
                        m_startMousePoint = this.getMousePoint();
                        isTouch = true;
                    }
                }
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
                        m_Direction = getDirection(m_currentMousePoint, pos.transform.position);
                        Rigidbody2D t = m_KnifeList[count].GetComponent<Rigidbody2D>();
                        t.velocity = m_Direction * m_fPower;

                        t.transform.up = m_Direction;


                        lineRenderer.SetPosition(0, pos.transform.position);
                        lineRenderer.SetPosition(1, m_currentMousePoint);

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

    Vector2 getDirection(Vector3 _head, Vector3 _tail)
    {
        Vector2 dir = (_head - _tail).normalized;
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
