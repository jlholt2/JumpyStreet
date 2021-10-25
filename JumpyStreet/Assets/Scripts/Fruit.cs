using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType { Banana = 0, Cherry = 1, Orange = 2}
public class Fruit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fruitSR;
    [SerializeField] private Sprite[] fruitSprites;
    public FruitType fruitType = FruitType.Banana;

    public void DetermineFruitType()
    {
        int randVal = Random.Range(0, 7);
        if(randVal >= 6)
        {
            fruitSR.sprite = fruitSprites[2];
            fruitType = FruitType.Orange;
        }else if(randVal >= 4)
        {
            fruitSR.sprite = fruitSprites[1];
            fruitType = FruitType.Cherry;
        }
        else
        {
            fruitSR.sprite = fruitSprites[0];
            fruitType = FruitType.Banana;
        }
        //switch (Random.Range(0, 3))
        //{
        //    case 1:
        //        fruitSR.sprite = fruitSprites[1];
        //        fruitType = FruitType.Cherry;
        //        break;
        //    case 2:
        //        fruitSR.sprite = fruitSprites[2];
        //        fruitType = FruitType.Orange;
        //        break;
        //    default:
        //        fruitSR.sprite = fruitSprites[0];
        //        fruitType = FruitType.Banana;
        //        break;
        //}
    }
}
