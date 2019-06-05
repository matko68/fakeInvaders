using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGoToNewGameMenu : Action
{

    public override void Act()
    {
        if (MenuManager.MM)
            MenuManager.MM.GoToNewGameMenu();
    }

}
