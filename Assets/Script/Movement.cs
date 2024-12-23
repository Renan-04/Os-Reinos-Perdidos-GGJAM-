using Fungus;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private InputsGame move;
    public float Velocidade = 25;
    public GameObject personagem;
    [SerializeField]
    private bool isMove = true;

    private Animator animator;

    private Flowchart fc;

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

    private void Start()
    {
        fc = GetComponent<Flowchart>();
        animator = GetComponent<Animator>();    
    }






    void FixedUpdate()
    {




        if (isMove)
        {
            animator.SetBool("Move", true);




            transform.Translate(move.Movement().normalized * Velocidade * Time.deltaTime);

            if (move.Movement() != Vector3.zero)
            {
                GameController.Instance.Move = true;
                fc.ExecuteBlock("Detransformation");

                if (move.Movement().y > 0)
                {
                
                       animator.SetFloat("MoveY", move.Movement().y);

                }

                if (move.Movement().y < 0)
                {

                    animator.SetFloat("MoveY", move.Movement().y);
                }

                if (move.Movement().x > 0)
                {
                    animator.SetFloat("MoveX", move.Movement().x);

                }

                if (move.Movement().x < 0)
                {
                    animator.SetFloat("MoveX", move.Movement().x);

                }
            }
            else
            {

                GameController.Instance.Move = false;
                animator.SetBool("Move", false);
            }

            Habilities();
        }


    }


    private void Habilities()
    {
        if (move.ButtonTransform())
        {
            fc.ExecuteBlock("Transformation");
            //Debug.Log("transformar");
        }
        if (move.ButtonVoices())  
        {
            fc.ExecuteBlock("Voices");

        }
        if (move.ButtonHypnotize())
        {
            fc.ExecuteBlock("Hypnosis");

        }
        
        
     
     
    }



}
