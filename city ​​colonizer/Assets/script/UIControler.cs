using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
    [SerializeField] GameObject Puase;
    [SerializeField] florest mapObj;

    //opem menu or close menu
    public void buttomMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(!Puase.activeInHierarchy)
            {
                Time.timeScale = 0f;
                Puase.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                Puase.SetActive(false);
            }

        }
    }

    //Press to continue
    public void continuir()
    {
        Time.timeScale = 1.0f;
        Puase.SetActive(false);
    }

    //Press to return to the main menu
    public void menu()
    {
        mapObj.saveInventory();
        Time.timeScale = 1.0f;
        Puase.SetActive(false);
        SceneManager.LoadSceneAsync(0);
    }

    //Squeeze to exit
    public void quit()
    {
        mapObj.saveInventory();
        Time.timeScale = 1.0f;
        Puase.SetActive(false);
        Application.Quit();
    }

    //close game
    private void OnApplicationQuit()
    {
        Time.timeScale = 1.0f;
        Puase.SetActive(false);
    }
}
