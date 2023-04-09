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

    private Dictionary<(float, float), Action> _setAnimator = new Dictionary<(float, float), Action>()
    {
        { (0, 0), () => _animator.SetInteger(Direction, 0) },
        { (1, 0), () => _animator.SetInteger(Direction, 1) },
        { (-1, 0), () => _animator.SetInteger(Direction, -1) },
        { (0, 1), () => _animator.SetInteger(Direction, 2) },
        { (0, -1), () => _animator.SetInteger(Direction, -2) },
        {((float)0.707107, (float)0.707107), () => _animator.SetInteger(Direction, 2)},
        {((float)-0.707107, (float)-0.707107), () => _animator.SetInteger(Direction, 1)},
        {((float)0.707107, (float)-0.707107), () => _animator.SetInteger(Direction, 1)},
        {((float)-0.707107, (float)0.707107), () => _animator.SetInteger(Direction, -1)},
    };
    private void Awake()
    {
        _controls = new PlayerController();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveDirection = _controls.Player.Move.ReadValue<Vector2>();
        _setAnimator[((float)moveDirection.x, (float)moveDirection.y)]();
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