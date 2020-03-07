using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateEye : Enemy {
  
  [Header("GateEye Specific Variables:")]
  public List<SpriteRenderer> objectsToFlash = new List<SpriteRenderer>();

  public UbhShotCtrl attack1;
  public UbhShotCtrl attack2;
  public UbhShotCtrl attack3;

  public GameObject door;
  public Animator shipEntranceAnimator;

  public GameObject groupToSpawn1;
  public GameObject groupToSpawn2;

  public GameObject explosionPoints1Parent;
  public GameObject explosionPoints2Parent;

  public GameObject bossExplosion;
  
  public List<SpriteRenderer> spritesToHide;
  public List<Animator> smallGateEyesAnimators;

  public FadeOutOnTrigger fadeOutOnTrigger;
  
  private bool hasOpenedDoorsOnce = false;
  
  private AudioPool _audioPool;

  public override void OnInit() {
    _audioPool = FindObjectOfType<AudioPool>();
    
    smallGateEyesAnimators.ForEach(smallEyeAnimator => {
      smallEyeAnimator.SetFloat("Offset", Random.Range(0.0f, 1.0f));
    });
  }

  public void StartAttackLoop() {
    canTakeDamage = true;
    StartCoroutine(AttackLoop());
  }

  private void Blink() {
    Animator.SetTrigger("Blink");
  }
  
  private IEnumerator AttackLoop() {
    if (!isDead) 
      attack1.StartShotRoutine();

    Blink();
    
    yield return new WaitUntil(() => {
      return !attack1.shooting;
    });

    Blink();
    
    for (int i = 0; i < 2; i++) {
      if (!isDead) 
        attack2.StartShotRoutine();
    
      yield return new WaitUntil(() => {
        return !attack2.shooting;
      });  
    }

    Blink();

    if (!isDead)
      attack3.StartShotRoutine();
    
    yield return new WaitUntil(() => {
      return !attack3.shooting;
    });

    Blink();
    
    if (!hasOpenedDoorsOnce && !isDead)
      yield return StartCoroutine(OpenDoorsAndSpawnEnemyGroup());

    Blink();
    
    StartCoroutine(AttackLoop());
  }

  private IEnumerator OpenDoorsAndSpawnEnemyGroup() {
    door.SetActive(true);
    shipEntranceAnimator.SetBool("isOpen", true);
    
    _audioPool.PlaySound("Open");
    
    spritesToHide.ForEach(spriteRenderer => { spriteRenderer.enabled = false; });
    BoxCollider2D.enabled = false;
    
    yield return new WaitForSeconds(3f);

    Instantiate(groupToSpawn1, Vector3.zero, Quaternion.identity);
    
    yield return new WaitForSeconds(1f);
    
    Instantiate(groupToSpawn2, Vector3.zero, Quaternion.identity);
    
    yield return new WaitForSeconds(6f);
    
    door.SetActive(false);
    shipEntranceAnimator.SetBool("isOpen", false);
    
    spritesToHide.ForEach(spriteRenderer => { spriteRenderer.enabled = true; });
    BoxCollider2D.enabled = true;

    hasOpenedDoorsOnce = true;
  }
  
  public override IEnumerator Flash() {
    StartCoroutine(base.Flash());
    
    yield return new WaitForSeconds(0.05f);
    
    objectsToFlash.ForEach(spriteRenderer => {
      spriteRenderer.material.SetFloat("_FlashAmount", 1);
    });
    
    yield return new WaitForSeconds(0.05f);
    
    objectsToFlash.ForEach(spriteRenderer => {
      spriteRenderer.material.SetFloat("_FlashAmount", 0);
    });
  }
  
  public override void Explode() {
    Animator.SetBool("isDead", true);
    List<Transform> explosionPoints1 = explosionPoints1Parent.GetComponentsInChildren<Transform>().ToList();
    List<Transform> explosionPoints2 = explosionPoints2Parent.GetComponentsInChildren<Transform>().ToList();
    
    explosionPoints1.RemoveAt(0);
    explosionPoints2.RemoveAt(0);
    
    StartCoroutine(BigExplosion(explosionPoints1));
    StartCoroutine(ExplodeFromGateEyes(explosionPoints2));
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

    Instantiate(bossExplosion, Vector3.zero, Quaternion.identity);
    shipEntranceAnimator.SetBool("isDead", true);

    fadeOutOnTrigger.StartFadeOut();

    Destroy(gameObject);
  }

  private IEnumerator ExplodeFromGateEyes(List<Transform> transforms) {
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      transforms.Select(transform => transform.position).ToList(), 0f));
    
    yield return new WaitForSeconds(0.2f);
    
    for (int i = 0; i < 8; i++) {
      StartCoroutine(EnemyExplosionManager.Explode(explosion2, 
        transforms.Select(transform => transform.position).ToList(), 0.1f));
      
      for (int j = 0; j < 5; j++)
        yield return StartCoroutine(Flash());
    }
  }
  
  public override void Destroy() {
    attack1.StopShotRoutineAndPlayingShot();
    attack2.StopShotRoutineAndPlayingShot();
    attack3.StopShotRoutineAndPlayingShot();
  }
}
