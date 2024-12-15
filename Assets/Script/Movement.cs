using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private InputsGame move;
    public float Velocidade = 25;
    public GameObject personagem;
    [SerializeField]
    private bool isMove = true;
    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }


    public void Move()
    {
        IsMove = true;
    }

    public void NotMove()
    {
        IsMove = false;
    }

    private void Awake()
    {



    }
        





void FixedUpdate()
    {

 


        if (isMove)
        {



          
            transform.Translate(move.Movement().normalized * Velocidade * Time.deltaTime);
          
            if (move.Movement() != Vector3.zero)
            {
                GameController.Instance.Move = true;
                if (move.Movement().y > 0)
                {
                  

                 
                }

                if (move.Movement().y < 0)
                {
                 
                  
                }

                if (move.Movement().x > 0)
                {
                   
                
                }

                if (move.Movement().x < 0)
                {
               
                   
                }
            }
            else
            {
                GameController.Instance.Move = false;
            }
         
        }





    }
}
