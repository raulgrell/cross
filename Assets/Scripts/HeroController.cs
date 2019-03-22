using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GridUnit))]
public class HeroController : UnitController
{
    public float speed;
    private Rigidbody rb;
    RaycastHit hit;
    public Transform Waypoint;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentInChildren<Rigidbody>();
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
            agent.destination = Waypoint.transform.position;
            Vector3 direction = Waypoint.transform.position - transform.position;
            
            if (Vector3.Distance(transform.position, Waypoint.transform.position) < 1)
            {
               Waypoint.gameObject.SetActive(false);
            }
        }
    }
}
