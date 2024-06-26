using UnityEngine;

public class Enemy : MonoBehaviour
{
    //血量 攻击 击杀经验
    public int hp, atk, exp, fen;
    //速度 移动计时 攻击计时1  攻击计时2
    float speed, time, ftime1, ftime2;
    //类型
    int type;
    //子弹缓冲池
    Transform list;
    Player player;
    void Start()
    {
        speed = 0.05f;
        time = -1;
        ftime1 = -1;
        ftime2 = -1;
        Destroy(gameObject, 10f);
        list = GameObject.Find("MainGame/EBulletList").transform;
        transform.parent = GameObject.Find("MainGame/Dynamic").transform;
        player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        ftime1 += Time.deltaTime;
        ftime2 += Time.deltaTime;
        time += Time.deltaTime;
        Move();
        Fire();
    }
    //敌人移动
    private void Move()
    {
        if (time < 3)
        {
            transform.Translate(Vector3.forward * speed);
        }

        if (time > 4)
        {
            if (type == 1)
            {
                transform.Translate(Vector3.forward * speed * 2f);
            }
            else
            {
                transform.Translate(Vector3.forward * speed * 1.5f);
                if (transform.position.x < 0)
                {
                    transform.Translate(Vector3.right * speed * 1.5f);
                }
                else
                {
                    transform.Translate(Vector3.left * speed * 1.5f);
                }
            }
        }

    }
    //敌人开火
    void Fire()
    {
        Transform pot = transform.Find("Pot");
        //1至2秒内随机开火
        if (ftime1 >= Random.Range(1f, 2f))
        {
            CreateBullet(pot.position, 0);
            ftime1 = 0;
        }
        //3至5秒内随机发射扇形子弹
        if (ftime2 > Random.Range(3f, 5f))
        {
            for (int i = -2; i < 3; i++)
            {
                if (i != 0)
                {
                    CreateBullet(pot.position, i * 20);
                }
            }
            ftime2 = 0;
        }
    }
    //敌人生成子弹
    void CreateBullet(Vector3 p, float x)
    {
        GameObject bullet;
        //获取缓冲池
        if (list.childCount == 0)
        {
            //缓冲池没子弹则生成
            bullet = Resources.Load<GameObject>("Prefabs/Bullet2");
            bullet = Instantiate(bullet);
            bullet.transform.parent = GameObject.Find("MainGame/Dynamic").transform;
            bullet.transform.position = p;
            bullet.transform.Rotate(Vector3.forward * x);
            bullet.GetComponent<Bullet>().SetAtk(atk);
        }
        else
        {
            //缓冲池有子弹则从缓冲池提取
            bullet = list.GetChild(0).gameObject;
            bullet.SetActive(true);
            bullet.transform.parent = GameObject.Find("MainGame/Dynamic").transform;
            bullet.transform.position = p;
            bullet.transform.rotation = Quaternion.Euler(-90, 0, 0);
            bullet.transform.Rotate(Vector3.forward * x);
            bullet.GetComponent<Bullet>().SetAtk(atk);
        }
    }
    public void setType(int Type)
    {
        type = Type;
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
            Instantiate(Resources.Load<GameObject>("Prefabs/VFX/Explosions/explosion_enemy"), transform.position, transform.rotation);
            player.setExp(player.getExp() + exp);
            player.setCoin(player.getCoin() + Random.Range(3, 10));
            player.setCount(player.getCount() + 1);
            player.setFen(player.getFen() + fen);
            Destroy(gameObject);

        }
    }

    public int getAtk()
    {
        return atk;
    }

}
