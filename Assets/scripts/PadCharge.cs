using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCharge : MonoBehaviour
{
    public int charge;
    public Sprite charge0;
    public Sprite charge1;
    public Sprite charge2;
    public Sprite charge3;
    public Sprite charge4;
    public Sprite charge5;
    public Sprite charge6;
    public Sprite charge7;
    public Sprite charge8;
    public Sprite charge9;
    public Sprite charge10;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // if (_spriteRenderer == charge0)
        // {
        //     _spriteRenderer.sprite = charge0;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = charge switch
        {
            0 => charge0,
            1 => charge1,
            2 => charge2,
            3 => charge3,
            4 => charge4,
            5 => charge5,
            6 => charge6,
            7 => charge7,
            8 => charge8,
            9 => charge9,
            10 => charge10,
            _ => _spriteRenderer.sprite
        };
    }
}
