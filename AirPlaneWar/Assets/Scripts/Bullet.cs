using UnityEngine;

public class Bullet : MonoBehaviour
{
    int atk;
    float speed, time;
    void Start()
    {
        speed = 0.2f;
    }
    private void OnEnable()
    {
        time = 0;
        if (transform.parent == null)
        {
            transform.parent = GameObject.Find("MainGame/Dynamic").transform;
        }
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        //子弹三秒后放入缓冲池
        if (time >= 5)
        {
            EnterPool();
        }
        transform.Translate(Vector3.up * speed);
    }
    //放入缓冲池
    void EnterPool()
    {
        if (transform.name == "Bullet1(Clone)")
        {
            transform.parent = GameObject.Find("MainGame/PBulletList").transform;
        }
        else
        {
            transform.parent = GameObject.Find("MainGame/EBulletList").transform;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.name == "Bullet1(Clone)" && other.tag =="Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.setHp(enemy.getHp() - atk);
            }

            Meteorite meteorite = other.GetComponent<Meteorite>();
            if (meteorite != null)
            {
                meteorite.setHp(meteorite.getHp() - atk);
            }
            
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.setHp(boss.getHp() - atk);
            }

            EnterPool();
        }
        if (transform.name== "Bullet2(Clone)" && other.tag=="Player")
        {
            Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
            Player.setHp(Player.getHp() - atk);
            EnterPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.name == "Bullet1(Clone)" && collision.transform.tag =="Enemy")
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
                enemy.setHp(enemy.getHp() - Player.getAtk());
            }

            Meteorite meteorite = collision.transform.GetComponent<Meteorite>();
            if (meteorite != null)
            {
                Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
                meteorite.setHp(meteorite.getHp() - Player.getAtk());
            }

            Boss boss = collision.transform.GetComponent<Boss>();
            if (boss != null)
            {
                Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
                boss.setHp(boss.getHp() - Player.getAtk());
            }
            EnterPool();
        }
        if (transform.name == "Bullet2(Clone)" && collision.transform.tag == "Player")
        {
            Player Player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
            Player.setHp(Player.getHp() - atk);
            EnterPool();
        }
    }
    public void SetAtk(int Atk ) 
    {
        atk = Atk;
    }
}
