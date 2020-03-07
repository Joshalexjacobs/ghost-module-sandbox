using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float bulletSpeed = 5f;
    public float damage = 1f;

    public GameObject playerBulletHit;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    private PlayerGhostModule _playerGhostModule;
    private AudioPool _audioPool;

    private float originalDamage = 1f;
    
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _playerGhostModule = FindObjectOfType<PlayerGhostModule>();
        _audioPool = FindObjectOfType<AudioPool>();

        originalDamage = damage;
    }

    private void OnEnable() {
        if (_rigidbody2D) {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0.0f, bulletSpeed));

            if (_playerGhostModule && _playerGhostModule.moduleActive) {
                _playerGhostModule.ShotFired();
            }
            
            _boxCollider2D.enabled = true;
        }
        
        if (_playerGhostModule.moduleActive) {
            damage *= 4;
        } else {
            damage = originalDamage;
        }
    }

    private void OnBecameInvisible() {
        _boxCollider2D.enabled = false;
        
        // release object back to bullet pool
        FindObjectOfType<Player>().PlayerBulletPoolReference.Destroy(gameObject);
    }

    private static readonly List<string> validTags = new List<string> {"Enemy"};
    
    public virtual void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.isTrigger) {
            if (validTags.Contains(collision.gameObject.tag)) {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy) {
                    enemy.Damage(damage);

                    if (enemy.canTakeDamage) {
                        _audioPool.PlaySound("EnemyHit");
                        _playerGhostModule.IncrementModule();
                    } else {
                        _audioPool.PlaySound("Hit (No Damage)");
                    }
                        
                }
            }
            
            Destroy();
        }
    }

    private void Destroy() {
        Instantiate(playerBulletHit, transform.position, Quaternion.identity);
        
        // release object back to bullet pool
        FindObjectOfType<Player>().PlayerBulletPoolReference.Destroy(gameObject);
    }
}
