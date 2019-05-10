using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSettingsButton : MonoBehaviour
{
    public GameObject StartMenu;
    public void ExitSettingsMenu()
    {
        StartMenu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
