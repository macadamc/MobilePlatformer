/**
 * This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
 * It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fungus
{
    [CommandInfo("Flow", 
                 "Stop", 
                 "Stop executing the Block that contains this command.")]
    [AddComponentMenu("")]
    public class Stop : Command
    {       
        public override void OnEnter()
        {
            StopParentBlock();

            //unpaused the game when called.
            //Stop() must be called to end a fungus block and unpause the game
            GameManager.GM.inDialogue = false;
            GameManager.GM.SetPaused(false);

        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }
    }

}