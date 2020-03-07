using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
  
  public int lives = 3;
  
  public float movementSpeed;

  public List<Transform> shootPoints;

  public Animator shadowAnimator;

  public CandyCoded.GameObjectPoolReference PlayerBulletPoolReference;

  public float iFrames;
  
  [HideInInspector]
  public float currentIFrames;
  
  public OneOffSprite explosion1;
  public OneOffSprite explosion2;
  public OneOffSprite explosion64x64;

  public GameObject explosionPointsParent;
  
  private PlayerInputActions _inputActions;
  private ButtonControl[] _fireButtonControls = new ButtonControl[3];

  private float _fireTimerMin = 0.1f;
  private float _fireTimerCurrent = 0f;

  private float _ghostModuleCooldown = 0.5f;
  private float _ghostModuleCooldownCurrent = 0.5f;
  
  private Rigidbody2D _rigidbody2D;
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;
  private CircleCollider2D _circleCollider2D;
  private PlayerGhostModule _playerGhostModule;
  
  private PauseUtil _pauseUtil;
  private SloMoUtil _sloMoUtil;
  private AudioPool _audioPool;
  private ExplosionManager _explosionManager;
  // private LivesManager _livesManager;
  private ScreenShake _screenShake;
  
  private Vector2 _movementInput;
  private Vector2 _movementInputDigital;

  private bool _isDead = false;

  public bool IsDead {
    get => _isDead;
  }
  
  void Awake() {
    _inputActions = new PlayerInputActions();

    _inputActions.PlayerControls.Move.performed += ctx => { _movementInput = ctx.ReadValue<Vector2>(); };

    _inputActions.PlayerControls.MoveDigital.performed += ctx => { _movementInputDigital = ctx.ReadValue<Vector2>(); };

    _fireButtonControls = PlayerControllerUtil.InitializeFireButtonControls(_inputActions);

    PlayerBulletPoolReference.parentTransform = new GameObject("Player Bullet Pool").transform;
    PlayerBulletPoolReference.Populate();

    currentIFrames = iFrames;
    
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _circleCollider2D = GetComponent<CircleCollider2D>();
    _playerGhostModule = GetComponent<PlayerGhostModule>();
    
    _explosionManager = FindObjectOfType<ExplosionManager>();
    // _livesManager = FindObjectOfType<LivesManager>();
    _pauseUtil = FindObjectOfType<PauseUtil>();
    _sloMoUtil = FindObjectOfType<SloMoUtil>();
    _audioPool = FindObjectOfType<AudioPool>();
    _screenShake = FindObjectOfType<ScreenShake>();
    
    _inputActions.PlayerControls.ActivateGhostModule.performed += ctx => { ActivateGhostModule(); };
    
    // _livesManager.Init(lives);
  }

  private void ActivateGhostModule() {
    if (!_isDead && !_pauseUtil.IsPaused  && _ghostModuleCooldownCurrent >= _ghostModuleCooldown) {
      if (_playerGhostModule.moduleActive) {
        _playerGhostModule.ActivateOrDeactivateModule();
        _ghostModuleCooldownCurrent = 0f;        
      } else if (_playerGhostModule.moduleCurrent > 25f) {
        _playerGhostModule.ActivateOrDeactivateModule();
        _ghostModuleCooldownCurrent = 0f;
      }      
    }
  }
  
  private void FixedUpdate() {
    if (!_isDead) {
      Move();
      
      IncrementIFrames();
    }
  }

  private void Update() {
    if (!_isDead) {
      if (PlayerControllerUtil.IsPlayerFiring(_fireButtonControls) && _fireTimerCurrent > _fireTimerMin) {
        FireBullet();
      }

      if (_playerGhostModule.moduleActive) {
        UnscaledMove();
      }
      
      IncrementFireTimer();
      IncrementGhostModuleCooldown();
    }
  }

  private Vector2 HandleControllerMovement() {
    float x = _movementInput.x * movementSpeed;
    float y = _movementInput.y * movementSpeed;

    if (_movementInput.Equals(Vector2.zero) && !_movementInputDigital.Equals(Vector2.zero)) {
      x = _movementInputDigital.x * movementSpeed;
      y = _movementInputDigital.y * movementSpeed;
    }

    return new Vector2(x, y);
  }

  private void Move() {
    _rigidbody2D.velocity = HandleControllerMovement();

    LeanShip(_rigidbody2D.velocity.x);
    transform.position = BoundPlayerToViewPort();
  }

  private void UnscaledMove() {
    Vector2 vector2 = HandleControllerMovement();
    transform.position += new Vector3(vector2.x * Time.unscaledDeltaTime, vector2.y * Time.unscaledDeltaTime, transform.position.z);
    
    LeanShip(vector2.x);

    if (_playerGhostModule.currentBoundaries != null) {
      transform.position = BoundPlayerToGhostModuleViewPort(_playerGhostModule.currentBoundaries.min, _playerGhostModule.currentBoundaries.max);      
    } else {
      transform.position = BoundPlayerToViewPort(); 
    }
  }

  private Vector3 BoundPlayerToGhostModuleViewPort(float xMin, float xMax) {
    float min = 0.025f;
    float max = 0.975f;
    
    Vector3 viewPort = Camera.main.WorldToViewportPoint(transform.position);

    viewPort = new Vector3(Mathf.Clamp(viewPort.x, xMin, xMax),
      Mathf.Clamp(viewPort.y, min, max), 0f);

    viewPort = Camera.main.ViewportToWorldPoint(viewPort);

    return new Vector3(viewPort.x, viewPort.y, 0f);
  }
  
  
  private Vector3 BoundPlayerToViewPort() {
    float min = 0.025f;
    float max = 0.975f;
      
    Vector3 viewPort = Camera.main.WorldToViewportPoint(transform.position);

    viewPort = new Vector3(Mathf.Clamp(viewPort.x, min, max),
      Mathf.Clamp(viewPort.y, min, max), 0f);

    viewPort = Camera.main.ViewportToWorldPoint(viewPort);

    return new Vector3(viewPort.x, viewPort.y, 0f);
  }

  private void LeanShip(float x) {
    _animator.SetFloat("changeInX", x);
    shadowAnimator.SetFloat("changeInX", x);
  }

  public void FireBullet() {
    if (!_pauseUtil.IsPaused) {
      shootPoints.ForEach(point => { PlayerBulletPoolReference.Spawn(point.position, Quaternion.identity); });

      _audioPool.PlaySound("PlayerShoot");
      _fireTimerCurrent = 0f;

      // if (_playerGhostModule.moduleActive) { }
    }
  }

  private static readonly List<string> DAMAGEABLE_TAGS = new List<string> { "EnemyBullet" };

  private void OnTriggerEnter2D(Collider2D collision) {
    if (DAMAGEABLE_TAGS.Contains(collision.gameObject.tag)) {
      bool playerTookDamage = Damage(collision.transform.position);

      if (playerTookDamage) {
        Destroy(collision.gameObject);        
      }
    }
  }

  public bool Damage() {
    return Damage(transform.position);
  }
  
  public bool Damage(Vector3 contactPosition) {
    if (currentIFrames >= iFrames) {
      // _livesManager.UpdateLives(--lives);

      if (lives <= 0) {
        StartCoroutine(KillPlayer());
        return true;
      }

      if (lives == 1) {
        _audioPool.PlaySound("LowHealthAlert");
      }
      
      _screenShake.TriggerPriorityShake(0.25f, 0.1f);
      
      if (_sloMoUtil && !_sloMoUtil.IsSlowMoBeingUsed) {
        StartCoroutine(_sloMoUtil.SlowDownForXSeconds(0f, 0.01f));
      }

      currentIFrames = 0f;
      StartCoroutine(FlashPlayer(1f));
    
      if (explosion1) 
        StartCoroutine(_explosionManager.Explode(explosion1, contactPosition));
        
      if (explosion2) 
        StartCoroutine(_explosionManager.Explode(explosion2, contactPosition));

      return true;
    }

    return false;
  }

  private IEnumerator FlashPlayer(float length) {
    float currentFlashLength = 0f; 
    while (currentFlashLength < length) {
      _spriteRenderer.material.SetFloat("_FlashAmount", 1);
      yield return new WaitForSeconds(0.05f);
        
      _spriteRenderer.material.SetFloat("_FlashAmount", 0);
      yield return new WaitForSeconds(0.05f);

      currentFlashLength += 0.1f;
    }
  }

  private IEnumerator KillPlayer() {
    _isDead = true;
    _circleCollider2D.enabled = false;
    
    _rigidbody2D.isKinematic = true;
    _rigidbody2D.velocity = Vector2.zero;

    List<Transform> explosionPoints1 = explosionPointsParent.GetComponentsInChildren<Transform>().ToList();
    List<Transform> explosionPoints2 = explosionPointsParent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints1.RemoveAt(0);
    explosionPoints2.RemoveAt(0);

    explosionPoints2.Reverse();
    
    _screenShake.TriggerPriorityShake(0.3f, 0.25f);
    
    if (_sloMoUtil) _sloMoUtil.SlowDownTimeScale(0.2f);
    
    yield return new WaitForAll(SpawnExplosion1s(explosionPoints1), SpawnExplosion2s(explosionPoints2));

    StartCoroutine(_explosionManager.Explode(explosion64x64, 1, transform.position, 0f));
    
    transform.position = new Vector3(0f, -5f, 0f);
    
    yield return new WaitForSeconds(1f);
    
    if (_sloMoUtil) _sloMoUtil.Resume();

    RestartScene();
  }

  public void RestartScene() {
    SceneManager.LoadScene("SampleScene");    
  }

  private IEnumerator SpawnExplosion1s(List<Transform> transforms) {
    yield return StartCoroutine(_explosionManager.Explode(explosion1, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    StartCoroutine(_explosionManager.Explode(explosion1, 
      transforms.Select(transform => transform.position).ToList(), 0.1f));
  }
  
  private IEnumerator SpawnExplosion2s(List<Transform> transforms) {
    yield return StartCoroutine(_explosionManager.Explode(explosion2, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    StartCoroutine(_explosionManager.Explode(explosion2, 
      transforms.Select(transform => transform.position).ToList(), 0.1f));
  }
  
  private void IncrementFireTimer() {
    if (!_pauseUtil.IsPaused) {
      _fireTimerCurrent += Time.unscaledDeltaTime;
    }
  }
  
  private void IncrementIFrames() {
    if (currentIFrames < iFrames) {
      currentIFrames += Time.deltaTime;
    }
  }

  private void IncrementGhostModuleCooldown() {
    if (_ghostModuleCooldownCurrent < _ghostModuleCooldown) {
      _ghostModuleCooldownCurrent += Time.unscaledDeltaTime;
    }
  }

  private void OnEnable() {
    _inputActions.Enable();
  }

  private void OnDisable() {
    _inputActions.Disable();
  }

  public void PauseGame() {
    if (_pauseUtil && !_isDead && !_playerGhostModule.moduleActive) {
      _audioPool.PlaySound("Pause");
      _pauseUtil.PauseAndUnpauseGame();      
    }
  }
  
  public void InitializeFireButtonControls() {
    int buttonCount = _inputActions.PlayerControls.Shoot.controls.Count;
    
    _fireButtonControls = new ButtonControl[buttonCount];
    
    for (int i = 0; i < buttonCount; i++) {
      _fireButtonControls[i] = (ButtonControl) _inputActions.PlayerControls.Shoot.controls[i];      
    }
  }
  
}