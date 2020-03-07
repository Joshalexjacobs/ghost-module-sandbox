using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CandyCoded;
using UnityEngine;

public class Butterfly : Enemy {

  public float preAttackDelay = 0f;
  
  public GameObject torpedo;
  public int torpedosToSpawn;
  public float torpedoSpawnDelay;

  [SerializeField]
  [RangedSlider(-1, 1)]
  public RangedFloat torpedoRandomX;
  
  [SerializeField]
  [RangedSlider(-1, 1)]
  public RangedFloat torpedoRandomY;

  public UbhShotCtrl spiralShot;
  public UbhShotCtrl overTakeShotLeft;
  public UbhShotCtrl overTakeShotRight;

  public GameObject movement1;

  public List<ParticleSystem> particleSystems;
  public List<SpriteRenderer> renderers;
  
  public GameObject explosionPoints1Parent;
  public GameObject explosionPoints2Parent;
  
  public OneOffSprite explosion64x64;
  
  private FollowAnimationCurve _followAnimationCurve;

  private bool battlePhaseStarted = false;
  
  public override void OnInit() {
    _followAnimationCurve = GetComponent<FollowAnimationCurve>();
  }

  public override void OnBecameVisible() {
    if (!battlePhaseStarted) {
      StartCoroutine(BattlePhase());
      battlePhaseStarted = true;
    }
  }

  private IEnumerator BattlePhase() {
    yield return new WaitForSeconds(preAttackDelay);

    BoxCollider2D.enabled = true;

    while (!isDead) {
      if (!isDead) yield return StartCoroutine(SpawnTorpedos(overTakeShotRight, -1));
      
      if (!isDead) yield return StartCoroutine(FireSpiralShot());
      
      if (!isDead) yield return StartCoroutine(SpawnTorpedos(overTakeShotLeft));

      if (!isDead) yield return StartCoroutine(FireSpiralShot());
      
      if (!isDead) yield return StartCoroutine(HandleButterflyMovement(movement1));
    }
  }

  private IEnumerator FireSpiralShot() {
    spiralShot.StartShotRoutine();
    yield return new WaitUntil(() => {
      return !spiralShot.shooting;
    }); 
  }
  
  private IEnumerator SpawnTorpedos(UbhShotCtrl ubhShotCtrl, int direction = 1) {
    float originalY = transform.position.y;
    
    MoveTo(new Vector3(2 * direction, originalY - 0.2f, 0f), torpedoSpawnDelay * torpedosToSpawn);
    RotateTo(new Vector3(0f, 0f, -10f * direction), torpedoSpawnDelay * torpedosToSpawn);
    
    ubhShotCtrl.StartShotRoutine();
    
    for (int i = 0; i < torpedosToSpawn && !isDead; i++) {
      Instantiate(torpedo, transform.position + new Vector3(torpedoRandomX.Random(), torpedoRandomY.Random(), 0f), Quaternion.identity);
      
      yield return new WaitForSeconds(torpedoSpawnDelay);
    }

    if (isDead) yield return null;

    RotateTo(new Vector3(0f, 0f, 0f), 1.5f);
    yield return MoveToCoroutine(new Vector3(0f, originalY, 0f), 1.5f);
  }

  private static readonly string GROUND_ENEMY = "GroundEnemy";
  private static readonly string ENEMY = "Enemy";
  
  private IEnumerator HandleButterflyMovement(GameObject movement) {
    BoxCollider2D.enabled = false;
    
    FollowAnimationCurve movementAnimationCurve = Instantiate(movement, transform.position, Quaternion.identity).GetComponent<FollowAnimationCurve>();
    movementAnimationCurve.gameObjectToManipulate = this.gameObject;
    movementAnimationCurve.gameObject.transform.parent = this.gameObject.transform;
    
    renderers.ForEach(renderer => { renderer.sortingLayerName = GROUND_ENEMY; });
    particleSystems.ForEach(particleSystem => { particleSystem.GetComponent<Renderer>().sortingLayerName = GROUND_ENEMY; });
    
    yield return new WaitUntil(() => { return movementAnimationCurve.hasReachDestination; });
    
    Destroy(movementAnimationCurve);
    
    DisableParticles();
    transform.position = new Vector3(0f, 5f, 0f);
    
    renderers.ForEach(renderer => { renderer.sortingLayerName = ENEMY; });
    particleSystems.ForEach(particleSystem => { particleSystem.GetComponent<Renderer>().sortingLayerName = ENEMY; });

    yield return new WaitForSeconds(5f);
    
    yield return MoveToCoroutine(new Vector3(0f, 2f, 0f), 1f);

    EnableParticles();
    BoxCollider2D.enabled = true;
  }

  private void DisableParticles() {
    particleSystems.ForEach(particleSystem => {
      particleSystem.Stop();      
    });
  }
  
  private void EnableParticles() {
    particleSystems.ForEach(particleSystem => {
      particleSystem.Play();      
    });
  }
  
  public override void Explode() {
    CandyCoded.Animate.StopAll(gameObject);
    
    List<Transform> explosionPoints1 = explosionPoints1Parent.GetComponentsInChildren<Transform>().ToList();
    List<Transform> explosionPoints2 = explosionPoints2Parent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints1.RemoveAt(0);
    explosionPoints2.RemoveAt(0);

    StartCoroutine(SpawnExplosion1s(explosionPoints1));
    StartCoroutine(SpawnExplosion2s(explosionPoints2));
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
    
    Destroy(gameObject);
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

  public override void Destroy() {
    spiralShot.StopShotRoutineAndPlayingShot();
    overTakeShotLeft.StopShotRoutineAndPlayingShot();
    overTakeShotRight.StopShotRoutineAndPlayingShot();
  }
}
