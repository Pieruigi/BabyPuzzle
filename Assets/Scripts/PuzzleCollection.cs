using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleAsset
{
    [SerializeField]
    Sprite bg;
    public Sprite Background{
        get { return bg; }
    }

    [SerializeField]
    List<Sprite> pieces;
    public IList<Sprite> Pieces
    {
        get
        {
            return pieces.AsReadOnly();
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class PuzzleCollection : MonoBehaviour
{
    [SerializeField]
    List<PuzzleAsset> puzzles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PuzzleAsset GetPuzzleAsset(int id)
    {
        return puzzles[id];
    }

    public IList<PuzzleAsset> GetPuzzleAssets(int numOfPieces)
    {
        Debug.Log(string.Format("Get puzzle all assets with {0} number of pieces", numOfPieces));
        Debug.Log(puzzles.FindAll(p => p.Pieces.Count == numOfPieces).Count);
        return puzzles.FindAll(p => p.Pieces.Count == numOfPieces).AsReadOnly();
    }
}
