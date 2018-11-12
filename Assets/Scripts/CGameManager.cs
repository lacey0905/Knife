using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour {


    private void Awake()
    {
        
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            


            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //{
            //    if (hit.transform.tag.Equals("Temp"))
            //    {
            //        Debug.Log("충돌");
            //    }
            //}
        }
	}
}
