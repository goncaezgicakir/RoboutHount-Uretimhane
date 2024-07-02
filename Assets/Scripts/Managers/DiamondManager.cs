using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    [Header("Elements")]
    public GameDirector gameDirector;
    public Diamond diamondPrefab;
    public Transform diamondPlaceHolderParent;


    public void StartDiamondManager()
    {
        foreach (Transform ph in diamondPlaceHolderParent)
        {
            ph.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void SpawnDiamonds()
    {
        foreach (Transform ph in diamondPlaceHolderParent)
        {
            var newDiamond = Instantiate(diamondPrefab);
            newDiamond.transform.position = ph.position;
            newDiamond.StartDiamond(gameDirector);
        }
    }

}
