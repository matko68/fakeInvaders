using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNewGame : Action
{

    public GameMode GameMode = GameMode.MEDIUM;

    public override void Act()
    {
        if (GameManager.GM)
            GameManager.GM.NewGame(GameMode);
    }

}
