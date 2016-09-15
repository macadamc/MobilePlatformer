using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public bool onTouch;
    public string teleportToKey;
    public string teleportKey;
    public string levelName;
    public Vector2 offset;

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (onTouch && coll.gameObject.name == "Player")
        {
            if (levelName == "")
            {
                TeleportManager.GM_TELEPORT.Teleport(teleportToKey);
            }
            else
            {
                TeleportManager.GM_TELEPORT.Teleport(teleportToKey, levelName);
            }
        }
    }


}
