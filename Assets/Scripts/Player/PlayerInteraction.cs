using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public GridCombat gridCombat;
    public Canvas interactionCanvas;
    public GameObject interactionTextPrefab;
    public string text;
    List<char> characters = new List<char>();
    private TextMeshProUGUI currentText;
    private int i = 0;
    // Start is called before the first frame update
    private bool spawning,finished;
    void Start()
    {
        foreach (char c in text)
            characters.Add(c);

    }

    IEnumerator SpawnText()
    {
        if (!currentText)
        {
            GameObject textObj = Instantiate(interactionTextPrefab, interactionCanvas.transform);
            currentText = textObj.GetComponentInChildren<TextMeshProUGUI>();
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
        yield return new WaitForSeconds(2f);
        }
        finished = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (gridCombat.Target)
        {
            if (gridCombat.Target.CompareTag("Interactable") && Input.GetMouseButtonDown(0) && !finished)
            {
                spawning = true;
            }
            else if (finished)
            {
                spawning = false;
                Destroy(currentText.transform.parent.gameObject);
                finished = false;
            }
        }
        if (spawning)
            SpawnText();
    }
}
