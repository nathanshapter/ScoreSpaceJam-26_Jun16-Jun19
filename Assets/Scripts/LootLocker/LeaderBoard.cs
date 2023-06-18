using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
  //  public static LeaderBoard instance;

    int leaderBoardID = 15159;

    public TextMeshProUGUI playerNames, playerScores;

    
    private void Awake()
    {
     //   if (instance == null)
        {
        //    instance = this;
         //   DontDestroyOnLoad(this);
        }
    //    else { Destroy(this.gameObject); }
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderBoardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
      
    }

    public IEnumerator FetchTopHighScoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderBoardID, 15, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Name(s)\n";
                string tempPlayerScores = "Score(s)\n";
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name + "\n";
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id + "\n";
                    }
                    tempPlayerScores += members[i].score + "";
                    tempPlayerScores += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed: + " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
