using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDelay : MonoBehaviour {

  public float fadeOutAfter = 0f;
  public float fadeRate;
  public float waitTime = 0.1f;

  private SpriteRenderer _spriteRenderer;
  
  private void Awake() {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    StartCoroutine(FadeOutAfterDelay());
  }

  IEnumerator FadeOutAfterDelay() {
    yield return new WaitForSeconds(fadeOutAfter);
    
    for (int i = 10; i >= 0; i--) {
      _spriteRenderer.color = new Color(1f, 1f, 1f, fadeRate * i);
      _spriteRenderer.material.color = new Color(1f, 1f, 1f, fadeRate * i);
      yield return new WaitForSeconds(waitTime);
    }
  }

}
