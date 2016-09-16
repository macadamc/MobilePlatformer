using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow CAM_FOLLOW;

    public Transform targetTransform;
    private Vector2 camOffset;
    private Vector2 offsetTarget;
    public Vector2 offsetModifier;

    public string horAxisName;
    public string verAxisName;

    public float camLerpSpd;
    public Bounds cameraBounds;

    private float camHeight;
    private float camWidth;
    public bool hasBounds;


	void Awake ()
    {
        if (CAM_FOLLOW == null)
            CAM_FOLLOW = this;
        else
        {
            if(CAM_FOLLOW != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        camHeight = Camera.main.orthographicSize * 2f;
        camWidth = camHeight * Camera.main.aspect;

    }

	// Update is called once per frame
	void FixedUpdate ()
    {

        LerpTowardTargetPos();

        if (GameManager.GM.gamePaused)
            return;

        if (CnControls.CnInputManager.GetAxisRaw(horAxisName) != 0 || CnControls.CnInputManager.GetAxisRaw(verAxisName) != 0)
            SetCameraOffset();

        camOffset = Vector2.Lerp(camOffset, offsetTarget, 0.1f);

    }

    public void SetAtTargetPos()
    {
        Vector3 newPos = targetTransform.position;
        newPos.z = -10;
        transform.position = newPos;

        if (hasBounds)
            ClampPosToBounds();
    }

    public void LerpTowardTargetPos()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, targetTransform.position + (Vector3)camOffset, camLerpSpd);
        newPos.z = -10;
        transform.position = newPos;

        if (hasBounds)
            ClampPosToBounds();
    }

    public void SetCameraZone(Bounds bounds)
    {
        cameraBounds = bounds;
        hasBounds = true;

        SetAtTargetPos();
    }

    public void ClampPosToBounds()
    {
        if (cameraBounds != null)
        {
            float clampedX = transform.position.x;
            float clampedY = transform.position.y;


            if (clampedX < cameraBounds.min.x + (camWidth / 2) || clampedX > cameraBounds.max.x - (camWidth / 2))
            {
                ClampPos();
            }
            if (clampedY < cameraBounds.min.y + (camHeight / 2) || clampedY > cameraBounds.max.y - (camHeight / 2))
            {
                ClampPos();
            }
        }
    }
    public void ClampPos()
    {
        float clampedX = Mathf.Clamp(transform.position.x, cameraBounds.min.x + (camWidth / 2), cameraBounds.max.x - (camWidth / 2));
        float clampedY = Mathf.Clamp(transform.position.y, cameraBounds.min.y + (camHeight / 2), cameraBounds.max.y - (camHeight / 2));
        Vector3 newPos = new Vector3(clampedX, clampedY, transform.position.z);
        transform.position = newPos;
    }

    public void SetCameraOffset()
    {
        offsetTarget = new Vector2(CnControls.CnInputManager.GetAxis(horAxisName) * offsetModifier.x, CnControls.CnInputManager.GetAxis(verAxisName) * offsetModifier.y);

    }

}
