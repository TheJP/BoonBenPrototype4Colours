using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detector : MonoBehaviour
{
    [field: SerializeField]
    public Colours Colour { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        var wall = other.GetComponent<Wall>();
        if (wall == null || wall.Colour == Colours.White) { return; }

        if (Colour != wall.Colour)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
