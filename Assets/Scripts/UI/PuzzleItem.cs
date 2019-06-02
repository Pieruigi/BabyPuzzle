using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleItem : MonoBehaviour
{
    PuzzleAsset puzzleAsset;

    Image image;
    Button button;

    MenuManager menuManager;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(StartPuzzle);
        Reset();
    }

    private void Start()
    {
        menuManager = GameObject.FindObjectOfType<MenuManager>();
    }

    public void Init(PuzzleAsset puzzleAsset)
    {
        this.puzzleAsset = puzzleAsset;
        image.sprite = puzzleAsset.Background;
        //image.color = Color.white;
        button.interactable = true;
    }

    public void Reset()
    {
        puzzleAsset = null;
        image.sprite = null;
        //image.color = Color.black;
        button.interactable = false;
    }

    public void StartPuzzle()
    {
        GameObject.FindObjectOfType<PuzzleController>().CreatePuzzle(puzzleAsset);
        menuManager.ShowInGameMenu();
    }
}
