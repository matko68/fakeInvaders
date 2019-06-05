using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Canvas DifficultyCanvas;
    public Canvas HighscoreCanvas;
    public Canvas MenuCanvas;
    public Canvas OpeningCanvas;

    public Text TitleText;
    public float TitleTextFadeDuration = 1.5f;

    private void Start()
    {

        if (GameManager.GM && GameManager.GM.InGame == false)
        {
            StartCoroutine(StartOpeningScene());
        }

        else
        {
            OpeningCanvas.enabled = false;
            MenuCanvas.enabled = true;
            HighscoreCanvas.enabled = false;
            DifficultyCanvas.enabled = false;
        }

    }

    private IEnumerator StartOpeningScene()
    {

        GameManager.GM.InGame = true;

        HighscoreCanvas.enabled = false;
        DifficultyCanvas.enabled = false;

        OpeningCanvas.enabled = true;
        MenuCanvas.enabled = false;
        yield return new WaitForSeconds(1.5f);

        #region TITLE FADING

        if (TitleText != null)
        {

            Vector2 alpha = new Vector2(1, 0);
            Color color = TitleText.color;
            float timer = 0;

            while (timer <= TitleTextFadeDuration)
            {
                timer += Time.deltaTime;
                color.a = Mathf.Lerp(alpha.x, alpha.y, timer / TitleTextFadeDuration);
                TitleText.color = color;
                yield return null;
            }

            color.a = alpha.y;
            TitleText.color = color;

        }

        #endregion

        OpeningCanvas.enabled = false;
        yield return new WaitForSeconds(0.5f);

        MenuCanvas.enabled = true;
        yield return null;

    }

    #region METHODS FOR UI BUTTONS

    public void DisplayDifficultyCanvas()
    {
        MenuCanvas.enabled = false;
        DifficultyCanvas.enabled = true;
    }

    public void DisplayHighscoreCanvas()
    {
        MenuCanvas.enabled = false;
        HighscoreCanvas.enabled = true;
    }

    public void Return()
    {

        if (DifficultyCanvas.enabled)
        {
            DifficultyCanvas.enabled = false;
            MenuCanvas.enabled = true;
            return;
        }

        if (HighscoreCanvas.enabled)
        {
            HighscoreCanvas.enabled = false;
            MenuCanvas.enabled = true;
            return;
        }

    }

    #endregion

}
