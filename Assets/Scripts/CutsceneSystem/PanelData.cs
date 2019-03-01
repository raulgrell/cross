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
    private GameObject[] panels;
    public Animation[] effects;
    public Sprite[] images;
    public string[] texts;

    public Sprite[] GetSprites => images;
    public string[] GetTexts => texts;
    public float getStartTime => startTime;
    public float getEndTime => endTime;
    public GameObject[] getPanels => panels;

    public GameObject[] DisplayImages(GameObject gameObject)
    {
        panels = new GameObject[images.Length];
        for(int i = 0; i < images.Length; i++)
        {
            panels[i] = Instantiate(gameObject);            
            panels[i].GetComponent<Image>().sprite = images[i];
        }

        return panels; 
    }

    public Text[] DisplayText()
    {
        Text[] finalText = new Text[texts.Length];

        for(int i = 0; i < texts.Length; ++i)
        {
            finalText[i].text = texts[i];
        }

        return finalText;
    }

}
