using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject splashScreen;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject inGameMenu;

    private GameObject currentMenu;

    // Start is called before the first frame update
    void Start()
    {
        Util.HideAllChildren(transform);
        //ShowSplashScreen();
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSplashScreen()
    {
        StartCoroutine( CoroShowSplashScreen() );
    }

    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
    }

    public void ShowInGameMenu()
    {
        ShowMenu(inGameMenu);
    }

    private void ShowMenu(GameObject menu)
    {
        if (menu == currentMenu)
            return;

        if (currentMenu != null)
            currentMenu.SetActive(false);

        currentMenu = menu;
        currentMenu.SetActive(true);
    }

    //private void HideAll()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Transform c = transform.GetChild(i);
    //        c.gameObject.SetActive(false);
    //    }

    //}

    private IEnumerator CoroShowSplashScreen()
    {
        ShowMenu(splashScreen);

        yield return new WaitForSeconds(5);

        ShowMainMenu();
    }
}
