using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene("StartScene");
        }
	}
}
