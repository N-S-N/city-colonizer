using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
    [SerializeField] GameObject Puase;
    [SerializeField] florest mapObj;

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

    public void continuir()
    {
        Time.timeScale = 1.0f;
        Puase.SetActive(false);
    }

    public void menu()
    {
        mapObj.saveInventory();
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    public void quit()
    {
        mapObj.saveInventory();
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Time.timeScale = 1.0f;
    }
}
