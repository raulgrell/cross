using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public GridCombat gridCombat;
    public Canvas interactionCanvas;
    public GameObject interactionTextPrefab;
    List<char> characters = new List<char>();
    private TextMeshProUGUI currentText;
    private int i = 0;
    // Start is called before the first frame update
    private bool spawning,finished;

    void Update()
    {
        if (gridCombat.Target)
        {
            if (gridCombat.Target.CompareTag("Interactable") && Input.GetMouseButtonDown(0) && characters.Count < 1)
            {
                i = 0;
                spawning = true;
                characters = getCharacters(gridCombat.Target.transform.GetComponent<InteractableObj>().text);
            }
            else if (gridCombat.Target.CompareTag("Interactable")  && finished && Input.GetMouseButtonDown(0))
            {
                spawning = false;
                if(currentText)
                Destroy(currentText.transform.parent.gameObject);
                characters.Clear();
                finished = false;
                
            }
        }
        if (spawning && !finished)
            SpawnText();
    }
    List<char> getCharacters(string text)
    {
        List<char> tempChar = new List<char>();
        foreach (char c in text)
            tempChar.Add(c);
        return tempChar;
    }

    IEnumerator SpawnText()
    {
        if (!currentText)
        {
            GameObject textObj = Instantiate(interactionTextPrefab, interactionCanvas.transform);
            currentText = textObj.GetComponentInChildren<TextMeshProUGUI>();
            currentText.text = "";
        }
        StartCoroutine(EachC());
        return null;
    }

    IEnumerator EachC()
    {
        while(i < characters.Count)
        {
        currentText.text += characters[i];
        i++;
        yield return new WaitForSeconds(3f);
        }
        finished = true;
    }
    // Update is called once per frame
}
