using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");

        if (PhotonNetwork.IsMasterClient)
        {
            var player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(-6, 0, 0), Quaternion.identity);
            player.name = "Player1";
            player.tag = "Player1";
        }
        else
        {
            var player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(6, 0, 0), Quaternion.identity);
            player.name = "Player2";
            player.tag = "Player2";
        }

    }
}
