using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum WeaponType
    {
        Single, 
        TwoWay,
        ThreeWay,
        FiveWay
    }
    public static WeaponType[] allWeaponTypes()
    {
        WeaponType[] ts = { WeaponType.Single, WeaponType.TwoWay, WeaponType.ThreeWay, WeaponType.FiveWay };
        return ts;
    }
        


    float speed = 2;

    public Sprite[] powerUpSprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = RandomSprite();
    }
	void Update()
	{
        transform.Translate(Vector3.down * speed * Time.deltaTime);
	}
	Sprite RandomSprite()
    {
        int i = Random.Range(0, powerUpSprites.Length);
        return powerUpSprites[i];
    }
	void OnTriggerEnter2D(Collider2D other)
	{
        PlayerController player = other.GetComponent<PlayerController>();
        if (player){
            player.ReceivePowerUp();
            Destroy(gameObject);
        }
	}
}
