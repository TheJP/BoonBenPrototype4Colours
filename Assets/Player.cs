using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Vector2 move = Vector2.zero;

    private void FixedUpdate() => GetComponent<Rigidbody>().velocity = move * speed;

    public void OnMove(InputValue input) => move = input.Get<Vector2>();
    public void OnRotateRight() => transform.Rotate(0, 0, -90);
    public void OnRotateLeft() => transform.Rotate(0, 0, 90);

    private void OnCollisionStay(Collision collision)
    {
        var wall = collision.collider.GetComponent<Wall>();
        if (wall == null || wall.Colour == Colours.White) { return; }

        Vector2? lastBadContact = null;
        foreach (var contact in collision.contacts)
        {
            // Becuase we use 3D colliders: only use the plane in front.
            if(contact.point.z > 0) { continue; }

            Debug.DrawRay(contact.point, contact.normal * 5f, Color.red, 3f);
            var angle = Vector2.SignedAngle(contact.normal, transform.right);
            Colours playerColour;
            if (Mathf.Abs(Mathf.DeltaAngle(angle, 180)) <= 46) playerColour = Colours.Green;
            else if (Mathf.Abs(Mathf.DeltaAngle(angle, -90)) <= 46) playerColour = Colours.Orange;
            else if (Mathf.Abs(Mathf.DeltaAngle(angle, 90)) <= 46) playerColour = Colours.Blue;
            else if (Mathf.Abs(Mathf.DeltaAngle(angle, 0)) <= 46) playerColour = Colours.Purple;
            else
            {
                Debug.LogWarning("Should not be reached");
                continue;
            }

            if (playerColour != wall.Colour)
            {
                if (lastBadContact == null) lastBadContact = contact.point;
                else if ((lastBadContact.Value - (Vector2)contact.point).sqrMagnitude < 0.001f) Debug.Log("Corner contact"); // very rare edge case
                else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                lastBadContact = contact.point;
            }
        }

    }
}
