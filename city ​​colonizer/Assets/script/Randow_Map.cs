using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Randow_Map : MonoBehaviour
{
    #region variaves 
    [Header("prefeb do terreno")]
    [SerializeField] GameObject ociano, terra,objPai;

    [Header("angulo do terendo")]
    [SerializeField] int maximoDeIngloinacao;

    [Header("porcentagem do mudar o elevo e a soma tem que dar 100")]
    [SerializeField] float manter;
    [SerializeField] float subir;
    [SerializeField] float decer;
    [SerializeField] int NivelDoDegrau ;


    [Header("tamanho do Map e lista do map de relevo")]
    [SerializeField] int MaxMap;
    public List<RandowMapInt> lineMap;
    [SerializeField] TMP_Text texto;

    private int LevelAtual = 1;

    #endregion

    #region fucion
    private void Awake()
    {
        Line();
        //RandowForLine();

        RandowForLevel();

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

    void RandowForLevel()
    {
        int RandowLivel = Random.Range(1, NivelDoDegrau);
        colunaanteriou += RandowLivel;
        if (NivelDoDegrau + colunaanteriou < MaxMap)
        {
            lineMap[0].Column[colunaanteriou] = LevelAtual;

            for (int j = 0; j < MaxMap - 1; j++)
            {
                float Randow = Random.Range(0.000f, 100.000f);

                if (Randow <= manter)
                {
                    if (lineMap[j].Column[colunaanteriou] != 0)
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
        }
        if (NivelDoDegrau + colunaanteriou < MaxMap)
        {
            LevelAtual++;
            RandowForLevel();
            return;
        }
        confim();
    }

    void confim()
    {
        for (int i = 0; i < MaxMap; i++)
        {      
            for (int j = 0; j < MaxMap; j++)
            {

            }

        }

        mapping();
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
                    spawm = Instantiate(ociano,new Vector3(i , lineMap[i].Column[j],j),transform.rotation, objPai.transform);
                }
                else
                {
                    spawm = Instantiate(terra, new Vector3(i, lineMap[i].Column[j],j), transform.rotation, objPai.transform);
                }
            }

        }
      
    }
    #endregion

}
#region list

[System.Serializable]
public class RandowMapInt
{
    public List<int> Column;
}

#endregion
