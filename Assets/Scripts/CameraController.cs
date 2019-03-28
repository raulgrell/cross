using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed = 16;
    public float horizontalSway = -2;
    public float verticalSway = 8;
    public float horizontalRotation = -8;
    public float verticalRotation = -2;

    private Vector3 offset;
    private Vector3 direction;
    private new Camera camera;
    
    bool finished = false;
    float timer;
    Color fadeColor;
    
    private Vector3 centerPlayer;
    List<Transform> t = new List<Transform>();
    Transform temp;

    void Start()
    {
        var initialPosition = transform.position;
        camera = GetComponent<Camera>();
        offset = (player == null)
            ? initialPosition
            : initialPosition - player.transform.position;
        direction = transform.eulerAngles;
    }

    void LateUpdate()
    {
        if (!player) return;

        var mousePos = Input.mousePosition;
        mousePos.x = 2 * (mousePos.x / Screen.width - 0.5f);
        mousePos.y = 2 * (mousePos.y / Screen.height - 0.5f);

        var mouseOffset = Vector3.right * mousePos.x * horizontalSway + Vector3.forward * mousePos.y * verticalSway;
        var mouseRotation =
            Quaternion.Euler(direction.x + mousePos.y * verticalRotation, mousePos.x * horizontalRotation, 0);
        transform.rotation = mouseRotation;

        Vector3 desiredPosition = player.transform.position + offset + mouseOffset;
        Vector3 currentPosition = transform.position;
        desiredPosition.y = currentPosition.y;

        transform.position = Vector3.MoveTowards(currentPosition, desiredPosition, cameraSpeed * Time.deltaTime);

        centerPlayer = player.transform.position;
        centerPlayer.y = centerPlayer.y + 2;
        RaycastHit[] hits = Physics.RaycastAll(camera.ScreenPointToRay(camera.WorldToScreenPoint(centerPlayer)));

        foreach (RaycastHit hit in hits)
        {
            if (!hit.transform.CompareTag("Player") && !t.Contains(hit.transform))
            {
                temp = hit.transform;
                t.Add(temp);
                if (!temp.GetComponent<MeshRenderer>())
                {
                    for (int i = 0; i < temp.childCount; i++)
                        temp.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                    //     FadeObj(true, temp.GetChild(i));
                }
                else
                    temp.GetComponent<MeshRenderer>().enabled = false;
                //     FadeObj(true, temp);

            }
            //else if (t.Count > 1 && t[0].name != hitInfo.transform.name)
            //{
            //    //TODO: Fix condiciont to only enable when not colliding with any obj
            //    if (!t[0].GetComponent<MeshRenderer>())
            //    {
            //        for (int i = 0; i < t[0].childCount; i++)
            //            t[0].GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            //    }
            //    else
            //        t[0].GetComponent<MeshRenderer>().enabled = true;
            //    t.RemoveAt(0);
            //}
            //else if (t.Count > 0 && hits.Length)
            //{
            //    foreach (Transform temporary in t)
            //    {
            //        if (!temporary.GetComponent<MeshRenderer>())
            //        {
            //            for (int i = 0; i < temporary.childCount; i++)
            //                temporary.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            //        }
            //        else
            //            temporary.GetComponent<MeshRenderer>().enabled = true;
            //    }
            //    t.Clear();
            //}
        }
    }

    private void FadeObj(bool inOut, Transform obj)
    {
        timer += 0.1f;
        fadeColor = obj.GetComponent<MeshRenderer>().material.color;

        if (finished)
            return;
        
        if (inOut)
        {
            if (timer < 1)
            {
                fadeColor.a -= timer;
            }
            else
            {
                timer = 0;
                finished = true;
            }
        }
        else
        {
            if (timer < 1)
                fadeColor.a = timer;
            else
            {
                timer = 0;
                finished = true;
            }
        }

        obj.GetComponent<MeshRenderer>().material.color = fadeColor;
    }

    private void OnDrawGizmos()
    {
        if (player)
            Gizmos.DrawLine(transform.position, centerPlayer);
    }
}