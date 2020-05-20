using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    public Vector3 startPosition;

    private float movement;
    private PhotonView myPV;

    public static float otherPlayerMovement;

    private void Start()
    {
        startPosition = transform.position;
        myPV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (myPV.IsMine)
        {
            movement = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, otherPlayerMovement * speed);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
