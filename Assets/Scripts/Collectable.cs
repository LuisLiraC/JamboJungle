using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Coin
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.Coin;
    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    private bool hasBeenCollected;
    public int value = 1;

    GameObject player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Show()
    {
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }

    private void Hide()
    {
        sprite.enabled = false;
        itemCollider.enabled = false;
    }

    private void Collect()
    {
        Hide();
        hasBeenCollected = true;

        switch (this.type)
        {
            case CollectableType.Coin:
                GameManager.sharedInstance.CollectObject(this);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}
