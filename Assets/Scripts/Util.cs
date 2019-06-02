using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{

    public static void HideAllChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform c = parent.GetChild(i);
            c.gameObject.SetActive(false);
        }

    }

    public static void ShowAllChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform c = parent.GetChild(i);
            c.gameObject.SetActive(true);
        }

    }

}
