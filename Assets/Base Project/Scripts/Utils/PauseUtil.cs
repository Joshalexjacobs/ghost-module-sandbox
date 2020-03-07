using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUtil : MonoBehaviour {

    public Image pauseScreen;

    public List<GameObject> otherUIGroups;
    
    private bool _isPaused;

    public bool IsPaused {
        get => _isPaused;
        set => _isPaused = value;
    }

    public void PauseAndUnpauseGame() {
        _isPaused = !_isPaused;

        if (_isPaused) {
            Time.timeScale = 0f;
            pauseScreen.enabled = true;
        } else {
            Time.timeScale = 1f;
            pauseScreen.enabled = false;
            DisableOtherUIElements();
        }
    }

    private void DisableOtherUIElements() {
        otherUIGroups.ForEach(gameObj => { gameObj.SetActive(false); });
    }
}
