using System.Collections;
using UnityEngine;

public class Halberd : Enemy {

  public float chargeDelay = 0f;

  public float chargeTime = 0f;

  public UbhShotCtrl ubhShotCtrl;
  
  public override void OnInit() {
    base.OnInit();
    StartCoroutine(ChargeCanon());
  }

  private IEnumerator ChargeCanon() {
    yield return new WaitForSeconds(chargeDelay);

    if (!isDead) {
      Animator.SetBool("isCharging", true);

      yield return new WaitForSeconds(chargeTime);

      if (!isDead) ubhShotCtrl.StartShotRoutine();

      yield return new WaitUntil(() => { return !ubhShotCtrl.shooting || isDead; });
      
      if (!isDead) Animator.SetBool("isCharging", false);
    }
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
