using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    private GameController gc;
    public List<GameObject> SnakePiece = new List<GameObject>();
    int TicTime,Score;
    public int TicTimeLimit;
    string direction;
    private Vector3 TempPos;
    public bool InGame;
    public float Dots;

	void Start () {
        InGame = true;
        gc = FindObjectOfType<GameController>();
        for (int i = 0; i < Dots; i++)
        {
            InstantiateSnake();
        }
    }

    void InstantiateSnake()
    {
        GameObject piece = (GameObject)Instantiate(Resources.Load("piece"),new Vector3(0,0,-1),Quaternion.identity);
        if (!SnakePiece.Contains(piece))
        {
            SnakePiece.Add(piece);

        }
    }
	
    void InstantiatePiece()
    {
        GameObject p = (GameObject)Instantiate(Resources.Load("Piece"), new Vector3(SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.x, 0, SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.z), Quaternion.identity);
        SnakePiece.Add(p);
    
    }

    void InputController()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != "Down")
        {
            direction = "Up";
            TicTime = TicTimeLimit;     
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && direction != "Up")
        {
            direction = "Down";
            TicTime = TicTimeLimit; 
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != "Right")
        {
            direction = "Left";
            TicTime = TicTimeLimit;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != "Left")
        {
            direction = "Right";
            TicTime = TicTimeLimit;
        }
    }

    private void UpdateSnake()
    {
        TicTime++;
        if(TicTime >= TicTimeLimit)
        {
            for(int i = SnakePiece.Count - 1 ; i > 0; i--){
                if(i > 0)
                {
                    SnakePiece[i].transform.position = SnakePiece[i - 1].transform.position;
                }
            }
            SnakePiece[0].transform.position = transform.position;
            switch (direction)
            {
                case "Up":
                    TempPos.z++;
                    break;

                case "Down":
                    TempPos.z--;
                    break;

                case "Left":
                    TempPos.x--;
                    break;

                case "Right":
                    TempPos.x++;
                    break;
            }
            TicTime = 0;
        }
        transform.position = TempPos;
    }

	private void Update () {
        if (transform.position == new Vector3(gc.Foodx, 0, gc.Foodz))
        {
            gc.DestroyFood();
            InstantiatePiece();
        }
        CheckSnake();
        if (InGame == false) return;
        InputController();
        UpdateSnake();
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SnakePiece.Count < 2)
            {
                GameObject p = (GameObject)Instantiate(Resources.Load("Piece"), new Vector3(SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.x, 0, SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.z), Quaternion.identity);
                SnakePiece.Add(p);
            }
            if (SnakePiece.Count >= 2)
            {
                GameObject p = (GameObject)Instantiate(Resources.Load("Piece"), new Vector3(SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.x, 0, SnakePiece[SnakePiece.Count - 1].GetComponent<Transform>().position.z), Quaternion.identity);
                p.gameObject.tag = "Piece";
                SnakePiece.Add(p);
            }
       

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Piece" || other.tag == "Barrier" )
        { 
            Debug.Log("lose");
            InGame = false;
        }
    }

    private void CheckSnake()
    {
        for(int i = 0; i< SnakePiece.Count - 1; i++)
        {
           
                if(transform.position == SnakePiece[i].transform.position)
                {
                    InGame = false;
                }
        }
    }
    
}
