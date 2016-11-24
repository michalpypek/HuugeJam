using UnityEngine;
using System.Collections;

public class GroundCheckScript : MonoBehaviour
{
    public bool isGrounded;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
