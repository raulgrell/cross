﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripted : MonoBehaviour
{
    private Animator buttonAnimatior;

    public void Start()
    {
        buttonAnimatior = GetComponent<Animator>();
    }
    public void ClickedAniamtion()
    {
        buttonAnimatior.Play("Clicked");
    }

    public void EndAnimation()
    {
        if (gameObject.HasComponent<ExitButton>())
            GetComponent<ExitButton>().exitGameButton();
        if (gameObject.HasComponent<SettingsButton>())
            GetComponent<SettingsButton>().GoSettings();
        if (gameObject.HasComponent<StartButton>())
            GetComponent<StartButton>().StartScene();

    }
}
