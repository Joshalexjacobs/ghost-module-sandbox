using UnityEngine;

public class LivesManager : MonoBehaviour {

  public SpriteRenderer energySpriteRenderer;
  
  private Animator _animator;

  private UIFadeManager _uiFadeManager;
  private SpriteRenderer _spriteRenderer;

  private void Awake() {
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _uiFadeManager = FindObjectOfType<UIFadeManager>();
  }

  public void Init(int lives) {
    _animator.SetInteger("lives", lives);
    StartCoroutine(_uiFadeManager.FadeIn(_spriteRenderer));
  }
  
  public void UpdateLives(int lives) {
    _animator.SetInteger("lives", lives);

    if (lives <= 0) {
      StartCoroutine(_uiFadeManager.FadeOut(_spriteRenderer));
      StartCoroutine(_uiFadeManager.FadeOut(energySpriteRenderer));
    }
  }
  
}
