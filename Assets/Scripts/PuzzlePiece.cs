

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    

    private SpriteRenderer spriteRend;

    private Vector3 upDefault;
    private float scaleDefault;

    private int dir = 0;

    private float elapsed;
    private float speed = 5;

    private float time;

    private float lerpStartScale;
    private Vector3 lerpStartUp;

    
    public bool Placed { get; private set; }

    public Vector2 PlacedSpot { get; private set; }

    private Vector3 startPos;
    private Quaternion startRot;

    AudioManager audioManager;

    //private float scale = 4.2f;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        upDefault = transform.up;
        time = 1 / speed;

        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dir != 0)
        {
            elapsed += Time.deltaTime * speed;

            Vector2 s;
            Vector3 u;
            if(dir == 1)
            {
                s = Vector2.Lerp(Vector2.one * lerpStartScale, Vector2.one * PuzzleController.PUZZLE_SCALE, elapsed / time);
                u = Vector2.Lerp(lerpStartUp, Vector3.up, elapsed / time);
            }
            else
            {
                s = Vector2.Lerp(Vector2.one * lerpStartScale, Vector2.one * scaleDefault, elapsed / time);
                u = Vector2.Lerp(lerpStartUp, upDefault, elapsed / time);
            }

            transform.localScale = s;
            transform.up = u;

            if (elapsed > time)
            {
                elapsed = 0;
                dir = 0;
            }
                

        }

    }

    public void SetSprite(Sprite sprite, float scale)
    {
        Reset();

        spriteRend.sprite = sprite;
        
        transform.localScale = Vector2.one * scale;
        scaleDefault = scale;
        spriteRend.sortingOrder = PuzzleController.ORDER_UNSELECTED;

        GetComponent<Collider2D>().enabled = true;
    }

    public Sprite GetSprite()
    {
        return spriteRend.sprite;
    }

    public void SetPlacedSpot(Vector2 spot)
    {
        PlacedSpot = spot;
    }

    public void Place()
    {
        //transform.localPosition = PlacedSpot;
        LeanTween.moveLocal(gameObject, PlacedSpot, 0.75f).setEaseInOutElastic();
        Placed = true;
        spriteRend.sortingOrder = PuzzleController.ORDER_PLACED;
        GetComponent<Collider2D>().enabled = false;

        audioManager.PlayDrop();
    }

    public void Reset()
    {
        spriteRend.sprite = null;
        transform.localScale = Vector2.one;
        scaleDefault = 1;
        
        elapsed = 0;
        dir = 0;
        spriteRend.sortingOrder = PuzzleController.ORDER_UNSELECTED;
        transform.up = upDefault;

        transform.position = startPos;
        transform.rotation = startRot;
        Placed = false;


    }

    private void Select()
    {
        dir = 1;
        elapsed = 0;
        spriteRend.sortingOrder = PuzzleController.ORDER_SELECTED;
        lerpStartScale = transform.localScale.x;
        lerpStartUp = transform.up;
    }

    private void Deselect()
    {
        dir = -1;
        elapsed = 0;
        spriteRend.sortingOrder = PuzzleController.ORDER_UNSELECTED;
        lerpStartScale = transform.localScale.x;
        lerpStartUp = transform.up;
    }
}
