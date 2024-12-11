using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class florest : MonoBehaviour
{
    [SerializeField]Randow_Map map;
    public List<ObjMap> ObjOnMapList = new List<ObjMap>();
    [Header("porcentagem do mudar o elevo e a soma tem que dar 100")]
    [SerializeField] float avore;
    [SerializeField] float mineiro;
    [SerializeField] float nada;
    [SerializeField] public List<ObjMapprefeb> prevebMap = new List<ObjMapprefeb>();
    [SerializeField] LayerMask leyer;
    [SerializeField]public Transform pai;

    private void Awake()
    {
        mineiro += avore;
        nada += mineiro;
        if (PlayerPrefs.GetFloat("load") == 1)
        {
            loud();
        }
            //vergetacao();
        }

    public  void vergetacao()
    {

        for (int i = 0; i < map.MaxMap; i++)
        {
            for(int j = 0; j < map.MaxMap; j++)
            {

                if (map.lineMap[i].Column[j] != 0)
                {
                    if (i - 1 > 0 && i + 1 < map.MaxMap && j > 0 && map.lineMap[i].Column[j] != map.lineMap[i].Column[j - 1] &&
                        map.lineMap[i].Column[j] != map.lineMap[i - 1].Column[j] && map.lineMap[i].Column[j] != map.lineMap[i + 1].Column[j])
                    {
                        aletorio( i,  j);
                    }
                    else if (i - 1 > 0 && j > 0 && map.lineMap[i].Column[j] != map.lineMap[i].Column[j - 1] && map.lineMap[i].Column[j] != map.lineMap[i - 1].Column[j])
                    {
                        aletorio( i,  j);
                    }
                    else if (i + 1 < map.MaxMap && j > 0 && map.lineMap[i].Column[j] != map.lineMap[i].Column[j - 1] && map.lineMap[i].Column[j] != map.lineMap[i + 1].Column[j])
                    {
                        aletorio( i,  j);
                    }
                    else if (j > 0 && map.lineMap[i].Column[j] == map.lineMap[i].Column[j - 1])
                    {

                        aletorio( i,  j);
                    }

                }
            }
        }
    }

    void aletorio(int i, int j)
    {
        float Randowvegetacao = Random.Range(0.00f, nada);
;
        if (Randowvegetacao <= avore)
        {
            GameObject spawm = Instantiate(prevebMap[0].obj, new Vector3(i, map.lineMap[i].Column[j] + 1, j), transform.rotation, pai);
            ObjOnMapList.Add(new ObjMap(spawm, new Vector3(i, map.lineMap[i].Column[j] + 1, j), spawm.transform.rotation, prevebMap[0].size,0));
        }
        else if (Randowvegetacao <= mineiro)
        {
            GameObject spawm = Instantiate(prevebMap[1].obj, new Vector3(i, map.lineMap[i].Column[j] + 1, j), transform.rotation, pai);
            ObjOnMapList.Add(new ObjMap(spawm, new Vector3(i, map.lineMap[i].Column[j] + 1, j), spawm.transform.rotation, prevebMap[1].size,1));
        }
    }

    public void saveInventory()
    {
        InvantoryData data = new InvantoryData();
        for (int i = 0;i< ObjOnMapList.Count;i++) 
        {
            if (ObjOnMapList[i].prefebcontrucao != -1 && ObjOnMapList[i].obj != null)
            {
                ObjMap itemdata = new ObjMap(null, ObjOnMapList[i].position, ObjOnMapList[i].Rotecion, ObjOnMapList[i].size, ObjOnMapList[i].prefebcontrucao);
                data.slotData.Add(itemdata);
            }
        }

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText("construcaoMap"+ PlayerPrefs.GetFloat("wordNumber") + ".json ", jsonData);
    }
    void loud()
    {
        if (File.Exists("construcaoMap"+ PlayerPrefs.GetFloat("wordNumber") + ".json "))
        {
            string jsonData = File.ReadAllText("construcaoMap"+ PlayerPrefs.GetFloat("wordNumber") + ".json ");

            InvantoryData lineMapdafe = JsonUtility.FromJson<InvantoryData>(jsonData);

            ObjOnMapList = lineMapdafe.slotData;
            spawm();
        }
    }

    void spawm()
    {
        for (int i = 0;i< ObjOnMapList.Count;i++)
        {
            if (ObjOnMapList[i].prefebcontrucao != -1) 
            {
                GameObject spawm = Instantiate(prevebMap[ObjOnMapList[i].prefebcontrucao].obj, ObjOnMapList[i].position, transform.rotation, pai);
                ObjOnMapList[i].obj = spawm;
            }
        }
    }
}

[System.Serializable]
public class ObjMap
{
    public GameObject obj;
    public Vector3 position;
    public Vector3 size;
    public Quaternion Rotecion;
    public int prefebcontrucao;
    public ObjMap(GameObject obj, Vector3 vecposition, Quaternion Rotecion, Vector3 size, int prefebcontrucao)
    {
        this.Rotecion = Rotecion;
        this.obj = obj;
        this.position = vecposition;
        this.size = size;
        this.prefebcontrucao = prefebcontrucao;
    }
}
[System.Serializable]
public class ObjMapprefeb
{
    public GameObject obj;
    public Vector3 size;
}

[System.Serializable]
public class InvantoryData
{
    public List<ObjMap> slotData = new List<ObjMap>();
}
