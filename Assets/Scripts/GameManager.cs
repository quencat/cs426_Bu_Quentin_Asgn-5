using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class GameManager : NetworkBehaviour
{
    public static GameManager manager;
    public static int score1 = 0;
    public static int score2 = 0;
    public static int score3 = 0;
    public static int score4 = 0;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;

    public bool gameIsOver = false;

    // TOTAL POINTS NEEDED TO END THE GAME, 1 for default cat + 4 for magic cats + 12 for current number of other cats spawned
    public static int totalPoints = 1 + 4 + 12; // 17 cats total



    //public GameObject canvas;
    //int playerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
    }
/*    void OnPlayerConnected()
    {
        playerCount++;
        Debug.Log(playerCount.ToString());
    }
*/
    //public void OnClientConnect()
    //{
    //    //canvas.gameObject.SetActive(true);
    //    Debug.Log("here");
    //}

    // Update is called once per frame
    void Update()
    {
        setScoreText();


        //canvas.gameObject.SetActive(true);
    }
    private void Awake()
    {
        setScoreText();
        manager = this;
    }

    //set score to display
    void setScoreText()
    {
        text1.text = score1.ToString();
        text2.text = score2.ToString();
        text3.text = score3.ToString();
        text4.text = score4.ToString();
    }


    //function to identify player collision and add points
    public void AddPoint(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit by dog"+ collision.gameObject.GetComponent<NetworkIdentity>().netId);
            if (collision.gameObject.GetComponent<NetworkIdentity>().netId.Value%4 == 1)
            {
                score1++;
            } else if (collision.gameObject.GetComponent<NetworkIdentity>().netId.Value % 4 == 2)
            {
                score2++;
            }else if (collision.gameObject.GetComponent<NetworkIdentity>().netId.Value % 4 == 3)
            {
                score3++;
            } else if (collision.gameObject.GetComponent<NetworkIdentity>().netId.Value % 4 == 0)
            {
                score4++;
            }

            Debug.Log("score = " + (score1 + score2+ score3 + score4));
            if ((score1 + score2+ score3 + score4) >= totalPoints || GameObject.FindGameObjectsWithTag("cat") == null) {
              Debug.Log("got a score large enough to end game ");
              gameIsOver = true;

              Invoke("gameOver", 3f);
            }
        }

    }

    string getWinnerString() {
      string toRet = "";

      int maxScore = 0;
      if (score1 > maxScore) {
        maxScore = score1;
      }
      if (score2 > maxScore) {
        maxScore = score2;
      }
      if (score3 > maxScore) {
        maxScore = score3;
      }
      if (score4 > maxScore) {
        maxScore = score4;
      }

      if (score1 == maxScore) {
        toRet += "Player 1 ";
      }

      if (score2 == maxScore) {
              toRet += "Player 2";
      }


      if (score3 == maxScore) {
              toRet += "Player 3";
      }


      if (score4 == maxScore) {
              toRet += "Player 4";
            }

      return toRet;

    }

    //end game
    void gameOver()
    {
        Debug.Log("game over");
        gameState.winners = getWinnerString();
        SceneManager.LoadScene("WinnerScene");
    }
}
