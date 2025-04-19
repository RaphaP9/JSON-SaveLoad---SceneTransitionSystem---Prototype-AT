using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIInput : UIInput
{
    private PlayerInputActions playerInputActions;

    protected override void Awake()
    {
        base.Awake();
        InitializePlayerInputActions();
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.UI.Disable();
    }

    public override bool CanProcessInput()
    {
        if (ScenesManager.Instance.SceneState != ScenesManager.State.Idle) return false;
        return true;
    }

    public override bool GetPauseDown()
    {
        if (!CanProcessInput()) return false;
        if (InputOnCooldown()) return false;

        bool pauseInput = playerInputActions.UI.Pause.WasPerformedThisFrame();
        return pauseInput;
    }
}
