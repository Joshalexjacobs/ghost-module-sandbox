using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnTrigger : MonoBehaviour {

  public float fadeOutDelay = 0f;

  public List<SpriteRenderer> spriteRenderers;

  public bool destroy = false;

  public void StartFadeOut() {
    StartCoroutine(FadeOut());
  }
  
  public IEnumerator FadeOut() {
    yield return new WaitForSeconds(fadeOutDelay);
    
    for (int i = 4; i >= 0; i--) {
      foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.25f * i);
      }
      
      yield return new WaitForSeconds(0.2f);
    }
    
    if (destroy) Destroy(gameObject);
  }

}
