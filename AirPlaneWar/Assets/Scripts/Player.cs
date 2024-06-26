using UnityEngine;

public class Player : MonoBehaviour
{
    //刚体
    Rigidbody rb;
    //当前 血量/攻击/经验/等级  最大血量 升级所需经验 金币数量 击毁数量 得分
    int hp, atk, exp, level, maxhp, needexp,potlevel, coin,count,fen;
    //速度 倾斜角度 攻击速度 当前攻击CD
    float speed, x, stime, cdtime;
    //发射点
    Transform firepots;
    //不同等级的属性和所需经验
    int[,] Attribute = { { 1, 100, 20, 100 }, { 2, 200, 30, 300 }, { 3, 500, 50, 500 } };
    void Start()
    {
        //初始化等级 攻击速度 攻击CD 速度   获取刚体
        setLevel(1);
        setPotLevel(1);
        setCoin(0);
        setFen(0);
        cdtime = 0.3f;
        stime = cdtime;
        speed = 500f;
        rb = GetComponent<Rigidbody>();
        transform.parent = GameObject.Find("MainGame/Dynamic").transform;
    }

    void Update()
    {
        //移动
        Move();
    }

    void FixedUpdate()
    {
        //攻击
        Atk();
    }
    //移动方法
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);
        if (h == 0)
        {
            x = Mathf.Lerp(x, 0, 0.1f);
        }
        else
        {
            if (h > 0)
            {
                x = Mathf.Lerp(x, -15, 0.1f);
            }
            else
            {
                x = Mathf.Lerp(x, 15, 0.1f);
            }
        }
        Quaternion q = Quaternion.AngleAxis(x, Vector3.forward);
        transform.rotation = q;
    }
    //攻击方法
    void Atk()
    {
        cdtime -= Time.deltaTime;
        if (cdtime <= 0)
        {
            //攻击CD为零则开火
            for (int i = 0; i < firepots.childCount; i++)
            {
                CreateBullet(firepots.GetChild(i).position);
            }
            cdtime = stime;
        }
    }
    //生成子弹 传入生成位置
    void CreateBullet(Vector3 p)
    {
        GameObject bullet;
        //获取缓冲池
        Transform list = GameObject.Find("MainGame/PBulletList").transform;

        if (list.childCount == 0)
        {
            //缓冲池没子弹则生成
            bullet = Resources.Load<GameObject>("Prefabs/Bullet1");
            bullet = Instantiate(bullet, p, bullet.transform.rotation);
            bullet.GetComponent<Bullet>().SetAtk(atk);
            bullet.transform.parent = GameObject.Find("MainGame/Dynamic").transform;
        }
        else
        {
            //缓冲池有子弹则从缓冲池提取
            bullet = list.GetChild(0).gameObject;
            bullet.SetActive(true);
            bullet.transform.parent = GameObject.Find("MainGame/Dynamic").transform;
            bullet.transform.position = p;
            bullet.GetComponent<Bullet>().SetAtk(atk);
        }

    }
    void UpPot()
    {
        //升级更新发射点
        firepots = transform.Find("P" + potlevel);
    }

    //获取 设置 属性
    public int getPotLevel()
    {
        return potlevel;
    }
    public void setPotLevel(int PotLevel)
    {
        potlevel = PotLevel;
        UpPot();
    }
    public int getHp()
    {
        return hp;
    }
    public void setHp(int Hp)
    {
        hp = Hp;
        if (hp <= 0)
        {
            hp = 0;
            Instantiate(Resources.Load<GameObject>("Prefabs/VFX/Explosions/explosion_player"), transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
    public int getAtk()
    {
        return atk;
    }

    public void setAtk(int atk)
    {
        this.atk = atk;
    }

    public int getExp()
    {
        return exp;
    }

    public void setExp(int exp)
    {
        this.exp = exp;
        if (exp >= needexp)
        {
            setLevel(level + 1);
        }
        if (level == 3)
        {
            exp = 0;
        }
    }

    public int getLevel()
    {
        return level;
    }
    public void setLevel(int Level)
    {
        level = Level;
        setMaxhp(Attribute[level - 1, 1]);
        setHp(Attribute[level - 1, 1]);
        setAtk(Attribute[level - 1, 2]);
        setNeedexp(Attribute[level - 1, 3]);
        setExp(0);
        
        stime = 0.8f / level;
    }

    public int getMaxhp()
    {
        return maxhp;
    }
    public void setMaxhp(int Maxhp)
    {
        this.maxhp = Maxhp;
    }
    public int getNeedexp()
    {
        return needexp;
    }

    public void setNeedexp(int Needexp)
    {
        this.needexp = Needexp;
    }

    public int getCoin()
    {
        return coin;
    }
    public void setCoin(int Coin)
    {
        coin = Coin;
    }
    public int getCount()
    {
        return count;
    }
    public void setCount(int Count)
    {
        count = Count;
    }

    public int getFen()
    {
        return fen;
    }
    public void setFen(int Fen)
    {
        fen = Fen;
    }
}
