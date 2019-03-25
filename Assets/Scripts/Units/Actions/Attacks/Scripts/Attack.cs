using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack
{
    public abstract void Act(GridLayer grid, GridUnit unit, int sector);
}
