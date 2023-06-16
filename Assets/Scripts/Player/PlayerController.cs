using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]  int score = 5;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            score += 5;
           StartCoroutine( LeaderBoard.instance.SubmitScoreRoutine(score));
        }
    }
}
