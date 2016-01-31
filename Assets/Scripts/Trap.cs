using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    private bool hasDemon = false;
    private bool hasPlayer = false;

    public void SetDemon() {
        hasDemon = true;
    }

    public bool HasDemon() {
        return hasDemon;
    }

    public void SetPlayer()
    {
        hasPlayer = true;
    }

    public bool HasPlayer()
    {
        return hasPlayer;
    }

    public bool HasObject()
    {
        return hasDemon || hasPlayer;
    }
}
