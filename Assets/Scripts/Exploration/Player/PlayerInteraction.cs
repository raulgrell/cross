using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Canvas interactionCanvas;
    public GameObject interactionTextPrefab;
    public float interactableY = 2.5f;
    private GridCombat gridCombat;
    private TextMeshProUGUI currentText;
    internal GridUnit gridUnit;
    private int i = 0;
    List<char> characters = new List<char>();
    // Start is called before the first frame update
    private bool spawning,finished, holding;
    private InteractableObj currentObj;
    private void Start()
    {
        gridCombat = GetComponent<GridCombat>();
        gridUnit = GetComponent<GridUnit>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !holding)
        {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
                {
                    if (info.transform.CompareTag("Interactable"))
                    {
                        currentObj = info.transform.GetComponent<InteractableObj>();
                        if (Vector2.Distance(gridUnit.position, currentObj.getGridPos) < 3)
                        {
                        Vector3 newPos = gridUnit.grid.CellToWorld(currentObj.getGridPos);
                        newPos.y = interactableY;
                        currentObj.transform.position = newPos;
                        currentObj.state = 1;
                        holding = true;
                        }
                    }
            }

        }
        else if(Input.GetMouseButtonUp(1) && holding)
        {
            currentObj.transform.position = new Vector3(currentObj.transform.position.x, currentObj.getGroundedY, currentObj.transform.position.z);
            currentObj.getGridPos = gridUnit.grid.WorldToCell(currentObj.transform.position);
            holding = false;
            currentObj.state = 0;
            currentObj = null;
        }
        if (Input.GetMouseButtonDown(0) && !finished)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                if (hitInfo.transform.CompareTag("Interactable") && characters.Count < 1 && hitInfo.transform.GetComponent<InteractableObj>().text.Length > 0)
                {
                    i = 0;
                    spawning = true;
                    characters = getCharacters(hitInfo.transform.GetComponent<InteractableObj>().text);
                }
            }
        }
        else if (finished && Input.GetMouseButtonDown(0))
        {
            spawning = false;
            if (currentText)
                Destroy(currentText.transform.parent.gameObject);
            characters.Clear();
            finished = false;
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
        while (i < characters.Count)
        {
            currentText.text += characters[i];
            i++;
            yield return new WaitForSeconds(3f);
        }

        finished = true;
    }
}