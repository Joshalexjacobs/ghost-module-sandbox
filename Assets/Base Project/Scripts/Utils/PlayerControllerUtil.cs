using UnityEngine.InputSystem.Controls;

public static class PlayerControllerUtil {
    
  public static ButtonControl[] InitializeFireButtonControls(PlayerInputActions _inputActions) {
    int buttonCount = _inputActions.PlayerControls.Shoot.controls.Count;
    
    ButtonControl[] _fireButtonControls = new ButtonControl[buttonCount];
    
    for (int i = 0; i < buttonCount; i++) {
      _fireButtonControls[i] = (ButtonControl) _inputActions.PlayerControls.Shoot.controls[i];      
    }

    return _fireButtonControls;
  }
  
  public static bool IsPlayerFiring(ButtonControl[] _fireButtonControls) {
    foreach (ButtonControl fireButtonControl in _fireButtonControls) {
      if (fireButtonControl != null && fireButtonControl.isPressed)
        return true;
    }

    return false;
  }
  
}
