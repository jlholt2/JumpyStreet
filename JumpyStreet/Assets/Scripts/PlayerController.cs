using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Scrollable
{
    [SerializeField] private Vector2 bounceTarget;
    [SerializeField] private bool moving;

    [SerializeField] private Sprite[] playerSprites = new Sprite[2];
    [SerializeField] private SpriteRenderer playerSR;

    private void Awake()
    {
        bounceTarget = transform.position;
    }

    private void Update()
    {
        if (transform.position.x == bounceTarget.x && transform.position.y == bounceTarget.y)
        {
            moving = false;
        }
        if (!moving)
        {
            playerSR.sprite = playerSprites[0];
            transform.position = bounceTarget;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bounceTarget = new Vector2(transform.position.x, transform.position.y + 1);
                if (bounceTarget.y > 4.5)
                {
                    bounceTarget = new Vector2(transform.position.x, transform.position.y);
                }
                ResetRotation();
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bounceTarget = new Vector2(transform.position.x, transform.position.y - 1);
                if (bounceTarget.y < -4.5)
                {
                    bounceTarget = new Vector2(transform.position.x, transform.position.y);
                }
                ResetRotation();
                transform.Rotate(0f, 0f, 180f);
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bounceTarget = new Vector2(transform.position.x - 1, transform.position.y);
                if(bounceTarget.x < -8)
                {
                    bounceTarget = new Vector2(-8, transform.position.y);
                }
                ResetRotation();
                transform.Rotate(0f, 0f, 90f);
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                bounceTarget = new Vector2(transform.position.x + 1, transform.position.y);
                if (bounceTarget.x > 8)
                {
                    bounceTarget = new Vector2(8, transform.position.y);
                }
                ResetRotation();
                transform.Rotate(0f, 0f, -90f);
                moving = true;
            }
        }
        else
        {
            playerSR.sprite = playerSprites[1];
        }
    }

    private void OnTriggerStay2D(Collider2D hit)
    {
        //Debug.Log("Colliding");
        if (!moving)
        {
            if (hit.tag == "Tile")
            {
                float tileDist = Vector2.Distance(new Vector2(0f, bounceTarget.y), new Vector2(0f, hit.transform.position.y));
                if (tileDist < 0.5f && tileDist > 0f)
                {
                    bounceTarget = new Vector2(bounceTarget.x,hit.transform.position.y);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //when making this script a child of scrollable, add OnActiveFixedUpdate()
        OnActiveFixedUpdate();
        bounceTarget = new Vector2(bounceTarget.x, MathUtils.Round((bounceTarget.y - moveSpeed), 3));
        if (moving)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, bounceTarget, 0.25f);
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
