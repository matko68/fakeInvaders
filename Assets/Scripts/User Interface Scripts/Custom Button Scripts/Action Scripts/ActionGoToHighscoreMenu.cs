using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGoToHighscoreMenu : Action
{

    public override void Act()
    {
        if (MenuManager.MM)
            MenuManager.MM.GoToHighscoreMenu();
    }

}
