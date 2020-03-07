using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Glaive : Enemy {
  
  public float startDelay;
  
  public SpriteRenderer glaiveTopSpriteRenderer;
  public SpriteRenderer glaiveExhaustSpriteRenderer;
  public SpriteRenderer glaiveFan1SpriteRenderer;
  public SpriteRenderer glaiveFan2SpriteRenderer;
  public SpriteRenderer glaiveShadowSpriteRenderer;

  public GameObject invulnerablePointsParent;
  
  public ParticleSystem backgroundParticles;
  
  public Transform torpedoSpawnLeft;
  public Transform torpedoSpawnRight;

  public List<UbhShotCtrl> phase1Shots;
  public float phase1ShotDelay;

  public float phase2StartingHealth;

  public GameObject glaiveChargeLeft;
  public GameObject glaiveChargeRight;

  public Transform glaiveChargeLeftPosition;
  public Transform glaiveChargeRightPosition;

  public float glaiveChargeDelay;

  public GameObject petalBulletsRight;
  public GameObject petalBulletsLeft;
  
  public GameObject lasersParent;

  public UbhShotCtrl tornadoShot;
  public UbhShotCtrl lockOnShotRight;
  public UbhShotCtrl lockOnShotLeft;

  public GameObject explosionPoints1Parent;
  public GameObject explosionPoints2Parent;
  
  public OneOffSprite explosion64x64;
  public GameObject bossExplosion;

  private int phase = 1;
  
  private FireTorpedos _fireTorpedos;
  private FollowAnimationCurve _followAnimationCurve;

  private UIFadeManager _uiFadeManager;

  private List<FireLaser> _lasers;

  private List<GameObject> currentGlaives = new List<GameObject>();
  
  public override void OnInit() {
    base.OnInit();

    _fireTorpedos = GetComponent<FireTorpedos>();
    _followAnimationCurve = GetComponent<FollowAnimationCurve>();

    EnemySpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    glaiveTopSpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    glaiveExhaustSpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    glaiveFan1SpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    glaiveFan2SpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    glaiveShadowSpriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
    
    _uiFadeManager = FindObjectOfType<UIFadeManager>();

    StartCoroutine(Intro());
  }

  public override void OnBecameVisible() {
    // base.OnBecameVisible();
  }

  private void FixedUpdate() {
    if (!isDead) {
      if (health <= phase2StartingHealth && phase == 1) {
        foreach (UbhShotCtrl ubhShotCtrl in phase1Shots) {
          ubhShotCtrl.StopShotRoutineAndPlayingShot();
        }

        StartCoroutine(Phase2Intro());
        phase++;
      }
    }
  }

  private IEnumerator Intro() {
    yield return new WaitForSeconds(startDelay);

    StartCoroutine(_uiFadeManager.FadeInMaterial(EnemySpriteRenderer));
    StartCoroutine(_uiFadeManager.FadeInMaterial(glaiveTopSpriteRenderer));
    StartCoroutine(_uiFadeManager.FadeInMaterial(glaiveExhaustSpriteRenderer));
    StartCoroutine(_uiFadeManager.FadeInMaterial(glaiveFan1SpriteRenderer));
    StartCoroutine(_uiFadeManager.FadeInMaterial(glaiveFan2SpriteRenderer));
    yield return StartCoroutine(_uiFadeManager.FadeInMaterial(glaiveShadowSpriteRenderer));

    foreach (CircleCollider2D circle in invulnerablePointsParent.GetComponents<CircleCollider2D>()) {
      circle.enabled = true;
    }
    
    CircleCollider2D.enabled = true;
    
    backgroundParticles.Play();
    
    if (CircleCollider2D) CircleCollider2D.enabled = true;
    
    StartCoroutine(Phase1());
  }

  private IEnumerator Phase1() {
    if (health > phase2StartingHealth)
      yield return StartCoroutine(_fireTorpedos.SpawnMultipleTorpedosAtOnce(this, torpedoSpawnLeft, torpedoSpawnRight));

    foreach (UbhShotCtrl ubhShotCtrl in phase1Shots) {
      yield return new WaitForSeconds(phase1ShotDelay);
      if (health > phase2StartingHealth) ubhShotCtrl.StartShotRoutine();
      yield return new WaitUntil(() => { return !ubhShotCtrl.shooting; });
    }
    
    if (health > phase2StartingHealth) 
      StartCoroutine(Phase1());
  }

  private IEnumerator Phase2Intro() {
    Destroy(_followAnimationCurve);
    yield return DestroyTop();

    // display design on floor and correlate that with following attacks in phase 2

    StartCoroutine(Phase2());
  }
  
  private IEnumerator DestroyTop() {
    StartCoroutine(_uiFadeManager.FadeOutMaterial(glaiveTopSpriteRenderer));
    
    List<Transform> explosionPoints1 = explosionPoints1Parent.GetComponentsInChildren<Transform>().ToList();
    List<Transform> explosionPoints2 = explosionPoints2Parent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints1.RemoveAt(0);
    explosionPoints2.RemoveAt(0);
    
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      explosionPoints1.Select(transform => transform.position).ToList(), 0.05f));
    
    yield return StartCoroutine(EnemyExplosionManager.Explode(explosion2,
      explosionPoints2.Select(transform => transform.position).ToList(), 0.05f));
  }
  
  private IEnumerator Phase2() {
    yield return StartCoroutine(FireLasers());
    
    yield return StartCoroutine(GlaiveChargeAttack());
    
    yield return StartCoroutine(LockOnShotAttack());

    StartCoroutine(Phase2());
    StartCoroutine(Phase2());
  }

  private IEnumerator FireLasers() {
    if (!isDead)
      yield return MoveToCoroutine(new Vector3(0f, 1f, 0f), 1f);

    if (!isDead) yield return null;
    
    _lasers = lasersParent.GetComponentsInChildren<FireLaser>().ToList();
    
    _lasers.ForEach(laser => {
      StartCoroutine(laser.ChargeAndShootLaser(1f, 20f, 0.5f));  
    });      
    
    if (!isDead) yield return null;

    tornadoShot.StartShotRoutine();
    yield return new WaitUntil(() => { return !tornadoShot.shooting; });
    
    if (!isDead) yield return null;
    
    RotateTo(new Vector3(0f, 0f, 15f), 5f);
    yield return MoveToCoroutine(new Vector3(-2.5f, 1.5f, 0f), 5f);
    
    if (!isDead) yield return null;
    
    RotateTo(new Vector3(0f, 0f, 0f), 5f);
    yield return MoveToCoroutine(new Vector3(0f, 0.5f, 0f), 5f);
    
    if (!isDead) yield return null;
    
    RotateTo(new Vector3(0f, 0f, -15f), 5f);
    yield return MoveToCoroutine(new Vector3(2.5f, 1.5f, 0f), 5f);
    
    if (!isDead) yield return null;
    
    RotateTo(new Vector3(0f, 0f, 0f), 5f);
    yield return MoveToCoroutine(new Vector3(0f, 0.5f, 0f), 5f);
  }
  
  private IEnumerator SpawnGlaiveCharges() {
    if (!isDead) {
      currentGlaives.Add(Instantiate(glaiveChargeLeft, glaiveChargeLeftPosition.position, Quaternion.identity));
      
      yield return new WaitForSeconds(glaiveChargeDelay);
      
      GameObject glaiveChargeObj = Instantiate(glaiveChargeRight, glaiveChargeRightPosition.position, Quaternion.identity);
      currentGlaives.Add(glaiveChargeObj);
      
      yield return new WaitUntil(() => {
        return glaiveChargeObj == null;
      });
    }
  }

  private IEnumerator GlaiveChargeAttack() {
    if (!isDead) {
      Instantiate(petalBulletsRight, Vector3.zero, Quaternion.identity);
      
      RotateTo(new Vector3(0f, 0f, 15f), 1f);
      yield return MoveToCoroutine(new Vector3(-2.5f, 1.5f, 0f), 1f);
      
      if (!isDead) yield return null;
      
      yield return StartCoroutine(SpawnGlaiveCharges());

      if (!isDead) yield return null;
      
      Instantiate(petalBulletsLeft, Vector3.zero, Quaternion.identity);
      
      RotateTo(new Vector3(0f, 0f, -15f), 2f);
      yield return MoveToCoroutine(new Vector3(2.5f, 1.5f, 0f), 2f);
      
      if (!isDead) yield return null;
      
      yield return StartCoroutine(SpawnGlaiveCharges());
    }
  }

  private IEnumerator LockOnShotAttack() {
    if (!isDead) {
      RotateTo(new Vector3(0f, 0f, 0f), 5f);
      yield return MoveToCoroutine(new Vector3(0f, 1.5f, 0f), 5f);
      
      if (!isDead) yield return null;
      
      lockOnShotRight.StartShotRoutine();
      lockOnShotLeft.StartShotRoutine();

      yield return new WaitUntil(() => {
        return !lockOnShotRight.shooting && !lockOnShotLeft.shooting;
      });
    }
  }
  
  public override IEnumerator Flash() {
    StartCoroutine(base.Flash());
    
    yield return new WaitForSeconds(0.05f);
    
    glaiveTopSpriteRenderer.material.SetFloat("_FlashAmount", 1);

    yield return new WaitForSeconds(0.05f);
    
    glaiveTopSpriteRenderer.material.SetFloat("_FlashAmount", 0);
  }

  public override void Die() {
    DisableAllAttacks();
    base.Die();
  }

  private void DisableAllAttacks() {
    CandyCoded.Animate.StopAll(gameObject);
    
    lasersParent.SetActive(false);

    phase1Shots.ForEach(shot => {
      shot.StopShotRoutineAndPlayingShot();
    });
    
    tornadoShot.StopShotRoutineAndPlayingShot();
    lockOnShotRight.StopShotRoutineAndPlayingShot();
    lockOnShotLeft.StopShotRoutineAndPlayingShot();
    
    currentGlaives.ForEach(glaive => {
      if (glaive != null) {
        Destroy(glaive);
      }
    });
  }

  public override void Destroy() {
    // base.Destroy();
  }

  public override void Explode() {
    List<Transform> explosionPoints1 = explosionPoints1Parent.GetComponentsInChildren<Transform>().ToList();
    List<Transform> explosionPoints2 = explosionPoints2Parent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints1.RemoveAt(0);
    explosionPoints2.RemoveAt(0);

    StartCoroutine(SpawnExplosion1s(explosionPoints1));
    StartCoroutine(SpawnExplosion2s(explosionPoints2));
    StartCoroutine(BigExplosion(explosionPoints1));
  }
  
  private IEnumerator SpawnExplosion1s(List<Transform> transforms) {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    yield return new WaitForSeconds(0.2f);
    
    for (int i = 0; i < 2; i++) {
      StartCoroutine(EnemyExplosionManager.Explode(explosion,
        transforms.Select(transform => transform.position).ToList(), 0.1f));
      
      StartCoroutine(EnemyExplosionManager.Explode(explosion2,
        transforms.Select(transform => transform.position).ToList(), 0.11f));
      
      yield return new WaitForSeconds(0.5f);
    }
    
    yield return StartCoroutine(EnemyExplosionManager.Explode(explosion64x64, 1, transform.position, 0f));
  }
  private IEnumerator SpawnExplosion2s(List<Transform> transforms) {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    yield return new WaitForSeconds(0.2f);
    
    for (int i = 0; i < 4; i++) {
      StartCoroutine(EnemyExplosionManager.Explode(explosion2, 
        transforms.Select(transform => transform.position).ToList(), 0.1f));
      
      for (int j = 0; j < 5; j++)
        yield return StartCoroutine(Flash());
    }
  }
  
  private IEnumerator BigExplosion(List<Transform> transforms) {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    yield return new WaitForSeconds(0.2f);
    
    for (int i = 0; i < 5; i++) {
      StartCoroutine(EnemyExplosionManager.Explode(explosion,
        transforms.Select(transform => transform.position).ToList(), 0.1f));
      
      StartCoroutine(EnemyExplosionManager.Explode(explosion2,
        transforms.Select(transform => transform.position).ToList(), 0.11f));
      
      yield return new WaitForSeconds(1f);
    }

    Instantiate(bossExplosion, transform.position, Quaternion.identity);

    Destroy(gameObject);
  }
}
