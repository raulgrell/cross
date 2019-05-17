using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScripted : MonoBehaviour
{
    private Animator buttonAnimator;

    public void Start()
    {
        buttonAnimator = GetComponent<Animator>();
    }
    
    public void ClickedAniamtion()
    {
        buttonAnimator.Play("Clicked");
    }

    public void EndAnimation()
    {
        if (gameObject.HasComponent<ExitButton>())
            GetComponent<ExitButton>().exitGameButton();
        if (gameObject.HasComponent<SettingsButton>())
            GetComponent<SettingsButton>().GoSettings();
        if (gameObject.HasComponent<StartButton>())
            GetComponent<StartButton>().StartScene();
        if (gameObject.HasComponent<ExittoMenuButton>())
            GetComponent<ExittoMenuButton>().GoBacktoMenu();
    }
}
