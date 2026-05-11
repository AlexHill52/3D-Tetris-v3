using UnityEngine;
using UnityEngine.InputSystem;

public class DisableLocomotor : MonoBehaviour
{
    public InputActionReference move;

    void OnEnable()
    {
        move.action.Disable();
    }

}