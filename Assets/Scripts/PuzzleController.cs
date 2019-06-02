using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public const string SPOT_GROUP_TAG = "SpotGroup";
    public const string PIECE_LAYER = "PieceLayer";
    public const float PUZZLE_SCALE = 1f;

    public const int ORDER_PLACED = 10;
    public const int ORDER_UNSELECTED = 20;
    public const int ORDER_SELECTED = 30;

    [SerializeField]
    SpriteRenderer backgroundImage;

    [SerializeField]
    GameObject puzzleFrame;

    [SerializeField]
    SpriteRenderer puzzleImage;

    [SerializeField]
    SpriteRenderer scatteredGroupX4;

    [SerializeField]
    SpriteRenderer scatteredGroupX9;

    [SerializeField]
    SpriteRenderer scatteredGroupX16;

    [SerializeField]
    SpriteRenderer scatteredGroupX25;

    [SerializeField]
    ParticleSystem completedParticle;

    //[SerializeField]
    List<PuzzlePiece> scatteredPieceList;

    PuzzleAsset puzzleAsset;        

    private float scaleX4 = 0.6f;
    private float scaleX9 = 0.6f;
    private float scaleX16 = 0.6f;
    private float scaleX25 = 0.6f;

    private float scaleOrig = 1;//4.2f;

    private bool ready;

    private Transform selected;

    private Vector2 selOffset;

    private Vector2[] placedSpots;

    AudioManager audioManager;

    SpriteRenderer currentScatteredGroup;
    float currentScale = 0;

    // Start is called before the first frame update
    void Start()
    {
        Util.HideAllChildren(transform);
        Util.HideAllChildren(scatteredGroupX4.transform);
        Util.HideAllChildren(scatteredGroupX9.transform);
        Util.HideAllChildren(scatteredGroupX16.transform);
        Util.HideAllChildren(scatteredGroupX25.transform);

        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
            return;

        MousePick();

        MouseDrag();
    }

    public void CreatePuzzle(PuzzleAsset puzzleAsset)
    {
        Reset();

        //backgroundImage.gameObject.SetActive(true);
        //puzzleFrame.SetActive(true);

        this.puzzleAsset = puzzleAsset;
        
        //currentScatteredGroup = GetScatteredGroup();
        //Util.ShowAllChildren(currentScatteredGroup.transform);

        Init();

        

        //currentScatteredGroup.sprite = puzzleAsset.Background;
        

        ScatterPieces();

        CreateSpots();

        Debug.Log("bg:" + puzzleImage.sprite.rect);
        Debug.Log("bg:" + puzzleImage.sprite.bounds.extents.x);


    }

    public void RestartPuzzle()
    {
        if (puzzleAsset == null)
            return;

        CreatePuzzle(puzzleAsset);
    }

    private void Reset()
    {
        ready = false;

        if(scatteredPieceList != null)
        {
            for (int i = 0; i < scatteredPieceList.Count; i++)
            {
                scatteredPieceList[i].Reset();
            }
        }

        selOffset = Vector2.zero;
        selected = null;
        placedSpots = null;

        
        if(currentScatteredGroup)
            Util.HideAllChildren(currentScatteredGroup.transform);

        currentScatteredGroup = null;
        backgroundImage.gameObject.SetActive(false);
        puzzleFrame.SetActive(false);
    }

    private void ScatterPieces()
    {

        List<Sprite> tmp = new List<Sprite>(puzzleAsset.Pieces);

        int count = scatteredPieceList.Count;

        //float scale = 1;
        //if (count == 4)
        //    scale = scaleX4;

        for (int i=0; i<count; i++)
        {
            int r = Random.Range(0, tmp.Count);

            scatteredPieceList[i].SetSprite(tmp[r], currentScale);
            tmp.Remove(tmp[r]);
            
        }

        ready = true;
    }

    void Init()
    {
        backgroundImage.gameObject.SetActive(true);
        puzzleFrame.SetActive(true);

        currentScale = GetCurrentScatteredScale();

        currentScatteredGroup = GetScatteredGroup();
        puzzleImage.sprite = puzzleAsset.Background;

        Util.ShowAllChildren(currentScatteredGroup.transform);



        //GameObject sg = scatteredGroupX4.gameObject;//GameObject.FindGameObjectWithTag(SPOT_GROUP_TAG);
        GameObject sg = currentScatteredGroup.gameObject;//GameObject.FindGameObjectWithTag(SPOT_GROUP_TAG);
        scatteredPieceList = new List<PuzzlePiece>();

        foreach (PuzzlePiece t in sg.GetComponentsInChildren<PuzzlePiece>())
        {
            scatteredPieceList.Add(t);
        }
    }

    private SpriteRenderer GetScatteredGroup()
    {
        SpriteRenderer ret = null;
        switch (puzzleAsset.Pieces.Count)
        {
            case 4:
                ret = scatteredGroupX4;
                break;
            case 9:
                ret = scatteredGroupX9;
                break;
            case 16:
                ret = scatteredGroupX16;
                break;
            case 25:
                ret = scatteredGroupX25;
                break;
        }

        return ret;
    }

    private float GetCurrentScatteredScale()
    {
        float ret = 0;
        switch (puzzleAsset.Pieces.Count)
        {
            case 4:
                ret = scaleX4;
                break;
            case 9:
                ret = scaleX9;
                break;
            case 16:
                ret = scaleX16;
                break;
            case 25:
                ret = scaleX25;
                break;
        }

        return ret;
    }

    private void CreateSpots()
    {
        List<Sprite> pieces = new List<Sprite>(puzzleAsset.Pieces);
        int size = (int)Mathf.Sqrt(pieces.Count);

        placedSpots = new Vector2[pieces.Count];

        float dist = puzzleAsset.Background.bounds.extents.x * 2f / size;
        float offset = -puzzleAsset.Background.bounds.extents.x + puzzleAsset.Background.bounds.extents.x / size;


        for (int i = 0; i < size; i++)
        {
            
            for (int j = 0; j < size; j++)
            {
               
                int id = i * size + j % size;
                float x = offset + dist*j;
                float y = offset + dist*i;

               
                //GameObject g = new GameObject();
                //g.transform.parent = bg.transform;
                //g.transform.localScale = Vector3.one;
                //Vector3 pos = Vector3.zero;
                //pos.x = x;
                //pos.y = y;
                //g.transform.localPosition = pos;

                placedSpots[id] = new Vector2(x, y);

                scatteredPieceList.Find(s => s.GetSprite() == pieces[id]).SetPlacedSpot(new Vector2(x, y));


            }
        }
    }


    private void MousePick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!selected)
            {
                Vector3 stw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(stw, Vector3.forward, LayerMask.GetMask(PIECE_LAYER));
                if (hit)
                {
                    Debug.Log("Hit:" + hit.collider.gameObject);
                    if (!hit.transform.GetComponent<PuzzlePiece>().Placed)
                    {
                        selected = hit.transform;
                        selected.SendMessage("Select");
                        selOffset = new Vector2(selected.position.x, selected.position.y) - new Vector2(stw.x, stw.y);
                    }
                }
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (selected)
            {
                selected.SendMessage("Deselect");
                selected = null;
            }
            
        }
            
    }

    private void MouseDrag()
    {
        if (selected == null)
            return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        pos.z = 0;
        selected.position = pos + (Vector3)selOffset;

        //Debug.Log(selected.GetComponent<SpriteRenderer>().sprite.rect);

        PuzzlePiece piece = selected.GetComponent<PuzzlePiece>();

        if((piece.PlacedSpot - new Vector2(piece.transform.localPosition.x, piece.transform.localPosition.y)).magnitude < 0.4f)
        {
            piece.Place();
            selected = null;

            if (CheckCompleted())
            {
                StartCoroutine(CompletedCoroutine());
            }
        }

    }

    private bool CheckCompleted()
    {
        foreach (PuzzlePiece piece in scatteredPieceList)
            if (!piece.Placed)
                return false;

        return true;
    }

    IEnumerator CompletedCoroutine()
    {
        yield return new WaitForSeconds(1f);

        //ParticleSystem ps = GameObject.Instantiate(completedParticle, scatteredGroupX4.transform.position, Quaternion.identity);
        ParticleSystem ps = GameObject.Instantiate(completedParticle, currentScatteredGroup.transform.position, Quaternion.identity);

        Debug.Log("Completed");
        ps.Play();

        audioManager.PlayWin();

        while (ps.isPlaying)
            yield return null;

        GameObject.Destroy(ps.gameObject);

    }
}
