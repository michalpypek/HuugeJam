using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Vector2> prevPositions = new List<Vector2>();

    void Start()
    {
        foreach(GameObject obj in enemies)
        {
            prevPositions.Add(obj.transform.position);
        }
    }

    public void PrepareToRotate()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            prevPositions[i] = enemies[i].transform.localPosition;
        }
    }

    public void FixAfterRotation()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.localPosition = prevPositions[i];
            enemies[i].GetComponent<EnemyScript>().FixAfterRotation();
        }
    }
}
