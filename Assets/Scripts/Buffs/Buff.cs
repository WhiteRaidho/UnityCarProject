using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Buff : NetworkBehaviour {
    public abstract void Apply(GameObject applyTo, int boost);
}
