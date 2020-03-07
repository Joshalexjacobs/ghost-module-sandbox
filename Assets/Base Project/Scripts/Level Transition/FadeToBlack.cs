using System.Collections;
using UnityEngine;

public class FadeToBlack : MonoBehaviour {

  private MeshRenderer _meshRenderer;

  private void Awake() {
    _meshRenderer = GetComponent<MeshRenderer>();
  }

  public IEnumerator FadeIn() {
    for (int i = 0; i <= 4; i++) {
      _meshRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
      yield return new WaitForSeconds(0.2f);
    }
  }
  
  public IEnumerator FadeOut() {
    for (int i = 4; i >= 0; i--) {
      _meshRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
      yield return new WaitForSeconds(0.2f);
    }
  }

}
