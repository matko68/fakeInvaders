using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    #region MENU MANAGER PROPERTY

    private static MenuManager _MM;

    public static MenuManager MM
    {
        get
        {
            if (_MM == null)
                _MM = FindObjectOfType<MenuManager>();
            return _MM;
        }
    }

    #endregion

    #region TITLE SCREEN ACTIVE PROPERTY

    private bool _titleScreenActive = true;
    
    public bool TitleScreenActive
    {
        get
        {
            return _titleScreenActive;
        }
    }

    #endregion

    public GameObject MainMenu;
    public GameObject NewGameMenu;
    public GameObject HighscoreMenu;

    public GameObject TitleScreen;

    private void Awake()
    {

        #region MENU MANAGER PROPERTY SET UP

        if (_MM == null)
            _MM = this;

        if (_MM != this)
            Destroy(gameObject);

        #endregion

        TitleScreen.SetActive(true);

        MainMenu.GetComponent<CustomButtonMenu>().Deactivate();
        MainMenu.SetActive(false);

        NewGameMenu.GetComponent<CustomButtonMenu>().Deactivate();
        NewGameMenu.SetActive(false);

        HighscoreMenu.GetComponent<CustomButtonMenu>().Deactivate();
        HighscoreMenu.SetActive(false);

    }

    public void CloseTitleScreen()
    {

        _titleScreenActive = false;

        TitleScreen.SetActive(false);

        MainMenu.SetActive(true);
        MainMenu.GetComponent<CustomButtonMenu>().Activate();

    }

    #region METHODS FOR MENU BUTTONS

    public void GoToNewGameMenu()
    {

        MainMenu.GetComponent<CustomButtonMenu>().Deactivate();
        MainMenu.SetActive(false);

        NewGameMenu.SetActive(true);
        NewGameMenu.GetComponent<CustomButtonMenu>().Activate();

    }

    public void GoToHighscoreMenu()
    {

        MainMenu.GetComponent<CustomButtonMenu>().Deactivate();
        MainMenu.SetActive(false);

        HighscoreMenu.SetActive(true);
        HighscoreMenu.GetComponent<CustomButtonMenu>().Activate();

    }

    public void ReturnToMainMenu()
    {

        if (NewGameMenu.activeSelf)
        {
            NewGameMenu.GetComponent<CustomButtonMenu>().Deactivate();
            NewGameMenu.SetActive(false);
        }

        if (HighscoreMenu.activeSelf)
        {
            HighscoreMenu.GetComponent<CustomButtonMenu>().Deactivate();
            HighscoreMenu.SetActive(false);
        }

        MainMenu.SetActive(true);
        MainMenu.GetComponent<CustomButtonMenu>().Activate();

    }

    #endregion

}
