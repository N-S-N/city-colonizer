using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class Randow_Map : MonoBehaviour
{
    #region variaves 
    [Header("prefeb do terreno")]
    [SerializeField] GameObject ociano;
    [SerializeField] GameObject terra;
    
    [SerializeField] GameObject terrainclinada1;
    [SerializeField] GameObject terrainclinada4;
    [SerializeField] GameObject objPai;
    [Header("Colideer")]
    [SerializeField] Collider camcoleder;

    [Header("angulo do terendo")]
    [SerializeField] int maximoDeIngloinacao;

    [Header("porcentagem do mudar o elevo e a soma tem que dar 100")]
    [SerializeField] float manter;
    [SerializeField] float subir;
    [SerializeField] float decer;
    [SerializeField] int NivelDoDegrau ;

    [Header("tamanho do Map e lista do map de relevo")]
    [SerializeField]public int MaxMap;
    public List<RandowMapInt> lineMap;
    [SerializeField] TMP_Text texto;
    [SerializeField] Quaternion rotation;
    private int LevelAtual = 1;

    [SerializeField] florest flot;
    #endregion

    #region fucion
    private void Awake()
    {
        if (PlayerPrefs.GetFloat("load") == 0) {

            Line();
            //RandowForLine();

            RandowForLevel();

            //ajuste de coleder
            coliddeer();

            //seve
            saveInventory();
        }
        else
        {
            loud();
        }
    }

    void coliddeer()
    {
        camcoleder.transform.localScale = new Vector3(MaxMap,1,MaxMap);
        camcoleder.transform.position = new Vector3((MaxMap/2)-0.5f, camcoleder.transform.position.y, (MaxMap/2) -0.5f);

    }
    void Line()
    {
        subir += manter;
        decer += subir;
    }
    void RandowForLine()
    {
        for (int i = 0; i < MaxMap; i++)//pafez A Primeira linha
        {
            for (int j = 0; j < MaxMap; j++)
            {
                if (j < 2)//prin]meira linha
                {
                    lineMap[i].Column[j] = 0;
                }
                else
                {
                    float Randow = Random.Range(0.000f, decer);
                    //Debug.Log(Randow + " " + i);
                    if (Randow <= manter)
                    {
                        lineMap[i].Column[j] = lineMap[i].Column[j - 1];
                    }
                    else if (Randow <= subir)
                    {
                        lineMap[i].Column[j] = lineMap[i].Column[j - 1] + 1;
                    }
                    else if (Randow <= decer)
                    {
                        if (lineMap[i].Column[j - 1] == 0)
                        {
                            lineMap[i].Column[j] = lineMap[i].Column[j - 1];
                        }
                        else
                        {
                            lineMap[i].Column[j] = lineMap[i].Column[j - 1] - 1;
                        }
                    }
                }
            }
         
        }
        confim();
        //colunas
        /*
        for (int i = 0; i < MaxMap; i++)//lionha
        {
            for (int j = 1; j < MaxMap; j++)//coluna
            {
                if (i < 2)//primeira linha
                {
                    lineMap[i].Column[j] = 0;
                }
                else
                {
                    float Randow = Random.Range(0.000f, 100.000f);

                    if (Randow <= manterColuna)
                    {
                        lineMap[j].Column[i] = lineMap[j - 1].Column[i];
                    }
                    else if (Randow <= decerColuna)
                    {
                        if (lineMap[j - 1].Column[i] == 0)
                        {
                            lineMap[j].Column[i] = lineMap[j - 1].Column[i];
                        }
                        else
                        {
                            lineMap[j].Column[i] = lineMap[j - 1].Column[i] - 1;
                        }
                    }
                    else
                    {
                        lineMap[j].Column[i] = lineMap[j - 1].Column[i] + 1;
                    }

                    confim(i, j);
                }
            }
        }
        */
    }

    int colunaanteriou = 0;
    int antrerialcoluna;
    void RandowForLevel()
    {
        int RandowLivel = Random.Range(1, NivelDoDegrau);
        antrerialcoluna += RandowLivel;
        colunaanteriou = antrerialcoluna;
        if (colunaanteriou < MaxMap)
        {
            lineMap[0].Column[colunaanteriou] = LevelAtual;

            for (int j = 0; j < MaxMap; j++)
            {
                float Randow = Random.Range(0.000f, 100.000f);

                if (Randow <= manter)
                {
                    if (colunaanteriou < MaxMap && lineMap[j].Column[colunaanteriou] != 0)
                    {
                        colunaanteriou++;
                        if (colunaanteriou < MaxMap)
                        {
                            lineMap[j].Column[colunaanteriou] = LevelAtual;
                        }
                    }
                    else
                    {
                        if(colunaanteriou < MaxMap)
                        {
                            lineMap[j].Column[colunaanteriou] = LevelAtual;
                        }
                    }
                }
                else if (Randow <= subir)
                {
                    colunaanteriou++;
                    if (colunaanteriou < MaxMap)
                    {
                        lineMap[j].Column[colunaanteriou] = LevelAtual;
                    }
                }
                else
                {
                    if (colunaanteriou < MaxMap && lineMap[j].Column[colunaanteriou] != 0)
                    {
                        colunaanteriou++;
                        if (colunaanteriou < MaxMap)
                        {
                            lineMap[j].Column[colunaanteriou] = LevelAtual;
                        }

                    } 
                    else if (colunaanteriou < MaxMap && colunaanteriou - 1 >= 0 && lineMap[j].Column[colunaanteriou-1] != 0)
                    {
                        if (colunaanteriou < MaxMap)
                        {
                            lineMap[j].Column[colunaanteriou] = LevelAtual;
                        }
                    }
                    else 
                    {
                        if (colunaanteriou != 2)
                        {
                            colunaanteriou--;
                            if (colunaanteriou < MaxMap)
                            {

                                lineMap[j].Column[colunaanteriou] = LevelAtual;
                            }
                        }
                        else
                        {
                            lineMap[j].Column[colunaanteriou] = LevelAtual;
                        }
                    }
                }
                
            }
            LevelAtual++;
            RandowForLevel();
            return;
        }
        confim();
    }

    int Livel;
    void confim()
    {
        for (int i = 0; i < MaxMap; i++)
        {
            Livel = lineMap[i].Column[0];
            for (int j = 0; j < MaxMap; j++)
            {
                if (lineMap[i].Column[j] != 0 && Livel < lineMap[i].Column[j])
                {
                    if (Livel + 1 < lineMap[i].Column[j])
                    {
                        Livel++;
                        lineMap[i].Column[j] = Livel;
                    }
                    else
                    {
                        Livel = lineMap[i].Column[j];
                    }
                }
                else
                {
                    lineMap[i].Column[j] = Livel;
                }
            }    
        }
        for (int i = 0; i < MaxMap; i++)
        {
            for (int j = 0; j < MaxMap; j++)
            {
                if (i == 0) break;
                Vector2 compar = new Vector2(lineMap[i].Column[j], lineMap[i - 1].Column[j]);

                lineMap[i].Column[j] = (int)Mathf.Clamp(compar.x, compar.y - maximoDeIngloinacao, compar.y + maximoDeIngloinacao);
            }
        }
        mapping();
        flot.vergetacao();
    }
    void mapping()
    {
        for (int i = lineMap.Count; i > MaxMap; i--)
        {
            lineMap.RemoveAt(i-1);
        }
        for (int i = 0; i < lineMap.Count; i++)
        {
            for (int j = lineMap[i].Column.Count; j > MaxMap; j--)
            {
                lineMap[i].Column.RemoveAt(j-1);
            }
        }
        for (int i = 0; i < lineMap.Count; i++)
        {
            for (int j = 0; j < MaxMap; j++)
            {
                GameObject spawm;
                if (lineMap[i].Column[j] == 0)
                {
                    spawm = Instantiate(ociano,new Vector3(i , lineMap[i].Column[j],j),gameObject.transform.rotation, objPai.transform);
                    spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                }
                else
                {
                    if (i-1 > 0 && i+1 < MaxMap && j > 0 && lineMap[i].Column[j] != lineMap[i].Column[j-1] &&
                        lineMap[i].Column[j] != lineMap[i-1].Column[j] && lineMap[i].Column[j] != lineMap[i + 1].Column[j])
                    {
                        spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                        spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    }
                    else if (i - 1 > 0 && j > 0 && lineMap[i].Column[j] != lineMap[i].Column[j - 1] && lineMap[i].Column[j] != lineMap[i - 1].Column[j])
                    {
                        spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                        spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    }
                    else if (i + 1 < MaxMap && j > 0 && lineMap[i].Column[j] != lineMap[i].Column[j - 1] && lineMap[i].Column[j] != lineMap[i + 1].Column[j])
                    {
                        spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                        spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    }
                    else if (j > 0 && lineMap[i].Column[j] != lineMap[i].Column[j - 1])
                    {
                        spawm = Instantiate(terrainclinada4, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                        spawm.transform.localRotation = rotation;
                    }
                    else
                    {
                        //Debug.Log(i + " " + lineMap[i + 1].Column[j]);
                        if (i == 0 && lineMap[i].Column[j] != lineMap[i + 1].Column[j])
                        {                      
                            spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                            spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        else if (i > 0 && lineMap[i].Column[j] != lineMap[i - 1].Column[j])
                        {
                            spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                            spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        }
                        else
                        {
                            if (i + 2 < MaxMap && j > 0 && lineMap[i + 1].Column[j] != lineMap[i + 1].Column[j - 1]
                                && lineMap[i + 1].Column[j] == lineMap[i + 2].Column[j])
                            {
                                //Debug.Log("aa");
                                spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                                spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                            }
                            else if (i - 1 > 0 && j > 0 && lineMap[i - 1].Column[j] != lineMap[i - 1].Column[j - 1]
                                && lineMap[i - 1].Column[j] == lineMap[i - 2].Column[j])
                            {

                                //Debug.Log("bbb");
                                spawm = Instantiate(terrainclinada1, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                                spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                            }
                            else
                            {

                                //Debug.Log("ccc");
                                spawm = Instantiate(terra, new Vector3(i, lineMap[i].Column[j], j), gameObject.transform.rotation, objPai.transform);
                                spawm.transform.localRotation = new Quaternion(0, 0, 0, 0);
                            }
                        }
                    }
                    
                }
                if(i == MaxMap - 1)
                {
                }
                
            }

        }

    }
    #endregion
    #region save
    private void saveInventory()
    {
        InvantoryData2 data = new InvantoryData2();
        for (int i = 0; i < lineMap.Count; i++)
        {
            RandowMapInt itemdata = new RandowMapInt(lineMap[i].Column);
            data.slotData.Add(itemdata);
        }

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText("mapgeneretion"+ PlayerPrefs.GetFloat("wordNumber")+".json ", jsonData);
    }

    void loud()
    {
        if (File.Exists("mapgeneretion"+ PlayerPrefs.GetFloat("wordNumber") +".json "))
        {
            string jsonData = File.ReadAllText("mapgeneretion"+ PlayerPrefs.GetFloat("wordNumber") + ".json ");

            InvantoryData2 lineMapdafe = JsonUtility.FromJson<InvantoryData2>(jsonData);

            lineMap = lineMapdafe.slotData;
            MaxMap = lineMap.Count;
            mapping();
        }
    }
    #endregion
}
#region list

[System.Serializable]
public class RandowMapInt
{
    public List<int> Column;
    public RandowMapInt(List<int> size)
    {
        this.Column = size;
    }
}

[System.Serializable]
public class InvantoryData2
{
    public List<RandowMapInt> slotData = new List<RandowMapInt>();
}
#endregion
