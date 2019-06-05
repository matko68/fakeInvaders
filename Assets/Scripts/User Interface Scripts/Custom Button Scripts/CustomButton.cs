using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{

    public Image CustomButtonImage;
    public Text CustomButtonText;

    public int AlphaPercentageDeselected;
    public int AlphaPercentageSelected;

    private Action _action;

    private float _alphaDeselected;
    private float _alphaSelected;

    private void Awake()
    {

        if (CustomButtonImage == null && CustomButtonText == null)
            return;

        _action = GetComponent<Action>();

        _alphaDeselected = Mathf.Clamp(AlphaPercentageDeselected / 100.0f, 0.0f, 1.0f);
        _alphaSelected = Mathf.Clamp(AlphaPercentageSelected / 100.0f, 0.0f, 1.0f);

        Deselect();

    }

    public void Act()
    {
        if (_action)
            _action.Act();
    }

    public void Deselect()
    {
        if (CustomButtonImage)
        {
            Color color = CustomButtonImage.color;
            color.a = _alphaDeselected;
            CustomButtonImage.color = color;
        }
    }

    public void Select()
    {
        if (CustomButtonImage)
        {
            Color color = CustomButtonImage.color;
            color.a = _alphaSelected;
            CustomButtonImage.color = color;
        }
    }

}
