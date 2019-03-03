using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CutscenePanel : MonoBehaviour, IHeapItem<CutscenePanel>
{
    public Image imagePrefab;
    public Text text;
    private int Id;
    internal PanelData data;
    internal GameObject[] images;    

    private float timer;

    private void Start()
    {
        for (int i = 0; i < data.Images.Length; i++)
        {
            for(int v = 0; v < data.Images[i].effects.Length; v++)
        {
            data.Images[i].effects[v].Setup(this, images[i]);
        }
        
    }
          

    }

    private void Update()
    {
        if (timer > data.Duration)
        {
            gameObject.SetActive(false);
        }
        for (int i = 0; i < data.Images.Length; i++)
        {
            for (int v = 0; v < data.Images[i].effects.Length; v++)
            {
                data.Images[i].effects[v].Apply(this, images[i]);
            }
        }


        timer += Time.deltaTime;
    }

    public void fromData(PanelData panelData)
    {
        data = panelData;
        images = new GameObject[data.Images.Length];
        for(int i = 0; i < data.Images.Length; i++)
        {
            images[i] = Instantiate(imagePrefab.gameObject,transform);
            Image newImage = images[i].GetComponent<Image>();
            newImage.sprite = data.Images[i].image;
        }
        text.text = data.Texts[0].text;
        gameObject.SetActive(false);
    }

    public int CompareTo(CutscenePanel other)
    {
        return data.StartTime > other.data.StartTime ? 1 : -1;
    }

    public int HeapIndex { get; set; }
}