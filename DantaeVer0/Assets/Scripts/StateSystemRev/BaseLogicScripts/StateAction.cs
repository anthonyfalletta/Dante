using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : ScriptableObject
{

    public abstract void ActStart(StateController controller);
    public abstract void ActUpdate(StateController controller);
}
