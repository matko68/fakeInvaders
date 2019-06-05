using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionExit : Action
{

    public override void Act()
    {
        if (GameManager.GM)
            GameManager.GM.Exit();
    }

}
