using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{

    protected Text GetText(string textName)
    {
        var texts = GetComponentsInChildren<Text>();

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == textName)
                return texts[i];
        }

        return null;
    }

    protected Text GetText(string textChildName, string textName)
    {
        var child = transform.Find(textChildName);
        var texts = child.GetComponentsInChildren<Text>();

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == textName)
                return texts[i];
        }

        return null;
    }

    protected Button GetButton(string buttonName)
    {
        var buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == buttonName)
            {
                buttons[i].onClick.RemoveAllListeners();
                return buttons[i];
            }
        }

        return null;
    }

    protected Button GetButton(string buttonChildName, string buttonName)
    {
        Transform child = transform.Find(buttonChildName);
        var buttons = child.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == buttonName)
            {
                buttons[i].onClick.RemoveAllListeners();
                return buttons[i];
            }
        }
        return null;
    }


    protected Button GetButton(Transform child, string buttonName)
    {
        var buttons = child.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == buttonName)
            {
                buttons[i].onClick.RemoveAllListeners();
                return buttons[i];
            }
        }

        return null;
    }

    protected void GetButton(Transform child, string buttonName, UnityAction action)
    {
        var buttons = child.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == buttonName)
            {
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(action);
            }
        }
    }



    protected Image GetImage(string ImageChildName, string ImageName)
    {
        Transform child = transform.Find(ImageChildName);
        var Images = child.GetComponentsInChildren<Image>();

        for (int i = 0; i < Images.Length; i++)
        {
            if (Images[i].name == ImageName)
                return Images[i];
        }

        return null;
    }

    protected Image GetImage(string ImageName)
    {
        var Images = GetComponentsInChildren<Image>();

        for (int i = 0; i < Images.Length; i++)
        {
            if (Images[i].name == ImageName)
                return Images[i];
        }

        return null;
    }



}
