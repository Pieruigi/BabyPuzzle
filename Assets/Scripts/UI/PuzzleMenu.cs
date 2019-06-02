using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMenu : MonoBehaviour
{
    [SerializeField]
    Transform content;

    PuzzleCollection puzzleCollection;

    IList<PuzzleAsset> puzzles;

    int itemsPerPage = 6;
    int pageNum = 0;

    List<Transform> items;

    // Start is called before the first frame update
    void Start()
    {
        puzzleCollection = GameObject.FindObjectOfType<PuzzleCollection>();
        items = new List<Transform>(content.GetComponentsInChildren<Transform>()).FindAll(t=>t != content.transform);
        Debug.Log("items:" + items.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int level)
    {
        Reset();

        puzzles = GetPuzzleAssets(level) ;
        
        Debug.Log(string.Format("Level {0} puzzle count:{1}", level, puzzles.Count));
        //foreach(PuzzleAsset p in puzzles)

        //int start = pageNum * itemsPerPage;
        ShowPuzzleItems();
       
    }

    private IList<PuzzleAsset> GetPuzzleAssets(int level)
    {
        int l = level + 2;
        
        return puzzleCollection.GetPuzzleAssets((int)Mathf.Pow((float)l, 2));
    }

    private void Reset()
    {
        ResetItems();

        puzzles = null;
        pageNum = 0; 

    }

    private void ShowPuzzleItems()
    {
        int start = pageNum * itemsPerPage;
        for (int i = 0; i < itemsPerPage; i++)
        {

            if (puzzles.Count <= start + i)
                return;
            PuzzleItem item = items[i].GetComponent<PuzzleItem>();
            Debug.Log("sssss");

            item.Init(puzzles[start + i]);
        }
    }

    private void ResetItems()
    {
        for (int i = 0; i < itemsPerPage; i++) items[i].GetComponent<PuzzleItem>().Reset();
        
    }
}
