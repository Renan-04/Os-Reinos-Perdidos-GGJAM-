using UnityEngine;

public class InputsGame : MonoBehaviour
{

    private InputActions InpAct; // fazer o input




    private bool BlockHab = false;
    private bool BlockDir = false;
    private bool BlockMove = false;
    private bool BlockAction = false;
    private bool BlockMenu = false;





    public void LiberarMove()
    {
        BlockDir = false;
        BlockMove = false;
    }

    public void BloquearMove()
    {
        BlockDir = true;
        BlockMove = true;
    }

    public void LiberarHab()
    {
        BlockHab = false;
  
    }

    public void BloquearHab()
    {
        BlockHab = true;
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
            if (BlockMove == false)
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
        if (BlockMenu == false)
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


    public bool ButtonTransform()
    {
        try
        { 
        if (BlockHab == false)

        {
            bool botao = InpAct.PlayerAction.Hab1.IsPressed();
                return botao;

        }
        return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ButtonVoices()
    {
        try
        {
            if (BlockHab == false)

            {
                bool botao = InpAct.PlayerAction.Hab2.IsPressed();
                return botao;

            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ButtonHypnotize()
    {
        try
        {
            if (BlockHab == false)
            {
                bool botao = InpAct.PlayerAction.Hab3.IsPressed();
                return botao;

            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ButtonAction()
    {
        try
        {
            if (BlockAction == false)

            {
                bool botao = InpAct.PlayerAction.Action.triggered;
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
