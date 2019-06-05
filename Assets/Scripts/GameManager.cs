using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StageDifficulty
{
    EASY,
    MEDIUM,
    HARD
}
public class GameManager : MonoBehaviour
{

    #region GAME MANAGER PROPERTY

    private static GameManager _GM;

    public static GameManager GM
    {
        get
        {
            if (_GM == null)
                _GM = FindObjectOfType<GameManager>();
            return _GM;
        }
    }

    #endregion

    #region GAME MODE PROPERTY

    private GameMode _gameMode = GameMode.MEDIUM;

    public GameMode GameMode
    {
        get
        {
            return _gameMode;
        }
    }

    #endregion
    public bool InGame = false;

    private StageDifficulty _difficulty = StageDifficulty.MEDIUM;

    public CustomEvent<int> SceneChangedEvent = new CustomEvent<int>();

    private void Awake()
    {
        if (_GM == null)
            _GM = this;

        if (_GM != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        #region GAME MANAGE PROPERTY SET UP

        if (_GM == null)
            _GM = this;

        if (_GM != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        #endregion

    }

    private void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void GoToNextLevel()
    {

        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index < SceneManager.sceneCountInBuildSettings)
            GoToLevel(index);

        else
            GoToLevel(0);

    }
    #region METHODS FOR UI BUTTONS

    public void SetDifficultyHard()
    {
        _difficulty = StageDifficulty.HARD;
    }

    public void SetDifficultyMedium()
    {
        _difficulty = StageDifficulty.MEDIUM;
    }

    public void SetDifficultyEasy()
    {
        _difficulty = StageDifficulty.EASY;
    }

    public void ReturnToMenu()
    {
        GoToLevel(0);
    }

    public void NewGame()
    {
        GoToLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
    #region METHODS FOR MENU BUTTONS

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame(GameMode gameMode)
    {
        _gameMode = gameMode;
        GoToLevel(1);
    }

    #endregion

}