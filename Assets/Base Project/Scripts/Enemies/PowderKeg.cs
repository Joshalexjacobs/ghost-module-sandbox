using System.Collections;
using UnityEngine;

public class PowderKeg : Enemy  {
    
  public UbhShotCtrl ubhShotCtrl;
  public Animator shadowAnimator;

  public bool droppedPowderKeg = false;

  private FollowAnimationCurve _followAnimationCurve;

  public override void OnInit() {
    base.OnInit();
    
    if (droppedPowderKeg) {
      _followAnimationCurve = GetComponent<FollowAnimationCurve>();
      if (_followAnimationCurve) _followAnimationCurve.spawnDelay = Random.Range(0f, 0.5f);
      
      StartCoroutine(SpawnPowderKeg());
    }
  }

  IEnumerator SpawnPowderKeg() {
    EnemySpriteRenderer.material.SetFloat("_FlashAmount", 1);
    
    yield return new WaitForSeconds(1f);

    CircleCollider2D.enabled = true;
    
    EnemySpriteRenderer.material.SetFloat("_FlashAmount", 0);
  }

  public override void OnBecameVisible() {
    if (!droppedPowderKeg)  
      base.OnBecameVisible();

    StartCoroutine(BeginSpawnAnimation());
  }

  private IEnumerator BeginSpawnAnimation() {
    yield return new WaitForSeconds(0.75f);
    this.Animator.SetBool("isVisible", true);
    shadowAnimator.SetBool("isVisible", true);
  } 
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);

    if (ubhShotCtrl) ubhShotCtrl.StartShotRoutine();
  }
  
}
