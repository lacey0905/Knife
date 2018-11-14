using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObject : MonoBehaviour {

    Rigidbody2D rid;

    float x;
    float y;
    float r;

    Vector2 startPos;

    void Awake()
    {
        rid = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPos = this.transform.position;
        setObject();
    }

    void FixedUpdate()
    {

    }

    public void setObject()
    {
        x = Random.Range(-1f, 1f);
        y = Random.Range(10f, 15f);
        r = Random.Range(-50f, 50f);
        rid.velocity = new Vector2(x, y);
        rid.AddTorque(r);
    }

    public void Reset()
    {
        rid.velocity = Vector2.zero;
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
