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
    [SerializeField] TextMeshProUGUI textTooLong;
    int nameCap = 10;
    public void SetPlayerName()
    {
        if(playerNameInputField.text.Length > nameCap)
        {
            print("name too long");
            textTooLong.text = $"Names are capped at {nameCap} digits";
            return;
           
        }
        else { textTooLong.text = string.Empty; }

        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player Name");
                StartCoroutine(FindObjectOfType<LeaderBoard>().FetchTopHighScoresRoutine());
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
