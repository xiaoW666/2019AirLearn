using UnityEngine;

public class Boss : MonoBehaviour
{
    //生命 攻击  击杀经验 分数
    public int hp, atk,exp,fen;
    //移动的x坐标和之前的x坐标
    int x, oldx;
    //速度 移动计时 攻击计时1 攻击计时2
    float speed, time, ftime1, ftime2;
    //子弹缓冲池
    Transform list;
    Player player;
    GameObject GameOver;
    void Start()
    {
        time = -4;
        ftime1 = -1;
        ftime2 = -1;
        speed = 0.05f;
        transform.parent = GameObject.Find("MainGame/Dynamic").transform;
        list = GameObject.Find("MainGame/EBulletList").transform;
        player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        ftime1 += Time.deltaTime;
        ftime2 += Time.deltaTime;

        Move();
        Fire();
    }
    private void Move()
    {
        if (time < 0)
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (time >= 3)
        {
            if (x % 2 == 1)
            {
                transform.Translate(Vector3.left * speed);
            }
            else
            {
                transform.Translate(Vector3.right * speed);
            }
            if (time >= 4 + Random.Range(0, 1f))
            {
                time = 0;
                int temp = Random.Range(1, 3);
                if (oldx == temp)
                {
                    temp++;
                }
                oldx = x;
                x = temp;
            }
        }
    }
    //Boss开火
    void Fire()
    {
        Transform pots = transform.Find("Pots");
        //1至3秒内随机开火
        if (ftime1 >= Random.Range(1f, 3f))
        {
            for (int i = 0; i < pots.childCount; i++)
            {
                CreateBullet(pots.GetChild(i).position, 0);
            }
            ftime1 = 0;
        }
        //3至5秒内随机发射扇形子弹
        if (ftime2 > Random.Range(3f, 5f))
        {
            for (int i = -7; i < 8; i++)
            {
                CreateBullet(pots.GetChild(Random.Range(0,pots.childCount)).position, i * 10);
            }
            ftime2 = 0;
        }
    }
    //Boss生成子弹
    void CreateBullet(Vector3 p, float ax)
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
            bullet.transform.Rotate(Vector3.forward * ax);
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
            bullet.transform.Rotate(Vector3.forward * ax);
            bullet.GetComponent<Bullet>().SetAtk(atk);
        }
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
            Instantiate(Resources.Load<GameObject>("Prefabs/VFX/Explosions/explosion_boss"), transform.position, transform.rotation);
            player.setExp(player.getExp() + exp);
            player.setCount(player.getCount() + 1);
            player.setFen(player.getFen() + fen);
            player.setCoin(player.getCoin() + Random.Range(10, 20));
            Destroy(gameObject);
            if (GameOver == null)
            {
                GameOver = GameObject.Find("UIRoot").transform.Find("End_Data").gameObject;
            }
            GameOver.SetActive(true);
            GameOver.transform.Find("SB").gameObject.SetActive(false);
            Invoke("SetTimeScale",1);
        }
    }
    public int getAtk()
    {
        return atk;
    }
    public int getExp()
    {
        return exp;
    }
}
