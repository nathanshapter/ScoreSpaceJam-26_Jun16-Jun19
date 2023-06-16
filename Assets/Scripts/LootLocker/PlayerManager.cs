using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{


    public LeaderBoard leaderBoard;
    public TMP_InputField playerNameInputField;


    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player Name");
                StartCoroutine(LeaderBoard.instance.FetchTopHighScoresRoutine());
            }
            else
            {
                Debug.Log("could not set player name " + response.Error);
            }
        });
    }
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("player was logged in");
                PlayerPrefs.SetString("Player ID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator SetUpRoutine()
    {
        yield return LoginRoutine();
        yield return leaderBoard.FetchTopHighScoresRoutine();
    }

    private void Start()
    {
        StartCoroutine(SetUpRoutine());
        leaderBoard = FindObjectOfType<LeaderBoard>();
    }
}
