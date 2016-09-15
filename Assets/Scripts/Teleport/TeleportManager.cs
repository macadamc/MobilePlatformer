using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class TeleportManager : MonoBehaviour {

    public static TeleportManager GM_TELEPORT;

    string teleportKeyToLookFor;

    Player player;

    bool levelLoaded;

    //SceneLoaded sceneLoaded;

    //delegate void SceneLoaded();

	// Use this for initialization
	void Awake ()
    {
        if (GM_TELEPORT == null)                        //If there is no GM_TELEPORT instance - Set it to this instance
            GM_TELEPORT = this;
        else
        if (GM_TELEPORT != this)                        //If GM_TELEPORT already exists - destroy this object
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


        player = FindObjectOfType<Player>();

    }

    void OnLevelWasLoaded()
    {

        if (teleportKeyToLookFor != "")
        {
            TeleportToDoor(teleportKeyToLookFor);
            levelLoaded = true;
        }
    }

    public void Teleport(string teleportKey, string levelToLoad)
    {
        teleportKeyToLookFor = teleportKey;
        LoadLevel(levelToLoad);
    }
    public void Teleport(string teleportKey)
    {
        teleportKeyToLookFor = teleportKey;
        TeleportToDoor(teleportKeyToLookFor);
    }

    public void TeleportToDoor(string key)
    {
        Teleport[] teleporters = GameObject.FindObjectsOfType<Teleport>();

        for(int i  = 0; i < teleporters.Length; i++)
        {
            if(teleporters[i].teleportKey == teleportKeyToLookFor)
            {
                player.transform.position = teleporters[i].GetComponent<BoxCollider2D>().bounds.center+((Vector3)teleporters[i].offset);
                player.rb.velocity = Vector2.zero;
                player.StunLock(0.5f);

                CameraFollow.CAM_FOLLOW.SetAtTargetPos();
            }
        }

        teleportKeyToLookFor = null;
    }

    public void LoadLevel(string level)
    {
        levelLoaded = false;
        SceneManager.LoadScene(level);
    }

}
