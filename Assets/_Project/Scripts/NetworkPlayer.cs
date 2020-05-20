using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer  : MonoBehaviourPun, IPunObservable 
{
    private Vector3 RemotePlayerPosition;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            RemotePlayerPosition = (Vector3)stream.ReceiveNext();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine)
            return;

        var LagDistance = RemotePlayerPosition - transform.position;

        if (LagDistance.magnitude > 5f)
        {
            transform.position = new Vector3(transform.position.x, RemotePlayerPosition.y);
            LagDistance = Vector3.zero;
        }

        if (LagDistance.magnitude < 0.11f)
        {
            Paddle.otherPlayerMovement = 0;
        }
        else
        {
            Paddle.otherPlayerMovement = LagDistance.normalized.y;
        }
    }
}
