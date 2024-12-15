using UnityEngine;

public class InputsGame : MonoBehaviour
{

    private InputActions InpAct; // fazer o input



    public bool BlockKeyP = false;
    public bool BlockKeyF = false;
    public bool BlockKeyQ = false;
    public bool BlockKeyE = false;
    public bool BlockDir = false;
    private bool Block_ = false;
    private bool BlockM = false;



    public void BlockFalseEQF()
    {
        BlockKeyE = false;
        BlockKeyQ = false;
        BlockKeyF = false;
    }

    public void BlockDir_()
    {
        BlockDir = false;
        Block_ = false;
    }




    private void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked; // travar mouse
       // Cursor.visible = false; // deixar mouse invisivel
       try
        { 
        InpAct = new InputActions();
        InpAct.PlayerAction.Enable();
        }
        catch
        {
          
        }

    }




    public Vector3 Movement()
    {

        try
        {
            if (Block_ == false)
            {
                Vector3 moveInput = InpAct.PlayerAction.Movement.ReadValue<Vector2>();
                return moveInput;
            }
            return Vector3.zero;
        }
        catch
        {
            return Vector3.zero;
        }
    }





    public bool ButtonMenu()
    {
        try
      { 
        if (BlockM == false)
        {
            bool botao = InpAct.PlayerAction.Menu.triggered;
            return botao;
        }
        return false;
    }
        catch
        {
            return false;
        }
    }


    public bool ButtonInteract()
    {
        try
        { 
        if (BlockKeyE == false)

        {
            bool botao = InpAct.PlayerAction.Interact.triggered;
            return botao;

        }
        return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ButtonPower()
    {
        try
        {
            if (BlockKeyP == false)

            {
                bool botao = InpAct.PlayerAction.Power.triggered;
                return botao;

            }
            return false;
        }
        catch
        {
            return false;
        }
    }


    public bool ButtonW()
    {
        try 
        { 
        if (BlockDir == false)
            return InpAct.PlayerAction.Up.triggered;
        return false;
        }
        catch
        {
            return false;
        }
    }
    public bool ButtonS()
    {
        try
        {
            if (BlockDir == false)
                return InpAct.PlayerAction.Up.triggered;
            return false;
        }
        catch
        {
            return false;
        }
    }



     
}
