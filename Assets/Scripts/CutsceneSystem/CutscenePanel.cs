using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class CutscenePanel : MonoBehaviour, IHeapItem<CutscenePanel>
{
    public Image image;
    public Text text;
    
    internal PanelData data;

    private float timer;

    private void Update()
    {
        if (timer > data.Duration)
        {
            gameObject.SetActive(false);
        }

        foreach (Effect effect in data.Effects)
        {
            effect.Apply(this);
        }

        timer += Time.deltaTime;
    }

    public void fromData(PanelData panelData)
    {
        data = panelData;
        image.sprite = data.Images[0].image;
        text.text = data.Texts[0].text;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    public int CompareTo(CutscenePanel other)
    {
        return data.StartTime > other.data.StartTime ? 1 : -1;
    }

    public int HeapIndex { get; set; }
}