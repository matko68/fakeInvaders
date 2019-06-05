using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    #region INPUT MANAGER PROPERTY

    private static InputManager _IM;

    public static InputManager IM
    {
        get
        {
            if (_IM == null)
                _IM = FindObjectOfType<InputManager>();
            return _IM;
        }
    }

    #endregion

    public CustomEvent<InputType> MenuInputEvent = new CustomEvent<InputType>();

    public CustomEvent<InputType> InputEvent = new CustomEvent<InputType>();
    public PlayerController Player;
    public ShootingManager PlayerShootingManager;

    private float timer;
    private float delta = 0.015f;
    public float ShootingWaitTime = 0.15f;

    private bool _inGame;

    private void Awake()
    {

        #region INPUT MANAGER PROPERTY SET UP

        if (_IM == null)
            _IM = this;

        if (_IM != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        #endregion

        if (Player == null) Player = FindObjectOfType<PlayerController>();

        _inGame = (SceneManager.GetActiveScene().buildIndex == 0) ? false : true;

        if (GameManager.GM)
            GameManager.GM.SceneChangedEvent.AddListener(SceneChangedHandler);

    }

    private void Update()
    {

        if (_inGame == false)
        {

            if (MenuManager.MM && MenuManager.MM.TitleScreenActive)
                if (Input.anyKeyDown)
                    MenuManager.MM.CloseTitleScreen();

            if (MenuManager.MM == null || MenuManager.MM.TitleScreenActive == false)
            {

                if (Input.GetKeyDown(KeyCode.DownArrow))
                    MenuInputEvent.Invoke(InputType.DOWN);

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    MenuInputEvent.Invoke(InputType.UP);

                if (Input.GetKeyDown(KeyCode.Space))
                    MenuInputEvent.Invoke(InputType.SELECT);

            }

        }

        if (_inGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.GM)
                GameManager.GM.ReturnToMenu();

            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            direction = direction.normalized;

            if (Player) Player.Move(direction);

            if (Input.GetKey(KeyCode.Space))
            {

                PlayerShootingManager.SetContinousShooting(true);

                if (Mathf.Abs(timer) < delta)
                    PlayerShootingManager.Shoot();

                timer += Time.deltaTime;

                if (timer > ShootingWaitTime)
                    timer = 0.0f;

            }

            else
            {
                timer = 0.0f;
                PlayerShootingManager.SetContinousShooting(false);
            }
        }

    }

    private void SceneChangedHandler(int index)
    {
        _inGame = (index == 0) ? false : true;
    }

}
