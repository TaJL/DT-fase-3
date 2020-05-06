using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire1")) 
        {
            SceneManager.LoadScene(1);
        }
    }
}
