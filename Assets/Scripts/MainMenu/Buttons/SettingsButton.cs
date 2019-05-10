using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;

    public void GoSettings()
    {
        transform.parent.parent.gameObject.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
