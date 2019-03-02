using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneSystem : MonoBehaviour
{
    public PanelData[] panelDatas;
    public Effect[] effects;
    public int[,] effectOrder;
    public GameObject panel;
    public GameObject text;
    private PanelData currentPanel;
    private Image BackgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundImage = GetComponent<Image>();
        Color transparent = BackgroundImage.color;
       // transparent.a = 0.5f;
        BackgroundImage.color = transparent;
        //Iteration still not working
        foreach (PanelData panelData in panelDatas)
        {
       //     panelData.setCanvas(transform.GetComponent<Canvas>());
            GameObject[] imagePanels = panelData.DisplayImages(panel);
            GameObject[] textPanels = panelData.DisplayText(text);
            //Need to check whats the first panel and nexts - not working;
            currentPanel = panelData;

            //Setting Deactive to false reset every cutscene
            currentPanel.deactive = false;

            //Setup Effects
            foreach (Effect effect in effects)
            {
                //not working
                effect.Setup(currentPanel);
            }
            //Image Panels Display - completed
            for (int i = 0; i < imagePanels.Length; i++)
            {

                //setParent
                imagePanels[i].transform.SetParent(transform);
                //Set Position
                imagePanels[i].transform.localPosition = Vector3.zero + new Vector3(0, -350 * i, 0);
              
                //Set Inactive
                imagePanels[i].gameObject.SetActive(false);
            }
            //Text Display - completed
            for (int i = 0; i < textPanels.Length; i++)
            {

                //setParent
                textPanels[i].transform.SetParent(transform);
                //Set Position 
                Vector3 position = new Vector3(100 * i, 100 * i, 100 * i);
                textPanels[i].transform.localPosition = Vector3.zero + position;
                //Set Inactive
                textPanels[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Time to start CurrentPanel  Work in Progress (only runs once)
        if (currentPanel.getStartTime < Time.time && (!currentPanel.getPanels[currentPanel.GetSprites.Length - 1].activeSelf && !currentPanel.getTextObject[currentPanel.GetTexts.Length - 1].activeSelf) && !currentPanel.deactive)
        {
            foreach (GameObject obj in currentPanel.getPanels)
            {
                obj.SetActive(true);               
            }
            foreach(GameObject obj in currentPanel.getTextObject)
            {
                obj.SetActive(true);
            }


            //Time to end Current Panel Work in Progress (only runs once)
        }
        else if(currentPanel.getEndTime < Time.time && (currentPanel.getPanels[currentPanel.GetSprites.Length - 1].activeSelf && currentPanel.getTextObject[currentPanel.GetTexts.Length - 1].activeSelf) && !currentPanel.deactive)
        {
            foreach (GameObject obj in currentPanel.getPanels)
            {
                obj.SetActive(false);
                
            }
            foreach (GameObject obj in currentPanel.getTextObject)
            {
                obj.SetActive(false);

            }
            currentPanel.deactive = true;
           // transform.gameObject.SetActive(false);
        }
        //Current Update still needs work
        if (currentPanel.getPanels[currentPanel.GetSprites.Length - 1].activeSelf)
        {
            foreach (Effect effect in effects)
            {
                foreach (GameObject obj in currentPanel.getPanels)
                {
                    effect.Apply(currentPanel);
                }
            }
        }
    }
}
