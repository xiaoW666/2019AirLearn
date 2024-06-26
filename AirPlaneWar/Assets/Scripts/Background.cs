using UnityEngine;

public class Background : MonoBehaviour
{
    int i;
    bool boss;
    float r1, r2, etime, mtime,time;
    Material m;
    void OnEnable()
    {
        boss = false;
        etime = 2f;
        mtime = 0;
        time = 0;
        r1 = Random.Range(1f, 5f);
        r2 = Random.Range(3f, 10f);
        m = GetComponent<MeshRenderer>().material;
        Transform Dynamic = GameObject.Find("MainGame/Dynamic").transform;
        for (int i = 0; i < Dynamic.childCount; i++)
        {
            Destroy(Dynamic.GetChild(i).gameObject);
        }
        Instantiate(Resources.Load<GameObject>("Prefabs/PlayerShip" + i)).name = "Player";
    }

    void FixedUpdate()
    {   //Boss出现后 停止刷新小怪
        if (!boss)
        {
            //游戏30S后出现Boss
            if (time < 30)
            {
                etime += Time.deltaTime;
                mtime += Time.deltaTime;
                time += Time.deltaTime;
            }
            else
            {
                CreateBoss();
                boss = true;
            }
        }
        
        //随机时间生成敌人和陨石
        if (etime >= r1)
        {
            CreateEnemy();
            etime = 0;
        }
        if (mtime >= r2)
        {
            CreateMeteorite();
            mtime = 0;
        }

        m.SetTextureOffset("_MainTex", m.GetTextureOffset("_MainTex") + new Vector2(0, 0.003f));
    }
    //生成敌人
    void CreateEnemy()
    {
        Enemy e;
        GameObject enemy;
        //敌人生成坐标
        int[] x = { 0, 7, -7, 3, -3 };
        int tpye = Random.Range(1, 3);
        switch (Random.Range(1, 4) % 3)
        {
            //随机生成一个敌人
            case 1:
                enemy = Resources.Load<GameObject>("Prefabs/EnemyShip" + Random.Range(1, 3));
                Instantiate(enemy, new Vector3(x[Random.Range(0, x.Length)], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(1);
                break;
            //随机生成两个敌人
            case 2:
                enemy = Resources.Load<GameObject>("Prefabs/EnemyShip" + Random.Range(1, 3));
                Instantiate(enemy, new Vector3(x[3], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(2);
                enemy = Resources.Load<GameObject>("Prefabs/EnemyShip" + Random.Range(1, 3));
                Instantiate(enemy, new Vector3(x[4], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(2);
                break;
            //随机生成三个敌人
            case 3:
                enemy = Resources.Load<GameObject>("Prefabs/EnemyShip" + Random.Range(1, 3));
                Instantiate(enemy, new Vector3(x[1], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(1);
                Instantiate(enemy, new Vector3(x[2], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(1);
                enemy = Resources.Load<GameObject>("Prefabs/EnemyShip" + Random.Range(1, 3));
                Instantiate(enemy, new Vector3(x[0], 1, 13), enemy.transform.rotation).GetComponent<Enemy>().setType(1);
                break;
        }
    }
    //生成陨石
    void CreateMeteorite()
    {
        GameObject meteorite = Resources.Load<GameObject>("Prefabs/Meteorite" + Random.Range(1, 3));
        //随机陨石大小
        float r = Random.Range(0.8f, 2f);
        meteorite.transform.localScale = new Vector3(r, r, r);
        //随机陨石位置
        Instantiate(meteorite, new Vector3(Random.Range(-6f, 6f), 1, 13), meteorite.transform.rotation);
    }
    //生成Boss
    void CreateBoss()
    {
        GameObject meteorite = Resources.Load<GameObject>("Prefabs/BossShip");
        Instantiate(meteorite, new Vector3(0, 1, 15), meteorite.transform.rotation);
    }

    public void SetPlayer(int I)
    {
        i = I;
    }
}
