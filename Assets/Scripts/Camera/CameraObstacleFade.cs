using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraObstacleFade : MonoBehaviour
{
    public GameObject player;
    public LayerMask layers;

    private new Camera camera;

    private bool finished = false;
    private float timer;
    private Color fadeColor;

    private Vector3 centerPlayer;
    private List<Transform> t = new List<Transform>();
    private Transform temp;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        centerPlayer = player.transform.position;
        centerPlayer.y = centerPlayer.y + 2;

        Ray ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(centerPlayer));
        RaycastHit[] hits = Physics.RaycastAll(ray, 25, layers.value);

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
                }
                else
                    temp.GetComponent<MeshRenderer>().enabled = false;
            }

            if (t.Count > hits.Length - 1)
            {
                foreach (Transform temporary in t)
                {
                    if (!temporary.GetComponent<MeshRenderer>())
                    {
                        for (int i = 0; i < temporary.childCount; i++)
                            temporary.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
                    }
                    else
                        temporary.GetComponent<MeshRenderer>().enabled = true;
                }

                t.Clear();
            }
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
}
