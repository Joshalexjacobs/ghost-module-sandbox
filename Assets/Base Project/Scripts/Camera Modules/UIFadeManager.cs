using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeManager : MonoBehaviour  {
    
    // Multiple UI Images
    
    public IEnumerator FadeIn(params Image[] _images) {
        for (int i = 0; i <= 4; i++) {
            foreach (Image image in _images) {
                image.color = new Color(1f, 1f, 1f, 0.25f * i);    
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
  
    public IEnumerator FadeOut(params Image[] _images) {
        for (int i = 4; i >= 0; i--) {
            foreach (Image image in _images) {
                image.color = new Color(1f, 1f, 1f, 0.25f * i);
            }
            
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    // Single UI Image
    
    public IEnumerator FadeIn(Image _image) {
        for (int i = 0; i <= 4; i++) {
            _image.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
  
    public IEnumerator FadeOut(Image _image) {
        for (int i = 4; i >= 0; i--) {
            _image.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    // MeshRenderer
    
    public IEnumerator FadeIn(MeshRenderer _meshRenderer) {
        for (int i = 0; i <= 4; i++) {
            _meshRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
  
    public IEnumerator FadeOut(MeshRenderer _meshRenderer) {
        for (int i = 4; i >= 0; i--) {
            _meshRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    // SpriteRenderer
    
    public IEnumerator FadeIn(SpriteRenderer _spriteRenderer) {
        for (int i = 0; i <= 4; i++) {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
  
    public IEnumerator FadeOut(SpriteRenderer _spriteRenderer) {
        for (int i = 4; i >= 0; i--) {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public IEnumerator FadeOut(SpriteRenderer _spriteRenderer, float delay) {
        for (int i = 4; i >= 0; i--) {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(delay);
        }
    }
    
    public IEnumerator FadeInMaterial(SpriteRenderer _spriteRenderer) {
        for (int i = 0; i <= 4; i++) {
            _spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public IEnumerator FadeOutMaterial(SpriteRenderer _spriteRenderer) {
        for (int i = 4; i >= 0; i--) {
            _spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
