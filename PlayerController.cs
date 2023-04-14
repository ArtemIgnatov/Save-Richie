using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "inputeController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        return Input.GetButtonDown("Jump");
    }

    public override bool RetrieveJumpHoldInput(GameObject gameObject)
    {
        return Input.GetButton("Jump");
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveDashInput(GameObject gameObject)
    {
       return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public override bool RetrieveAttackInput(GameObject gameObject)
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}

