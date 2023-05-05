using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PadCharge : MonoBehaviour
{
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
    private Image _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<Image>();
    }

    void Update()
    {
        _spriteRenderer.sprite = ChargeBatteryTrigger.ChargeLVL switch
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
