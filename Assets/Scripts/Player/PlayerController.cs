using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int state;
    RaycastHit hit;
    public Transform Waypoint;


    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void setState(int newState)
    {
        state = newState;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 9))
            {
                Waypoint.transform.position = hit.point;
                Waypoint.gameObject.SetActive(true);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.Log("Did not Hit");
            }
        }

        if (Waypoint.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoint.transform.position, speed);
            Vector3 direction = Waypoint.transform.position - transform.position;
          //  transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * -180 / Mathf.PI);


            if (Vector3.Distance(transform.position, Waypoint.transform.position) < 1)
            {
               Waypoint.gameObject.SetActive(false);
            }
      //    
        }
        //      


    }

   
}
