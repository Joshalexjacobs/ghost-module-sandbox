using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 1f;

    public OneOffSprite explosion;
    public OneOffSprite explosion2;

    public GameObject shadow;
    
    [HideInInspector]
    public bool isDead = false;

    public bool canTakeDamage = true;

    public bool diesOnContact = false;
    
    public bool isABoss = false;
    
    [HideInInspector]
    public bool hasCompletedDeathAnimation = false;
    
    private ExplosionManager _explosionManager;
    
    public ExplosionManager EnemyExplosionManager {
        get => _explosionManager;
    }
    
    private SpriteRenderer _spriteRenderer;
    
    public SpriteRenderer EnemySpriteRenderer {
        get => _spriteRenderer;
    }
    
    private Animator _animator;

    public Animator Animator {
        get => _animator;
    }
    
    private CircleCollider2D _circleCollider2D;

    public CircleCollider2D CircleCollider2D {
        get => _circleCollider2D;
    }

    private BoxCollider2D _boxCollider2D;

    public BoxCollider2D BoxCollider2D {
        get  => _boxCollider2D;
    }

    private void Awake() {
        _explosionManager = FindObjectOfType<ExplosionManager>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        OnInit();
    }

    public virtual void OnInit() {
        // does nothing, but is called at the end of Awake()
    }
    
    public virtual void OnBecameVisible() {
        if (_circleCollider2D) _circleCollider2D.enabled = true; 
        if (_boxCollider2D) _boxCollider2D.enabled = true;
    }

    public virtual void OnBecameInvisible() {
        if (_circleCollider2D) _circleCollider2D.enabled = false; 
        if (_boxCollider2D) _boxCollider2D.enabled = false;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Player player = collision.gameObject.GetComponent<Player>();
            bool playerTookDamage = player.Damage(collision.transform.position);

            if (playerTookDamage && !isDead && canTakeDamage && diesOnContact) {
                Damage(health);
            }
        }
    }
    
    public virtual void Damage (float damage) {
        if (!isDead && canTakeDamage) {
            health -= damage;
            StartCoroutine(Flash());
            
            if (health <= 0f) {
                isDead = true;
                Die();
            }
        }
    }
    
    public virtual Coroutine MoveToCoroutine(Vector3 positionToMoveTo, float duration) {
        return CandyCoded.Animate.MoveTo(gameObject, positionToMoveTo, duration, Space.World);
    }
    
    public virtual void MoveTo(Vector3 positionToMoveTo, float duration) {
        CandyCoded.Animate.MoveTo(gameObject, positionToMoveTo, duration, Space.World);
    }

    public virtual Coroutine RotateToCoroutine(Vector3 newRotation, float duration) {
        return CandyCoded.Animate.RotateTo(gameObject, newRotation, duration);
    }
    
    public virtual void RotateTo(Vector3 newRotation, float duration) {
        CandyCoded.Animate.RotateTo(gameObject, newRotation, duration);
    }
    
    public virtual IEnumerator FadeOut(float rate, float waitTime = 0.1f) {
        for (int i = 10; i >= 0; i--) {
            _spriteRenderer.color = new Color(1f, 1f, 1f, rate * i);
            _spriteRenderer.material.color = new Color(1f, 1f, 1f, rate * i);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public virtual IEnumerator Flicker(int flickerAmount = 5, float waitTime = 0.1f) {
        for (int i = 0; i < flickerAmount; i++) {
            _spriteRenderer.material.SetFloat("_FlashAmount", 1);
            
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
            
            yield return new WaitForSeconds(waitTime);
            
            _spriteRenderer.material.SetFloat("_FlashAmount", 0);
            
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            _spriteRenderer.material.color = new Color(1f, 1f, 1f, 0f);
            
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    public virtual IEnumerator Flash() {
        _spriteRenderer.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.05f);
        
        _spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }

    public virtual void Die() {
        if (_animator && _animator.GetBool("isDead")) _animator.SetBool("isDead", true);
        if (_circleCollider2D) _circleCollider2D.enabled = false; 
        if (_boxCollider2D) _boxCollider2D.enabled = false;

        Explode();
        Destroy();
    }

    public virtual void Explode() {
        if (explosion) 
            StartCoroutine(_explosionManager.Explode(explosion, transform.position));
        
        if (explosion2) 
            StartCoroutine(_explosionManager.Explode(explosion2, transform.position));
    }

    public virtual void Destroy() {
        Destroy(gameObject);
    }

    public virtual void OnDestroy() {
        
    }
}
