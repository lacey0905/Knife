using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObject : MonoBehaviour {

    Rigidbody2D rid;

    public float x;
    public float y;
    public float r;

    public Vector2 startPos;
    public Quaternion m_rotate;

    void Awake()
    { 
        startPos = this.transform.position;
        rid = this.GetComponent<Rigidbody2D>();
        m_rotate = this.transform.rotation;
    }

    void Start()
    {
        setObject();
    }

    bool m_Active = false;
    float m_Timer = 0.0f;

    void FixedUpdate()
    {
        if (m_Active)
        {
            m_Timer += Time.smoothDeltaTime;
        }
       
        if (m_Timer > 3.0f)
        {
            m_Active = false;
            this.gameObject.SetActive(false);
            Reset();
            setObject();
            m_Timer = 0.0f;
        }
    }

    public void setObject()
    {
        m_Active = true;
        x = Random.Range(-1f, 1f);
        y = Random.Range(10f, 15f);
        r = Random.Range(-50f, 50f);
        rid.velocity = new Vector2(x, y);
        rid.AddTorque(r);
    }

    public void Reset()
    {
        rid.velocity = Vector2.zero;
        this.transform.rotation = m_rotate;
        rid.AddTorque(0.0f);
        this.transform.position = startPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knife")
        {
            this.gameObject.SetActive(false);
            Reset();
            setObject();
        }
    }
}
