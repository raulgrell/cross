﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CutsceneSystem : MonoBehaviour
{
    public PanelData[] panelData;
    public GameObject panelPrefab;
    
    private Image backgroundImage;
    private CutscenePanel[] panels;

    private Heap<CutscenePanel> incoming;
    private Heap<CutscenePanel> outgoing;
    private CutscenePanel currentPanel;

    private float timer;

    
    void Start()
    {
        backgroundImage = GetComponent<Image>();
        
        incoming = new Heap<CutscenePanel>(panelData.Length);
        panels = new CutscenePanel[panelData.Length];
        for(int i = 0; i < panelData.Length; i++)
        {
            var panel = Instantiate(panelPrefab, transform);
            var cPanel = panel.GetComponent<CutscenePanel>();
            cPanel.fromData(panelData[i]);
            panels[i] = cPanel;
            incoming.Add(cPanel);
        }

        currentPanel = incoming.RemoveFirst();
        Debug.Log("FirstPanel" + currentPanel.data.StartTime );
        Debug.Log("Second Panel" + incoming);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (currentPanel != null && currentPanel.data.StartTime > timer)
        {
            Debug.Log("here");
            currentPanel.gameObject.SetActive(true);
            currentPanel = (incoming.Count > 0)
                ? incoming.RemoveFirst()
                : null;
        }
    }
}
