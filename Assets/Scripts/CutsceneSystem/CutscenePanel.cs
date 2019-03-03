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

    private CutsceneImage[] images;    
    private float timer;

    private void Start()
    {
        foreach (CutsceneImage image in images)
        {
            image.effect.Setup(this);
        }
   

    }

    private void Update()
    {
        if (timer > data.Duration)
        {
            gameObject.SetActive(false);
        }

        foreach (CutsceneImage image in images)
        {
            image.effect.Apply(this);
        }

        timer += Time.deltaTime;
    }

    public void fromData(PanelData panelData)
    {
        data = panelData;
        images = new CutsceneImage[data.Images.Length];
        for(int i = 0; i < data.Images.Length; i++)
        {
            GameObject imageObject = Instantiate(imagePrefab.gameObject,transform);
            images[i] = data.Images[i];
            imageObject.GetComponent<Image>().sprite = images[i].image;
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