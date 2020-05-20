using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gamemanager : MonoBehaviour, IPunObservable
{

    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;


    private int Player1Score;
    private int Player2Score;

    private static TextMeshProUGUI player1Score;
    private static TextMeshProUGUI player2Score;

    private void Start()
    {
        player1Score = Player1Text.GetComponent<TextMeshProUGUI>();
        player2Score = Player2Text.GetComponent<TextMeshProUGUI>();
    }

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        Invoke("ResetPosition", 3);
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        Invoke("ResetPosition", 3);
    }
    
    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();

        var player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<Paddle>().Reset();

        var player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player2 != null)
            player2.GetComponent<Paddle>().Reset();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Player1Score.ToString());
            stream.SendNext(Player2Score.ToString());
        }
        else
        {
            Player1Text.GetComponent<TextMeshProUGUI>().text = (string)stream.ReceiveNext();
            Player2Text.GetComponent<TextMeshProUGUI>().text = (string)stream.ReceiveNext();
        }
    }
}
