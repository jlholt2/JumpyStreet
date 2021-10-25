//using System;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private PlayerSensor centralSensor;

    [SerializeField] private bool can_move;
    [SerializeField] private bool on_water;
    [SerializeField] private bool moving_horiz;
    [SerializeField] private float log_movement = 0.0f;
    [SerializeField] private int movement_cooldown = 0;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private Text deathText;
    [SerializeField] private string deathMessage = "ded";

    private void Awake()
    {
        deathText.gameObject.SetActive(false);
        can_move = true;
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
            if(movement_cooldown > 0)
            {
                movement_cooldown--;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RecenterPlayerToTile();
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
                    PlayJumpSound();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    RecenterPlayerToTile();
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
                    PlayJumpSound();
                    transform.Rotate(0f, 0f, 180f);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    RecenterPlayerToTile();
                    if (leftSensor.sensedTileType != TileType.Wall)
                    {
                        bounceTarget = leftSensor.transform.position;
                        if (bounceTarget.x < -8.5f)
                        {
                            bounceTarget = new Vector2(-8.5f, transform.position.y);
                        }
                        moving = true;
                    }
                    //else
                    //{
                    //    RecenterPlayerToTile();
                    //}
                    ResetRotation();
                    PlayJumpSound();
                    transform.Rotate(0f, 0f, 90f);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    RecenterPlayerToTile();
                    if (rightSensor.sensedTileType != TileType.Wall)
                    {
                        bounceTarget = rightSensor.transform.position;
                        if (bounceTarget.x > 8.5f)
                        {
                            bounceTarget = new Vector2(8.5f, transform.position.y);
                        }
                        moving = true;
                    }
                    //else
                    //{
                    //    RecenterPlayerToTile();
                    //}
                    ResetRotation();
                    PlayJumpSound();
                    transform.Rotate(0f, 0f, -90f);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    moving_horiz = true;
                }
                else
                {
                    moving_horiz = false;
                }
            }
        }
        else
        {
            playerSR.sprite = playerSprites[1];
            movement_cooldown = 2;
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
                    if (moving_horiz)
                    {
                        bounceTarget = new Vector2(hit.transform.position.x, bounceTarget.y);
                        moving_horiz = false;
                    }
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
                        deathMessage = "SQUISH";
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
            if (hit.tag == "Fruit")
            {
                Fruit fruit = hit.GetComponent<Fruit>();
                int scoreGain = 0;
                switch (fruit.fruitType)
                {
                    case FruitType.Banana:
                        scoreGain = 2;
                        break;
                    case FruitType.Cherry:
                        scoreGain = 5;
                        break;
                    case FruitType.Orange:
                        scoreGain = 10;
                        break;
                    default:
                        Debug.Log("ERROR: Unrecognized Fruit Type! Did something go wrong?");
                        break;
                }
                StartCoroutine(TimerScore.instance.AddScoreCount(scoreGain));
                Destroy(hit.gameObject);
            }
        }
    }

    private void RecenterPlayerToTile()
    {
        if(log_movement != 0f)
        {
            bounceTarget = centralSensor.sensedTile.transform.position;
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
                UpdateBounceTargetAccordingToScroll();
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
            deathMessage = "TOO SLOW";
            StartCoroutine(Death());
        }
        if (on_water)
        {
            if(log_movement == 0f)
            {
                deathMessage = "SPLASH";
                StartCoroutine(Death());
            }
        }
        on_water = false;
        log_movement = 0f;
    }

    private void UpdateBounceTargetAccordingToScroll()
    {
        bounceTarget = new Vector2(bounceTarget.x, bounceTarget.y - Scrollable.scrollSpeed);
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
        RecenterPlayerToTile();
        Debug.Log("Death Event!");
        deathText.text = deathMessage;
        deathText.gameObject.SetActive(true);
        can_move = false;
        Scrollable.should_scroll = false;
        TimerScore.instance.SaveHighScore();
        PlayDeathSound();
        for (int i = 0; i < 60*5; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        // return to Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    private void PlayJumpSound()
    {
        jumpSound.Play();
    }

    private void PlayDeathSound()
    {
        deathSound.Play();
    }
}
