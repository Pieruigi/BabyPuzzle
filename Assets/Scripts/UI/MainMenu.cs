using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject levelMenu;

    [SerializeField]
    GameObject puzzleMenu;

    int currentPuzzleId = -1; // From puzzle collection

    MenuManager menuManager;
    PuzzleController puzzleController;

    // Start is called before the first frame update
    void Start()
    {
        currentPuzzleId = 1;
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        puzzleController = GameObject.FindObjectOfType<PuzzleController>();

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPuzzleMenu(int level)
    {
        levelMenu.SetActive(false);
        puzzleMenu.SetActive(true);

        puzzleMenu.GetComponent<PuzzleMenu>().Init(level);
    }

    public void ShowLevelMenu()
    {
        puzzleMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    //public void StartPuzzle()
    //{
    //    puzzleController.CreatePuzzle(currentPuzzleId);
    //    menuManager.ShowInGameMenu();
    //}

    private void Reset()
    {
        levelMenu.SetActive(true);
        puzzleMenu.SetActive(false);
    }

}
