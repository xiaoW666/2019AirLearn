using System;
using UnityEngine;

public class UI : MonoBehaviour
{
    UISlider hps, exps;
    UILabel hpl, expl,coin,fen,count;
    Player player;
    private void OnEnable()
    {
        hps = transform.Find("PlayerHp").GetComponent<UISlider>();
        exps = transform.Find("PlayerExp").GetComponent<UISlider>();
        hpl = transform.Find("PlayerHp/Label").GetComponent<UILabel>();
        expl = transform.Find("PlayerExp/Label").GetComponent<UILabel>();
        coin =transform.Find("coin/Label").GetComponent<UILabel>();
        fen = transform.parent.Find("End_Data/Label2/count").GetComponent<UILabel>();
        count = transform.parent.Find("End_Data/Label1/Dead_Enemy").GetComponent<UILabel>();
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
        }
        else
        {
            hpl.text = player.getHp() + "/" + player.getMaxhp();
            expl.text = player.getExp() + "/" + player.getNeedexp();
            coin.text = player.getCoin().ToString();
            fen.text = player.getFen().ToString();
            count.text = player.getCount().ToString();
            hps.value = (float)Math.Round((decimal)player.getHp() / player.getMaxhp(), 3);
            exps.value = (float)Math.Round((decimal)player.getExp() / player.getNeedexp(), 3);


        }

    }
}
