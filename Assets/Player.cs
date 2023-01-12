using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Vector2 move = Vector2.zero;

    private void FixedUpdate() => GetComponent<Rigidbody>().velocity = move * speed;

    public void OnMove(InputValue input) => move = input.Get<Vector2>();

    public void OnRotateRight() => transform.Rotate(0, 0, -90);
    public void OnRotateLeft() => transform.Rotate(0, 0, 90);
}
