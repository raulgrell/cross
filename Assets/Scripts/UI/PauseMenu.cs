using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool active;
    public GridInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && !active)
        {
            pauseMenu.gameObject.SetActive(true);
            active = true;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.gameObject.SetActive(false);
            active = false;
            Time.timeScale = 1;
            
        }

    }
}
