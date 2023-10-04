using System;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public float targetCameraFollowSensitivity = 0.02f;  // target lerpT when at initial base speed

    private float lerpT;
    private PlayerController pc;
    private float initialSpeed;

    private void Start()
    {
        pc = player.GetComponent<PlayerController>();

        initialSpeed = pc.speed;
    }

    private void Update()
    {
        Vector2 pos = Vector2.Lerp(transform.position, player.transform.position, lerpT);
        transform.position = new Vector3(pos.x, pos.y, -10f);
    }

    private void FixedUpdate()
    {
        // doing this so that as speed increases, the lerp increases as well as to not make the camera stray too far away
        // lerpT = (speed / initialSpeed) * targetSensitivity
        lerpT = targetCameraFollowSensitivity;
    }
}
