using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
   public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
