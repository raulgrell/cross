using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CutscenePanel : MonoBehaviour, IHeapItem<CutscenePanel>
{
    public Image imagePrefab;
    public Text textPrefab;
    internal PanelData data;
    internal GameObject[] images;
    internal GameObject[] texts;
    internal CutsceneImage backgroundImage;

    private float timer;

    private void Start()
    {
        GetComponent<Image>().sprite = backgroundImage.image;
        //For more than one image
        for (int i = 0; i < data.Images.Length; i++)
        {
            //For more than one effect
            foreach (Effect effect in data.Images[i].effects)
            {
                effect.Setup(this, images[i]);
            }
        }
        //For more than one text
        for (int i = 0; i < data.Texts.Length; i++)
        {
            //For more than one effect
            foreach (Effect effect in data.Texts[i].effects)
            {
                effect.Setup(this, texts[i]);
            }
        }
        //For more than one ffect on the background
        foreach (Effect effect in backgroundImage.effects)
        {
            effect.Setup(this, gameObject);
        }
        transform.localScale = data.PanelScale;


    }

    private void Update()
    {
        if (timer > data.Duration)
        {
            gameObject.SetActive(false);
        }
        for (int i = 0; i < data.Images.Length; i++)
        {
            foreach(Effect effect in data.Images[i].effects)
            {
                effect.Apply(this, images[i]);
            }
        }
        for (int i = 0; i < data.Texts.Length; i++)
        {
            foreach (Effect effect in data.Texts[i].effects)
            {
                effect.Apply(this, texts[i]);
            }
        }
        foreach (Effect effect in backgroundImage.effects)
        {
            effect.Apply(this, gameObject);
        }

        timer += Time.deltaTime;
    }

    public void fromData(PanelData panelData)
    {
        data = panelData;
        backgroundImage = panelData.BackgroundImage;
        images = new GameObject[data.Images.Length];
        texts = new GameObject[data.Texts.Length];
        for(int i = 0; i < data.Images.Length; i++)
        {
            images[i] = Instantiate(imagePrefab.gameObject,transform);
            Image newImage = images[i].GetComponent<Image>();
            newImage.sprite = data.Images[i].image;
        }
        for (int i = 0; i < data.Texts.Length; i++)
        {
            texts[i] = Instantiate(textPrefab.gameObject, transform);
            Text newText = texts[i].GetComponent<Text>();
            newText.text = data.Texts[i].text;
            newText.fontSize = data.Texts[i].size;
            newText.color = data.Texts[i].background;
        }
        gameObject.SetActive(false);
    }

    public int CompareTo(CutscenePanel other)
    {
        return data.StartTime < other.data.StartTime ? 1 : -1;
    }

    public int HeapIndex { get; set; }
}