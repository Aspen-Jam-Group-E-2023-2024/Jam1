using System;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public float cameraFollowSensitivity = 0.05f;

    private void Update()
    {
        Vector2 pos = Vector2.Lerp(transform.position, player.transform.position, cameraFollowSensitivity);
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }
}
