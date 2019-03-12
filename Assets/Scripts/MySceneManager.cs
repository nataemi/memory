using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    public static void RestartGame()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void RunRestartGame(){
        SetSize2x2();
        MySceneManager.RestartGame();
    }

    public void Play(){
        if (!AddButtons.sizeSetted){
            SetSize2x2();
        }
        SceneManager.LoadScene("GameScene");
    }

    public void SetSize2x2(){
        AddButtons.SetSize(2, 2);
    }

    public void SetSize2x4()
    {
        AddButtons.SetSize(2, 4);
    }

    public void SetSize4x4()
    {
        AddButtons.SetSize(4, 4);
    }

}
