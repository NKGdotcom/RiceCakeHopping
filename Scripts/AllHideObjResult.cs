using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHideObjResult : MonoBehaviour
{
    [SerializeField] private GameObject[] hideObjects;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject hopping;
    [SerializeField] private GameObject feetShadow;
    /// <summary>
    /// リザルト用にすべて削除
    /// </summary>
    public void AllHideObj()
    {
        if(hideObjects != null)
        {
            foreach(GameObject obj in hideObjects)
            {
                obj.SetActive(false);
            }
        }
        table.SetActive(false);
        hopping.SetActive(false);
        feetShadow.SetActive(false);
    }

    /// <summary>
    /// 食えないじゃないかはちゃぶ台もアニメーションで用いるため使用しない
    /// </summary>
    public void FailedResultHideObj()
    {
        if (hideObjects != null)
        {
            foreach (GameObject obj in hideObjects)
            {
                obj.SetActive(false);
            }
        }
        hopping.SetActive(false);
        feetShadow.SetActive(false);
    }
}
