using UnityEngine;
using System.Collections;
using Fungus;


public class CallFungusBlock : MonoBehaviour {

    //name of flowchart to call
    public string flowchartToCall;
    //name of block to call
    public string blockToCall;

    //if the flowchart gets triggered when entering the trigger, or when you are inside and press input button
    public bool waitForUserInput;

    //if the flowchart should only activate once per game session.
    public bool triggerOnce;

    public bool inTrigger;


    void CallFungus(string flowchart, string block)
    {

        if (triggerOnce)
        {
            Collider2D triggerCollider = GetComponent<Collider2D>();
            triggerCollider.enabled = false;
        }

        //paused game when dialogue appears.
        //game gets unpaused in Fungus / "Stop" block event.
        GameManager.GM.inDialogue = true;
        GameManager.GM.SetPaused(true);

        //getting reference to all the flowcharts
        Flowchart[] flowcharts = GameManager.GM.flowcharts.transform.GetComponentsInChildren<Flowchart>();

        //the actual flowchart that will get called
        Flowchart fc;

        //finding the right flowchart by name before calling any block
        for(int i = 0; i < flowcharts.Length; i++)
        {
            if(flowcharts[i].name == flowchartToCall)
            {
                fc = flowcharts[i];
                fc.ExecuteBlock(blockToCall);
            }
        }

        GameManager.GM.interact = false;
    }

    //if the player enters trigger and it does not want to wait for user input.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            if(!waitForUserInput)
            {
                CallFungus(flowchartToCall, blockToCall);
            }

            inTrigger = true;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            inTrigger = false;
        }
    }

    void Update()
    {
        if (GameManager.GM.gamePaused)
            return;

        //if you are waiting for user input
        if(inTrigger)
        {
            if (waitForUserInput && GameManager.GM.interact)
            {
                CallFungus(flowchartToCall, blockToCall);
            }
        }
    }

}
