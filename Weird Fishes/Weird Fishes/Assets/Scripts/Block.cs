using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;

    //cached references
    Level level;
    Rigidbody2D rigidBody2d;

    //state variables
    [SerializeField] int timesHit;  //only serialized for debug purposes

    private void Start() {
        CountBreakableBlocks();
        DropGravityBlocks();
    }

    private void DropGravityBlocks() {
        if(tag == "Gravity") {
            //rigidBody2d = GetComponent<Rigidbody2D>();
            //rigidBody2d.velocity = new Vector2(xPush, yPush);
        }
    }

    private void CountBreakableBlocks() {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable" || tag == "Gravity") {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Camera.main.transform.position to play the block breaking sound where the camera is located
        if (tag == "Breakable" || tag == "Gravity") {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            DestroyBlock();
        }

        else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }

        else {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
       
    }

    private void DestroyBlock() {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX() {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
