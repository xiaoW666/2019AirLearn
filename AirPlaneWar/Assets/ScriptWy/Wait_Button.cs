using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait_Button : MonoBehaviour
{
    public GameObject MainGame,UI,Background;
    //查找精灵物件
    GameObject shoppingOn, temp,offUi, fallback,Cube,music,hp, GameOver;
    bool open;
    Vector3 p;
    int i;
    Player player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
    void Start()
    {
        open = false;
        //关开商店
        shoppingOn = this.transform.Find("Panel_button/Panel_bg").gameObject;
        GameOver = transform.Find("End_Data").gameObject;
        hp = this.transform.Find("Panel_Hp").gameObject;
        //选择角色
        i = 1;
        temp = GameObject.Find("Main/PlayerList/Plyer_mod/PlayerShip" + i);
        p = GameObject.Find("Main/PlayerList/Plyer_mod/PlayerShip" + i).transform.position;
        //开始游戏
        fallback = this.transform.Find("Fallback").gameObject;//显示回到菜单
        offUi = this.transform.Find("Panel_button").gameObject;//商店
        Cube = GameObject.Find("Main/PlayerList");//关闭不需要的gameObject
        //设置音乐
        music = this.transform.Find("music").gameObject;//显示音乐ui
    }

    private void FixedUpdate()
    {
        temp.transform.Rotate(Vector3.forward *2f);
    }
    //---------------------------------------------------
    //商店开关
    public void ShoppingOn()
    {
        shoppingOn.SetActive(!shoppingOn.activeSelf);//开
        Time.timeScale = 0;
        //Off.SetActive(false);//关
    }
    public void ShoppingOff()
    {
        shoppingOn.SetActive(false);//关
        Time.timeScale = 1;
        //Off.SetActive(true);//开
    }
    //-----------------------------------------------------------------------------
    //换角色
    public void Left_Button()
    {
        i--;
        if (i < 1)
        {
            i = 3;
        }
        show();
    }
    public void Right_Button()
    {
        i++;
        if (i > 3)
        {
            i = 1;
        }
        show();
    }
    void show()//选角色
    {
        GameObject player = GameObject.Find("Main/PlayerList/Plyer_mod/PlayerShip" + i);
        temp.transform.position = player.transform.position;
        player.transform.position = p;
        temp = player;
    }
    //------------------------------------------------------------------
    public void GameStart()//开始游戏
    {
        //offUi.SetActive(false);//关闭不需要在游戏中现实的UI
        fallback.SetActive(true);//显示回到主菜单按钮
        Cube.SetActive(false);//关闭界面不需要的GameObject
        offUi.transform.GetChild(1).gameObject.SetActive(true); //商店显示
        offUi.transform.GetChild(2).gameObject.SetActive(false);
        Background.GetComponent<Background>().SetPlayer(i);
        MainGame.SetActive(true);
        hp.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetMainMenu()//回主菜单
    {
        //offUi.SetActive(true);//打开需要在游戏中现实的UI
        fallback.SetActive(false);//不显示回到主菜单按钮
        Cube.SetActive(true);//显示界面不需要的GameObject
        offUi.transform.GetChild(1).gameObject.SetActive(false);
        offUi.transform.GetChild(2).gameObject.SetActive(true);
        MainGame.SetActive(false);
        hp.SetActive(false);
        UI.SetActive(true);
        GameOver.SetActive(false);
        Time.timeScale = 1;
        ShoppingOff();
    }
    public void SetBullet1()//购买子弹
    {
        if (player == null)
        {
            player = GameObject.Find("MainGame/Dynamic/Player").GetComponent<Player>();
        }
        if (player.getCoin() >= 20)
        {
            player.setPotLevel(player.getPotLevel() + 1);
            player.setCoin(player.getCoin() - 20);
            shoppingOn.SetActive(false);
            Time.timeScale = 1;
        }

    }
    
    public void Setup()//设置
    {

        open = !open;//求反

        if (open == true)
        {
            music.SetActive(true);
        }
        else
        {
            music.SetActive(false);
        }
       
    }
    public void Music1()//音乐
    {
        music.transform.GetChild(1).gameObject.GetComponent<AudioSource>().enabled = true;
        music.transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled = false;
    }
    public void Music2()//音乐
    {
        music.transform.GetChild(1).gameObject.GetComponent<AudioSource>().enabled = false;
        music.transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled = true;
    }
    public void StopMusic()//关闭音乐
    {
        

        if (music.transform.GetChild(1).gameObject.GetComponent<AudioSource>().enabled ==true|| music.transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled ==true)
        {
            music.transform.GetChild(1).gameObject.GetComponent<AudioSource>().enabled = false;
            music.transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled = false;
            
        }
        //else
        //{
        //    OnoPen = !OnoPen;//求反

        //    if (OnoPen == true)
        //    {
        //        music.transform.GetChild(1).gameObject.GetComponent<AudioSource>().enabled = true;
        //    }
        //    else
        //    {
        //        music.transform.GetChild(2).gameObject.GetComponent<AudioSource>().enabled = true;
        //    }
        //}
       
    }
}
