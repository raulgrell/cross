using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneSystem : MonoBehaviour
{
    public Image BackgroundImage;
    public PanelData[] panelDatas;
    public GameObject panel;
    private GameObject text;
    private PanelData currentPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        Color transparent = BackgroundImage.color;
        transparent.a = 0.5f;
        BackgroundImage.color = transparent;
        foreach (PanelData panelData in panelDatas)
        {
            GameObject[] finalPanels = panelData.DisplayImages(panel);
            currentPanel = panelData;
            for(int i = 0; i < finalPanels.Length; i++)
            {
                
                //setParent
                finalPanels[i].transform.SetParent(transform);
                //Set Position
                Vector3 position = new Vector3(100 * i, 100 * i, 100 * i);
                finalPanels[i].transform.localPosition = Vector3.zero + position;
            }

       //     panelData.DisplayText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentPanel.getStartTime > Time.deltaTime)
        //{
        //    for (int i = 0; i < finalPanels.Length; i++)
        //    {

        //        //setParent
        //        finalPanels[i].transform.SetParent(transform);
        //        //Set Position
        //        Vector3 position = new Vector3(100 * i, 100 * i, 100 * i);
        //        finalPanels[i].transform.localPosition = Vector3.zero + position;
        //    }
        //}
        if(currentPanel.getEndTime < Time.time)
        {
            foreach(GameObject obj in currentPanel.getPanels)
            {
                obj.SetActive(false);
            }
        }
    }
}
