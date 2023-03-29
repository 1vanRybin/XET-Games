using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    private PlayerController _controls;
    private static Animator _animator;
    public float speed;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _controls = new PlayerController();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveDirection = _controls.Player.Move.ReadValue<Vector2>();
        switch (moveDirection.x)
        {
            case > 0:
                _animator.SetInteger("Direction", 1);
                break;
            case < 0:
                _animator.SetInteger("Direction", -1);
                break;
            default:
                _animator.SetInteger("Direction", 0);
                break;
        }
        GetComponent<Rigidbody2D>().velocity = speed * moveDirection;
    }
        

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
}