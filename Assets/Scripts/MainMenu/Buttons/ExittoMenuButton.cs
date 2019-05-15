using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExittoMenuButton : MonoBehaviour
{
    public void GoBacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
