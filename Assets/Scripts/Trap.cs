using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    private bool hasDemon = false;

    public void SetDemon() {
        hasDemon = true;
    }

    public bool HasDemon() {
        return hasDemon;
    }
}
