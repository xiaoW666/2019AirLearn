using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public int hp, atk;
    float speed;
    void Start()
    {
        speed = Random.Range(0.03f,0.08f);
        transform.parent = GameObject.Find("MainGame/Dynamic").transform;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed);
    }

    public int getHp()
    {
        return hp;
    }
    public void setHp(int Hp)
    {
        if (hp <= 0)
        {
            return;
        }
        hp = Hp;
        if (hp <= 0)
        {
            hp = 0;
            Instantiate(Resources.Load<GameObject>("Prefabs/VFX/Explosions/explosion_asteroid"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
            Player.setHp(Player.getHp() - atk);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
        Player.setHp(Player.getHp() - atk);

    }
    public int getAtk()
    {
        return atk;
    }
}
