using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Done_Main", LoadSceneMode.Single);
    }

    public void LoadFakeMenuScene(){
        SceneManager.LoadScene("Fake_Menu", LoadSceneMode.Single);
    }
}