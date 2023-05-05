using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private PlayerController _controls;
    private static Animator _animator;
    public float speed;
    private Rigidbody2D _rigidbody2D;
    private static readonly int Direction = Animator.StringToHash("Direction");
    private void Awake()
    {
        _controls = new PlayerController();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveDirection = _controls.Player.Move.ReadValue<Vector2>();
        _animator.SetFloat("Horizontal", moveDirection.x);
        _animator.SetFloat("Vertical", moveDirection.y);
        _animator.SetFloat("Speed", moveDirection.SqrMagnitude());
        _rigidbody2D.velocity = speed * moveDirection;
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