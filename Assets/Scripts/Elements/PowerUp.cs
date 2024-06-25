using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Player _player;
    public Transform hearthMesh;

    public void startPowerUp(Player player)
    {
        _player = player;
        //NOTE:
        //baslangýc sizei 1.5 ve .3 saniyede loop icinde gitgel(yoyo) halinde scale degisir
        hearthMesh.DOScale(1f, .3f).SetLoops(-1, LoopType.Yoyo);

    }

    private void Update()
    {
        //NOTE:
        //kendi duzlemine bakmasý icin y degerini playerin y degerine esitledik
        var lookPosition = _player.transform.position;
        lookPosition.y = transform.position.y;

        //powerup surekli olarak playera dogur baksin
        transform.LookAt(lookPosition);
    }

}
