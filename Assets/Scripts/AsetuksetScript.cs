using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AsetuksetScript : MonoBehaviour
{

    public void AsetuksetTakaisin()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void äänislider(float äänenvoimmakuus)
    {
        print(äänenvoimmakuus);
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
 