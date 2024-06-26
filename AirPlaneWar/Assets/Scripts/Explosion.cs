using UnityEngine;

public class Explosion : MonoBehaviour
{
    float DTime;
    GameObject GameOver;
    private void Start()
    {
        transform.parent = GameObject.Find("MainGame/Dynamic").transform;
    }
    void FixedUpdate()
    {
        DTime += Time.deltaTime;
        if (DTime >= 1f)
        {
            Destroy(gameObject);
            if (GameObject.Find("MainGame/Dynamic/Player").activeSelf == false)
            {
                Time.timeScale = 0f;
                if (GameOver == null)
                {
                    GameOver = GameObject.Find("UIRoot").transform.Find("End_Data").gameObject;
                }
                GameOver.SetActive(true);
                GameOver.transform.Find("SL").gameObject.SetActive(false);
                
            }
            if (transform.name == "explosion_boss(Clone)")
            {
                Time.timeScale = 0f;
                if (GameOver == null)
                {
                    GameOver = GameObject.Find("UIRoot").transform.Find("End_Data").gameObject;
                }
                GameOver.SetActive(true);
                GameOver.transform.Find("SB").gameObject.SetActive(false);
                
            }
        }
    }
}
