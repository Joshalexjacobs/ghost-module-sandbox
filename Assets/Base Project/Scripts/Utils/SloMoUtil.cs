using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloMoUtil : MonoBehaviour {

    private float _currentTimeScale = 1f;
    
    private bool _isSlowMoBeingUsed = false;
    
    public bool IsSlowMoBeingUsed {
        get => _isSlowMoBeingUsed;
    }

    public void SlowDownTimeScale(float slowDown) {
        _currentTimeScale = slowDown;
        Time.timeScale = _currentTimeScale;
        
        _isSlowMoBeingUsed = true;
    }

    public void Resume() {
        _currentTimeScale = 1f;
        Time.timeScale = 1f;

        _isSlowMoBeingUsed = false;
    }

    public IEnumerator PauseForAFrame() {
        SlowDownTimeScale(0f);
        yield return null;
        Resume();
    }

    public IEnumerator SlowDownForXSeconds(float slowDown, float seconds) {
        SlowDownTimeScale(slowDown);
        yield return new WaitForSecondsRealtime(slowDown);
        Resume();
    }
}
