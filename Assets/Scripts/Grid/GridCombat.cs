using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject specialPrefab;
    public new Camera camera;
    private GridLayer grid;
    private GridUnit gridUnit;

    private List<GridNode> neighbors;
    
    private Transform target;
    private bool targeting;

    void Start()
    {
        gridUnit = GetComponent<GridUnit>();
        grid = gridUnit.grid;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            rotateTowards(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0.2f;
            targeting = true;
        }

        if (targeting && Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        {
            target = hitInfo.transform;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Time.timeScale = 1f;                
            targeting = false;
        }
        if (target && target.CompareTag("Enemy"))
        {
            //kinda working
            transform.LookAt(target);
        }
        else if (target)
        {
            rotateTowards(target.position);
        }

    }

    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(target.position, Vector3.one);
            Gizmos.DrawLine(transform.position, target.position);
            
        }
    }
    void rotateTowards(Vector3 direction)
    {
        //not working
        float angle = Vector3.Angle(transform.position, direction);
        transform.localRotation = new Quaternion(0,angle,0,1);

    }
}
