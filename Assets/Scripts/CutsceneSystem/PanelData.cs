using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Cutscene/Panel")]
public class PanelData : ScriptableObject
{
    [SerializeField]
    private float endTime;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private int layer;
    [SerializeField]
    private int layerText;
   // public Effect[] effects;
    public Sprite[] images;
    public string[] texts;
    internal bool deactive = false;
    private Canvas canvas;
    

    public Sprite[] GetSprites => images;
    public string[] GetTexts => texts;
    public float getStartTime => startTime;
    public float getEndTime => endTime;
    public GameObject[] getPanels { get; private set; }
    public GameObject[] getTextObject { get; private set; }
   // public Effect[] getEffects { get; private set; }

    //public Canvas setCanvas(Canvas newCanvas)
    //{
    //    canvas = newCanvas;
    //    return canvas;
    //}

    //Create Gamejects to diplay in scene - Completed
    public GameObject[] DisplayImages(GameObject gameObject)
    {
        getPanels = new GameObject[images.Length];
        for(int i = 0; i < images.Length; i++)
        {
            getPanels[i] = Instantiate(gameObject);            
            getPanels[i].GetComponent<Image>().sprite = images[i];
        }

        return getPanels; 
    }

    //Not working needing better implementation
    public void setInicialPosition(Vector3 inicialPosition)
    {
        for (int i = 0; i < images.Length; i++)
        {
      //    getPanels[i].transform.localPosition = Vector3.zero + inicialPosition;
        }

    }
    //Eork in Progress needing better implementation
    public void ApplyEffects(Vector3 target, float speed)
    {
        for (int i = 0; i < images.Length; i++)
        {
            getPanels[i].transform.localPosition = Vector3.MoveTowards(getPanels[i].transform.localPosition, getPanels[i].transform.localPosition + target, speed);
        }
    }

    //Create gameobjects to display text - Completed
    public GameObject[] DisplayText(GameObject gameObject)
    {
        getTextObject = new GameObject[texts.Length];
        for(int i = 0; i < texts.Length; ++i)
        {
            getTextObject[i] = Instantiate(gameObject);
            getTextObject[i].GetComponent<Text>().text = texts[i];
        }

        return getTextObject;
    }

}
