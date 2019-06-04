using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public Canvas interactionCanvas;
    public GameObject interactionTextPrefab;
    public float interactableY = 2.5f;
    
    private new Camera camera;
    private GridUnit gridUnit;
    private GridCombat gridCombat;
    private TextMeshProUGUI currentText;
    private int currentChar;

    private List<char> characters = new List<char>();
    
    private bool spawning, finished, holding;
    private InteractableObj currentObj;

    public GridUnit Unit => gridUnit;

    private void Start()
    {
        camera = Camera.main;
        gridCombat = GetComponent<GridCombat>();
        gridUnit = GetComponent<GridUnit>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !holding)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit info))
            {
                if (info.transform.CompareTag("Interactable"))
                {
                    currentObj = info.transform.GetComponent<InteractableObj>();

                    if (Vector2.Distance(gridUnit.Position, currentObj.Position) <= 1.5f)
                    {
                        gridCombat.State = CombatState.Interacting;
                        gridUnit.Grid.Nodes[currentObj.Position.y, currentObj.Position.x].walkable = true;
                        Vector3 newPos = gridUnit.Grid.CellToWorld(gridUnit.Position);
                        newPos.y = interactableY + currentObj.getGroundedY;
                        currentObj.transform.position = newPos;
                        currentObj.state = ObjectState.Held;
                        holding = true;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(1) && holding)
        { 
                gridCombat.State = CombatState.Idle;
                currentObj.state = ObjectState.Fixed;
                holding = false;
                currentObj = null;
        }

        if (Input.GetMouseButtonDown(0) && !finished)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                if (hitInfo.transform.CompareTag("Interactable") && characters.Count < 1 &&
                    hitInfo.transform.GetComponent<InteractableObj>().text.Length > 0)
                {
                    currentChar = 0;
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

    void SpawnText()
    {
        if (!currentText)
        {
            GameObject textObj = Instantiate(interactionTextPrefab, interactionCanvas.transform);
            currentText = textObj.GetComponentInChildren<TextMeshProUGUI>();
            currentText.text = "";
        }
        StartCoroutine(EachC());
    }

    IEnumerator EachC()
    {
        while (currentChar < characters.Count)
        {
            currentText.text += characters[currentChar];
            currentChar++;
            yield return new WaitForSeconds(3f);
        }

        finished = true;
    }
}