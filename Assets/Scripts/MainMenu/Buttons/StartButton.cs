using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private bool active;
    
    public void StartScene()
    {
        if (active) return;
        active = true;
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        MenuGlitch.Instance.active = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("ShowScene"); 
    }
}
