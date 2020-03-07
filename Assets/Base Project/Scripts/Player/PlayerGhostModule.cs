using System.Collections;
using UnityEngine;

public class PlayerGhostModule : MonoBehaviour {

  public float moduleMax = 100f;
  
  public float moduleCurrent = 0f;

  public float increaseRate = 1f;
  public float decreaseRate = 3f;
  public float shotDecreaseRate = 1f;

  public bool moduleActive = false;

  public PlayerBoundaries currentBoundaries;
  
  public GameObject ghostModuleActivated;
  public GameObject _ghostModuleActivatedBackground;
  
  private GameObject _ghostModuleActivatedObj;
  private GameObject _ghostModuleActivatedBackgroundObj;

  private Player _player;
  
  private UIFadeManager _uiFadeManager;
  private GhostModuleEnergy _ghostModuleEnergy;
  private AudioPool _audioPool;
  private SpriteRenderer _ghostModuleEnergySpriteRenderer;

  private void Awake() {
    _player = GetComponent<Player>();

    _uiFadeManager = FindObjectOfType<UIFadeManager>();
    _ghostModuleEnergy = FindObjectOfType<GhostModuleEnergy>();
    _audioPool = FindObjectOfType<AudioPool>();

    if (_ghostModuleEnergy) {
      _ghostModuleEnergySpriteRenderer = _ghostModuleEnergy.GetComponent<SpriteRenderer>();
    }
  }
  
  private void Update() {
    if (!_player.IsDead) {
      if (moduleActive) {
        DecrementModule();
      }

      IncreaseGhostModuleEnergyUI();
    }
  }

  private void IncreaseGhostModuleEnergyUI() {
    float width = moduleCurrent / moduleMax;
    
    if (moduleCurrent <= 0f) {
      width = 0f;
    }
    
    _ghostModuleEnergySpriteRenderer.size = new Vector2(width * 2, 0.5f);
  }
  
  public void IncrementModule() {
    if (moduleCurrent < moduleMax) {
      if (moduleCurrent < 25f && (moduleCurrent + increaseRate) >= 25f) {
        _audioPool.PlaySound("GhostModuleOnline");
      }
      
      moduleCurrent += increaseRate;

      if (moduleCurrent > moduleMax) {
        moduleCurrent = moduleMax;
      }
    }
  }

  public void ShotFired() {
    moduleCurrent -= shotDecreaseRate;

    if (moduleCurrent < 0) {
      moduleCurrent = 0f;
    }
  }
  
  private void DecrementModule() {
    if (moduleCurrent > 0) {
      moduleCurrent -= Time.unscaledDeltaTime * decreaseRate;
    }

    if (moduleCurrent <= 0) {
      StartCoroutine(DeactivateModule());
    }
  }
  
  public void ActivateOrDeactivateModule() {
    moduleActive = !moduleActive;

    if (moduleActive) ActivateModule();
    else StartCoroutine(DeactivateModule());
  }

  private void ActivateModule() {
    Time.timeScale = 0.01f;
    moduleCurrent -= 10f;
    _ghostModuleActivatedObj = Instantiate(ghostModuleActivated, Vector3.zero, Quaternion.identity);

    FindAndSetCurrentBoundaries();
    SpawnParallax();
  }

  private void FindAndSetCurrentBoundaries() {
    currentBoundaries = FindObjectOfType<PlayerBoundaries>();
  }

  private void SpawnParallax() {
    ParallaxWaveManager parallaxWaveManager = FindObjectOfType<ParallaxWaveManager>();

    if (parallaxWaveManager) {
      _ghostModuleActivatedBackgroundObj = Instantiate(_ghostModuleActivatedBackground, Vector3.zero, Quaternion.identity);
      _ghostModuleActivatedBackgroundObj.transform.parent = parallaxWaveManager.transform;
    }
  }
  
  private IEnumerator DeactivateModule() {
    moduleActive = false;
    
    if (_ghostModuleActivatedObj) {
      SpriteRenderer ghostModuleSpriteRenderer = _ghostModuleActivatedObj.GetComponent<SpriteRenderer>();
      
      Time.timeScale = 0.5f;
      
      if (_ghostModuleActivatedBackgroundObj) Destroy(_ghostModuleActivatedBackgroundObj);
      
      yield return StartCoroutine(_uiFadeManager.FadeOut(ghostModuleSpriteRenderer, 0.05f));
    }
    
    Time.timeScale = 1f;
  }

}
