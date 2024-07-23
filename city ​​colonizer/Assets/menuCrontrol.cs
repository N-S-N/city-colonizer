using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuCrontrol : MonoBehaviour
{
    [SerializeField] List<words> words = new List<words>();
    [SerializeField] List<wordsunity> wordsCena = new List<wordsunity>();
    [SerializeField] TMP_InputField inputRei,inputName;

    int mapsave;
    private void Awake()
    {
        loud();
        seting();
    }

    #region buttom
    public void playload(int map)
    {
        PlayerPrefs.SetFloat("load", 1);
        PlayerPrefs.SetFloat("wordNumber", map);
        saveInventory();
        SceneManager.LoadSceneAsync(1);
    }
    public void playcreat(int map)
    {
        PlayerPrefs.SetFloat("load", 0);     
        mapsave = map;
        
    }

    public void destoyword(int map)
    {
        words[map].name = "new word";
        words[map].iscreat = false;
        words[map].rei = "";
        saveInventory();
        seting();
    }

    public void names()
    {
        words[mapsave].iscreat = true;
        words[mapsave].rei = inputRei.text;
        words[mapsave].name = inputName.text;
        saveInventory();
        PlayerPrefs.SetFloat("wordNumber", mapsave);
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region load and save 
    public void seting()
    {
        for (int i = 0; i< wordsCena.Count ;i++)
        {
            wordsCena[i].neme.text = words[i].name;
            if (words[i].iscreat)
            {
                wordsCena[i].start.SetActive(true);
                wordsCena[i].creat.SetActive(false);
            }
            else
            {
                wordsCena[i].start.SetActive(false);
                wordsCena[i].creat.SetActive(true);
            }
        }
    }

    public void saveInventory()
    {
        InvantoryData3 data = new InvantoryData3();
        for (int i = 0; i < words.Count; i++)
        {
            words itemdata = new words(words[i].name, words[i].iscreat, words[i].rei);
            data.slotData.Add(itemdata);
        }

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText("menu.json", jsonData);
    }

    void loud()
    {
        if (File.Exists("menu.json"))
        {
            string jsonData = File.ReadAllText("menu.json");

            InvantoryData3 lineMapdafe = JsonUtility.FromJson<InvantoryData3>(jsonData);
            words = lineMapdafe.slotData;
        }
    }
    #endregion
}

#region list
[System.Serializable]
public class wordsunity
{
    public TMP_Text neme;
    public GameObject creat;
    public GameObject start;
}
[System.Serializable]
public class words
{
    public string name;
    public string rei;
    public bool iscreat;
    public words(string name,bool iscreat,string rei)
    {
        this.name = name;
        this.rei = rei;
        this.iscreat = iscreat;

    }
}

[System.Serializable]
public class InvantoryData3
{
    public List<words> slotData = new List<words>();
}

#endregion
