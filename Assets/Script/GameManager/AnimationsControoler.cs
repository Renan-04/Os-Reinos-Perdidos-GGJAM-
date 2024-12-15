using UnityEngine;

public class AnimationsControoler : MonoBehaviour
{

    public static AnimationsControoler instance;   
    
    [SerializeField]
    Animator anim;



   
    public bool IsWalk = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            anim.SetBool("Walk", IsWalk);
    }
}
