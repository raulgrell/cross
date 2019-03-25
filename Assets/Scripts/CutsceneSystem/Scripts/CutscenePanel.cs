using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CutscenePanel : MonoBehaviour, IHeapItem<CutscenePanel>
{
    public Image imagePrefab;
    public Text textPrefab;
    public GameObject maskPrefab;
    internal PanelData data;
    internal GameObject[] images;
    internal GameObject[] texts;
    internal GameObject mask;
    internal CutsceneImage backgroundImage;

    private RectTransform rectTransform;

    private float timer;

    private void Start()
    {
        //Panel setup
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(backgroundImage.image.bounds.size.x * 100,
            backgroundImage.image.bounds.size.y * 100);
        GetComponent<Image>().sprite = backgroundImage.image;

        if (data.Mask != null)
        {
            Debug.Log("here");
            foreach (Effect effect in data.Mask.effects)
            {
                effect.Setup(this, transform.parent.gameObject);
            }
        }
        
        
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
            if(!transform.parent.CompareTag("Canvas"))
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        for (int i = 0; i < data.Images.Length; i++)
        {
            foreach (Effect effect in data.Images[i].effects)
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
        if (data.Mask != null)
        {
            Debug.Log("here");
            foreach (Effect effect in data.Mask.effects)
            {
                effect.Apply(this, transform.parent.gameObject);
            }
        }

        timer += Time.deltaTime;
    }

    public void fromData(PanelData panelData)
    {
        data = panelData;
        backgroundImage = data.BackgroundImage;
        if (data.Mask != null)
        {
            GameObject maskObject = Instantiate(maskPrefab, transform.parent);
            maskObject.GetComponent<Image>().sprite = data.Mask.image;
            maskObject.GetComponent<RectTransform>().sizeDelta = data.Mask.size;
            transform.SetParent(maskObject.transform);
            maskObject.SetActive(false);
        }
        images = new GameObject[data.Images.Length];
        texts = new GameObject[data.Texts.Length];
       
        

        for (int i = 0; i < data.Images.Length; i++)
        {
            images[i] = Instantiate(imagePrefab.gameObject, transform);
            Image newImage = images[i].GetComponent<Image>();
            RectTransform rectTransform = newImage.rectTransform;
            newImage.sprite = data.Images[i].image;
            rectTransform.sizeDelta =
                new Vector2(newImage.sprite.bounds.size.x * 100, newImage.sprite.bounds.size.y * 100);
            newImage.rectTransform.sizeDelta = rectTransform.sizeDelta;
            //if (data.Images[i].mask != null) {
            //    SpriteMask masky = images[i].GetComponent<SpriteMask>();
            //    masky.sprite = newImage.sprite;
            //    masky.alphaCutoff = data.Images[i].mask.AlphaCutoff;
            //}
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