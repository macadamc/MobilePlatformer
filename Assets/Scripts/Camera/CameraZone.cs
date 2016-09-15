using UnityEngine;
using System.Collections;

public class CameraZone : MonoBehaviour {

    Bounds bounds;

	void Awake ()
    {
        bounds = GetComponent<BoxCollider2D>().bounds;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            if(CameraFollow.CAM_FOLLOW.cameraBounds != bounds)
            {
                CameraFollow.CAM_FOLLOW.SetCameraZone(bounds);

            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (CameraFollow.CAM_FOLLOW.cameraBounds == bounds)
                CameraFollow.CAM_FOLLOW.hasBounds = false;
        }
    }
}
