using System.Collections;
using System.Collections.Generic;
using CandyCoded;
using UnityEngine;

public class Torpedo : Enemy {

  public UbhShotCtrl ubhShotCtrl;
  public ParticleSystem particleSystem;

  public float colliderDelay = 1f;
  
  [SerializeField]
  [RangedSlider(-1, 1)]
  public RangedFloat torpedoRandomX;

  public override void OnInit() {
    FollowAnimationCurve followAnimationCurve = GetComponent<FollowAnimationCurve>();
    followAnimationCurve.animationCurve.x.EditKeyframeValue(1, transform.position.x + torpedoRandomX.Random());
  }

  public override void OnBecameVisible() {
    StartCoroutine(EnableCollider());
  }

  private IEnumerator EnableCollider() {
    yield return new WaitForSeconds(colliderDelay);

    BoxCollider2D.enabled = true;
  }
  
  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;

    if (ubhShotCtrl) ubhShotCtrl.StartShotRoutine();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
