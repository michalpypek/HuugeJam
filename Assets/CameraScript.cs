using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Vector2 startPos, endPos;
    float moveDistance;
    public float startTime, speed, journeyLength, startScale;

    void Start()
    {
        startPos = transform.localPosition;
        startTime = Time.time;
        startScale = Camera.main.orthographicSize;
        journeyLength = startScale - 4.5f;
        moveDistance = Vector2.Distance(startPos, endPos);
    }

	void Update ()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        Camera.main.orthographicSize = Mathf.Lerp(startScale, 4.5f, fracJourney);

        float moveCovered = (Time.time - startTime) * speed;
        float fracDistance = moveCovered / moveDistance;

        transform.localPosition = new Vector3(Vector2.Lerp(startPos, endPos, fracDistance).x, Vector2.Lerp(startPos, endPos, fracDistance).y, -8);
    }
}
