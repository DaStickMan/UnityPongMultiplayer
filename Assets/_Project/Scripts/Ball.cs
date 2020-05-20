using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviourPun
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    public Vector3 RemotePlayerPosition;

    private bool lauchStarted;

    public static Vector2 netWorkPosition;
    public static Vector2 netWorkVelocity;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        lauchStarted = false;
        startPosition = transform.position;
        photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.PlayerList.Length > 1 && !lauchStarted && PhotonNetwork.IsMasterClient)
            Lauch();

        if (!photonView.IsMine)
        {
            rb.position = Vector2.MoveTowards(rb.position, netWorkPosition, Time.fixedDeltaTime);            
        }
    }
    
    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        Lauch();
    }
            
    private void Lauch()
    {
        lauchStarted = true;

        float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float y = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y) * Time.deltaTime;
    }
}
