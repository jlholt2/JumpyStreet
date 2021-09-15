using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Scrollable
{
    [SerializeField] private Vector2 bounceTarget;
    [SerializeField] private bool moving;

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
            // Note: need to move bounceTarget y position to be exactly the y position of the tile the player is currently standing on

            transform.position = bounceTarget;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bounceTarget = new Vector2(transform.position.x, transform.position.y + 1);
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bounceTarget = new Vector2(transform.position.x, transform.position.y - 1);
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bounceTarget = new Vector2(transform.position.x - 1, transform.position.y);
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                bounceTarget = new Vector2(transform.position.x + 1, transform.position.y);
                moving = true;
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
}
