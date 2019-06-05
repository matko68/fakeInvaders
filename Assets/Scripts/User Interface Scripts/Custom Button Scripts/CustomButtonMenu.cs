using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButtonMenu : MonoBehaviour
{

    public List<CustomButton> CustomButtons = new List<CustomButton>();

    private bool _active;
    private int _index;

    private void Awake()
    {
        if (InputManager.IM)
            InputManager.IM.MenuInputEvent.AddListener(InputHandler);
    }

    private void InputHandler(InputType type)
    {

        if (CustomButtons.Count <= 0)
            return;

        if (_active == false)
            return;

        if (type == InputType.DOWN)
        {
            CustomButtons[_index].Deselect();
            _index = Mathf.Clamp(_index + 1, 0, CustomButtons.Count - 1);
            CustomButtons[_index].Select();
        }

        if (type == InputType.UP)
        {
            CustomButtons[_index].Deselect();
            _index = Mathf.Clamp(_index - 1, 0, CustomButtons.Count - 1);
            CustomButtons[_index].Select();
        }

        if (type == InputType.SELECT)
            CustomButtons[_index].Act();

    }

    private void ResetMenu()
    {

        if (CustomButtons.Count <= 0)
            return;

        _index = 0;
        CustomButtons[_index].Select();

        for (int index = 1; index < CustomButtons.Count; index++)
            CustomButtons[index].Deselect();

    }

    private IEnumerator Activation()
    {
        yield return new WaitForSeconds(0.15f);
        _active = true;
    }

    public void Activate()
    {
        ResetMenu();
        StartCoroutine(Activation());
    }

    public void Deactivate()
    {
        _active = false;   
    }

}
