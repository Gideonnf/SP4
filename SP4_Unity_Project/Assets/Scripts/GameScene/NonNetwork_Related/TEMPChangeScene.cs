using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMPChangeScene : MonoBehaviour
{
    public void NextSceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
