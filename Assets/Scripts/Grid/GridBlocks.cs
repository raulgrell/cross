using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridFloor", menuName = "Grid/Models")]
public class GridBlocks : ScriptableObject
{
    public GameObject empty;
    public GameObject[] metals;
    public GameObject[] walkways;
    public GameObject[] exterior;
}
