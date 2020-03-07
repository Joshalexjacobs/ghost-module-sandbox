using System.Collections;
using UnityEngine;

public class Arrow : Enemy {

  public bool activateWhenSeen = false;
  public float activateDelay;

  private bool _hasBeenSeen = false;
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);

    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }

  public override void OnBecameVisible() {
    if (activateWhenSeen && !_hasBeenSeen) {
      base.OnBecameVisible();
      _hasBeenSeen = true;
      StartCoroutine(ActivateAnimationCurve());
    }
  }

  private IEnumerator ActivateAnimationCurve() {
    yield return new WaitForSeconds(activateDelay);
    
    FollowAnimationCurve followAnimationCurve = GetComponent<FollowAnimationCurve>();
    
    if (followAnimationCurve) {
      followAnimationCurve.ResetInitializationTime();
      StartCoroutine(followAnimationCurve.Animate());
      StartCoroutine(followAnimationCurve.AnimateScaleCurve());
    }
  }
}
