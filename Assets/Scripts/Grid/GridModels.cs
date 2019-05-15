using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridFloor", menuName = "Grid/Models")]
public class GridModels : ScriptableObject
{
    public GameObject plates;
    public GameObject stones;
    public GameObject rock;
    public GameObject redRock;
    public GameObject[] metals;
    public GameObject[] walkways;
}
