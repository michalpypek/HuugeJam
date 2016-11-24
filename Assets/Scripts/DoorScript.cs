using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public SpriteRenderer spRenderer;
    public Sprite unlocked;
    public bool isUnlocked = false;

    public void Unlock()
    {
        spRenderer.sprite = unlocked;
        isUnlocked = true;
    }
}
