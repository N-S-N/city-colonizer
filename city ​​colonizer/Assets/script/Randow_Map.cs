using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Randow_Map : MonoBehaviour
{
    #region variaves 

    [Header("angulo do terendo")]
    [SerializeField] int maximoDeIngloinacao;

    [Header("porcentagem do mudar o elevo e a soma tem que dar 100")]
    [SerializeField] float manter;
    [SerializeField] float subir;
    [SerializeField] float decer;

    [Header("tamanho do Map e lista do map de relevo")]
    [SerializeField] int MaxMap;
    public List<RandowMapInt> lineMap;
    [SerializeField] TMP_Text texto;
    #endregion

    #region fucion
    private void Awake()
    {
        Line();
        Randow();
        confim();
        mapping();
    }
    void Line()
    {
        subir += manter;
        decer += subir;
    }
    void Randow()
    {
        for(int i = 0;i < MaxMap; i++)
        {
            for (int j = 0; j < MaxMap; j++)
            {
                if(j == 0)//prin]meira linha
                {
                    lineMap[i].Column[j] = 0;
                }
                else
                {
                    float Randow = Random.Range(0.000f, 100.000f);
                    //Debug.Log(Randow + " " + i);
                    if (Randow <= manter)
                    {
                        lineMap[i].Column[j] = lineMap[i].Column[j - 1];   
                    }
                    else if (Randow <= subir)
                    {
                        lineMap[i].Column[j] = lineMap[i].Column[j - 1] + 1;                 
                    }
                    else if(Randow <= decer)
                    {
                        if (lineMap[i].Column[j-1] == 0)
                        {
                            lineMap[i].Column[j] = lineMap[i].Column[j - 1];
                        }
                        else
                        {
                            lineMap[i].Column[j] = lineMap[i].Column[j - 1]-1;
                        }
                    }
                }
            }
        }
    }
    void confim()
    {
        for (int i = 0; i < MaxMap; i++)
        {
            for (int j = 0; j < MaxMap; j++)
            {
                if (i == 0) break; 
                Vector2 compar = new Vector2(lineMap[i].Column[j], lineMap[i-1].Column[j]);

                lineMap[i].Column[j] = (int)Mathf.Clamp(compar.x, compar.y - maximoDeIngloinacao, compar.y + maximoDeIngloinacao);
            }
        }
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

        string mapstring = "";
        for (int i = 0; i < lineMap.Count; i++)
        {
            for (int j = 0; j < MaxMap; j++)
            {
                mapstring +=  lineMap[i].Column[j].ToString() + " ";
            }
            mapstring += "\n";
        }
        texto.text = mapstring;
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
