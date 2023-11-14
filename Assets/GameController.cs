using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private SnakeController sc;
    public Transform Top, Bot, Left, Right;
    public float Foodx, Foodz, temp;
    private GameObject FoodObj;
    private int Scoree;
    public Text ScoreTxt,TimerTxt,GameOverTxt;
 

    private void Start () {
        GameOverTxt.GetComponent<Text>().enabled = false;
        Scoree = 0;
        InstantiateFood();
        sc = FindObjectOfType<SnakeController>();
	}


    private void InstantiateFood()
    {
        float x = Mathf.Round(Random.Range(Left.position.x + 1, Right.position.x - 1));
        float z = Mathf.Round(Random.Range(Bot.transform.position.z + 1, Top.transform.position.z - 1));
        Foodx = x;
        Foodz = z;
        FoodObj = (GameObject)Instantiate(Resources.Load("Food"), new Vector3(x, 0, z), Quaternion.identity);
    }

    public void DestroyFood()
    {
        if(sc.TicTimeLimit == 1)
        {
            sc.TicTimeLimit = 1;
        }
        if(Scoree % 2 == 0)
        {
            sc.TicTimeLimit -= 1;
        }
        Destroy(FoodObj);
        Scoree++;
        InstantiateFood();
     
    }
    
    void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            sc.InGame = false;
            TimerTxt.GetComponent<Animator>().enabled = true;
        }
        if (sc.InGame == false)
        {
            GameOverTxt.GetComponent<Text>().enabled = true;
            sc.TicTimeLimit = 1000000000;
            Time.timeScale = 0;
            ScoreTxt.GetComponent<Animator>().enabled = true;
            TimerTxt.GetComponent<Animator>().enabled = true;
          
        }
        ScoreTxt.text = "SCORE  "+ Scoree.ToString();
        TimerTxt.text = "TIME  "+ Mathf.Round( Time.time).ToString();
	}

    
}
