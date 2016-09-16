using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour {

    public string sceneToLoad;
    public string doorKey;

	// Use this for initialization
	void Start ()
    {
        TeleportManager.GM_TELEPORT.SetTeleportKeyToLookFor(doorKey);
        SceneManager.LoadScene(sceneToLoad);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
