using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Transform diamondPlaceHolderParent;
    public Diamond diamondPrefab;

    private void Start()
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
