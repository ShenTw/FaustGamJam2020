using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public List<GameObject> flowerList;

    private int flowerState = 0;
    public void OnDropFlower()
    {
        flowerState += 1;

        if (flowerState >= 4)
        {
            return;
        }

        foreach(GameObject go in flowerList)
        {
            go.SetActive(false);
        }
        flowerList[flowerState].SetActive(true);

       
    }
}
