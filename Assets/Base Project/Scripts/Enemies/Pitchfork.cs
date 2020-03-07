using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pitchfork : Enemy  {
    
  public bool activateWhenSeen = false;
  public float activateDelay;

  private bool _hasBeenSeen = false;

  public FollowAnimationCurve shadowCurve;
  
  public GameObject explosionPoints1Parent;
  public UbhShotCtrl ubhShotCtrl;
  
  public override void OnBecameVisible() {
    if (activateWhenSeen && !_hasBeenSeen) {
      _hasBeenSeen = true;
      StartCoroutine(ActivateAnimationCurve());
    }
  }
  
  private IEnumerator ActivateAnimationCurve() {
    yield return new WaitForSeconds(activateDelay);
    base.OnBecameVisible();

    transform.parent = null;
    FollowAnimationCurve followAnimationCurve = GetComponent<FollowAnimationCurve>();
    
    if (followAnimationCurve) {
      followAnimationCurve.ResetInitializationTime();
      StartCoroutine(followAnimationCurve.Animate());
      StartCoroutine(followAnimationCurve.AnimateScaleCurve());
    }
    
    if (shadowCurve) StartCoroutine(shadowCurve.Animate());

    ubhShotCtrl.enabled = true;
  }

  public override void Explode() {
    List<Transform> explosionPoints1 = explosionPoints1Parent.GetComponentsInChildren<Transform>().ToList();

    explosionPoints1.RemoveAt(0);
    
    StartCoroutine(EnemyExplosionManager.Explode(explosion, 
      explosionPoints1.Select(transform => transform.position).ToList(), 0.1f));
  }
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);
    ubhShotCtrl.StopShotRoutineAndPlayingShot();

    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
