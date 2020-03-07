using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeardropEye : Enemy {

  public override void Destroy() {
    EnemySpriteRenderer.enabled = false;
    shadow.SetActive(false);

    ParticleSystem particleSystem = GetComponent<ParticleSystem>();

    if (particleSystem) {
      particleSystem.Stop();
    }
  }
  
}
