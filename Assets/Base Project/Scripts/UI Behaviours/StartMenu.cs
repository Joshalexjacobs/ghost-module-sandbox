using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

  public Image[] imagesToFadeOut;

  public GameObject levelController;

  public GameObject player;
  
  public UnityEvent eventsOnPress;
  public UnityEvent events;

  public float loadDelay = 0f; // currently there is a bug where the audio is taking a long time to load

  private bool _hasLoaded = false;
  
  private PlayerInputActions _inputActions;
  private ButtonControl[] _fireButtonControls;

  private bool hasPressedFire = false;
  
  private void Awake() {
    _inputActions = new PlayerInputActions();
    
    _fireButtonControls = PlayerControllerUtil.InitializeFireButtonControls(_inputActions);
  }

  private IEnumerator Start() {
    yield return new WaitForSeconds(loadDelay);
    _hasLoaded = true;
  }

  private void Update() {
    if (_hasLoaded && PlayerControllerUtil.IsPlayerFiring(_fireButtonControls) && !hasPressedFire) {
      hasPressedFire = true;
      StartCoroutine(StartGame());
    }
  }

  private IEnumerator StartGame() {
    UIFadeManager _uiFadeManager = FindObjectOfType<UIFadeManager>();

    eventsOnPress.Invoke();
    
    yield return StartCoroutine(_uiFadeManager.FadeOut(imagesToFadeOut));

    StartLevel();
    
    events.Invoke();
    
    Destroy(gameObject);
  }

  private void StartLevel() {
    Instantiate(levelController, Vector3.zero, Quaternion.identity);
    Instantiate(player, new Vector3(0f, -5f, 0f), Quaternion.identity);
  }

}
