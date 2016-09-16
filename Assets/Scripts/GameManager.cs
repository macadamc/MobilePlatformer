using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    public bool gamePaused;
    public bool inDialogue;

    public GameObject pauseScreen;
    public GameObject touchControls;
    public GameObject pauseButton;

    public GameObject flowcharts;

    public bool interact;

	// Use this for initialization
	void Awake ()
    {
        if (GM == null)
            GM = this;
        else
        {
            if (GM != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        UpdatePauseScreen();
    }

    void Update()
    {
        if(CnControls.CnInputManager.GetButtonDown("Submit"))
        {
            TogglePause();
        }

        if(!interact && CnControls.CnInputManager.GetButtonDown("Fire1") && !gamePaused)
        {
            StartCoroutine(Interact());
        }
    }

    public void TogglePause()
    {
        if (inDialogue)
            return;

        gamePaused = !gamePaused;
        UpdatePauseScreen();
    }
    public void SetPaused(bool status)
    {
        gamePaused = status;
        UpdatePauseScreen();
    }

    public void UpdatePauseScreen()
    {
        if (gamePaused)
        {
            if(!inDialogue)
            {
                pauseScreen.SetActive(true);
            }
            else
            {
                pauseButton.SetActive(false);
            }

            touchControls.SetActive(false);
        }
        else
        {
            pauseScreen.SetActive(false);
            touchControls.SetActive(true);
            pauseButton.SetActive(true);
        }
    }

    public void StartInteract()
    {
        StartCoroutine(Interact());
    }
    public IEnumerator Interact()
    {

        interact = true;
        yield return new WaitForSeconds(0.25f);
        interact = false;
    }
}
