//using System;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 bounceTarget;
    [SerializeField] private bool moving;

    [SerializeField] private Sprite[] playerSprites = new Sprite[2];
    [SerializeField] private SpriteRenderer playerSR;

    [Header("Sensors")]
    [SerializeField] private PlayerSensor upSensor;
    [SerializeField] private PlayerSensor downSensor;
    [SerializeField] private PlayerSensor leftSensor;
    [SerializeField] private PlayerSensor rightSensor;

    [SerializeField] private bool can_move;
    [SerializeField] private bool on_water;
    [SerializeField] private float log_movement = 0.0f;

    private void Awake()
    {
        can_move = true;
        bounceTarget = transform.position;
    }

    private void Update()
    {
        on_water = false;

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
                if (upSensor.sensedTileType != TileType.Wall)
                {
                    bounceTarget = upSensor.transform.position;
                    if (bounceTarget.y > 5.5f)
                    {
                        bounceTarget = new Vector2(transform.position.x, transform.position.y);
                    }
                    moving = true;
                }
                ResetRotation();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (downSensor.sensedTileType != TileType.Wall)
                {
                    bounceTarget = downSensor.transform.position;
                    if (bounceTarget.y < -5f)
                    {
                        bounceTarget = new Vector2(transform.position.x, transform.position.y);
                    }
                }
                moving = true;
                ResetRotation();
                transform.Rotate(0f, 0f, 180f);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (leftSensor.sensedTileType != TileType.Wall)
                {
                    bounceTarget = leftSensor.transform.position;
                    if (bounceTarget.x < -8.5f)
                    {
                        bounceTarget = new Vector2(-8.5f, transform.position.y);
                    }
                    moving = true;
                }
                ResetRotation();
                transform.Rotate(0f, 0f, 90f);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (rightSensor.sensedTileType != TileType.Wall)
                {
                    bounceTarget = rightSensor.transform.position;
                    if (bounceTarget.x > 8.5f)
                    {
                        bounceTarget = new Vector2(8.5f, transform.position.y);
                    }
                    moving = true;
                }
                ResetRotation();
                transform.Rotate(0f, 0f, -90f);
            }
        }
        else
        {
            playerSR.sprite = playerSprites[1];
        }
    }

    private void OnTriggerStay2D(Collider2D hit)
    {
        if (!moving && Scrollable.should_scroll)
        {
            if (hit.tag == "Tile")
            {
                float tileDist = Vector2.Distance(new Vector2(0f, bounceTarget.y), new Vector2(0f, hit.transform.position.y));
                if (tileDist < 0.5f && tileDist > 0f)
                {
                    bounceTarget = new Vector2(bounceTarget.x,hit.transform.position.y);
                }
                if(hit.GetComponent<Tile>().TypeOfTile == TileType.Water)
                {
                    on_water = true;
                }
            }
            if (hit.tag == "MovingObj")
            {
                switch (hit.GetComponent<MovingObject>().objectType)
                {
                    case SpawnObjectData.Car:
                        Debug.Log("Hit by car!");
                        StartCoroutine(Death());
                        break;
                    case SpawnObjectData.Log:
                        if(log_movement == 0f)
                        {
                            log_movement = hit.GetComponent<MovingObject>().moveSpeed* hit.GetComponent<MovingObject>().spawnDirMod;
                        }
                        break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //when making this script a child of scrollable, add OnActiveFixedUpdate()
        if (!moving)
        {
            if (log_movement != 0)
            {
                transform.position = new Vector2(transform.position.x + log_movement, transform.position.y);
            }
            bounceTarget = new Vector2(transform.position.x, transform.position.y);
        }
        else
        {
            if (can_move)
            {
                MovePlayer();
            }
        }
        //clamp bounceTarget to sides of screen
        if (bounceTarget.x > 8.5f)
        {
            bounceTarget = new Vector2(8.5f, transform.position.y);
        }
        if (bounceTarget.x < -8.5f)
        {
            bounceTarget = new Vector2(-8.5f, transform.position.y);
        }
        if(transform.position.y <= -6)
        {
            StartCoroutine(Death());
        }
        if (on_water)
        {
            if(log_movement == 0f)
            {
                StartCoroutine(Death());
            }
        }
        log_movement = 0f;
    }

    private void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, bounceTarget, 0.25f);
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private IEnumerator Death()
    {
        Debug.Log("Death Event!");
        can_move = false;
        Scrollable.should_scroll = false;
        for (int i = 0; i < 60*5; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        // return to Main Menu
        SceneManager.LoadScene("MainMenu");
    }
}
