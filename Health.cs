using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public GameObject gameObj;
    public float health = 100;
    bool isDead = false;
    public GameObject sb;
    public float lifeTime = 70;
    public bool isPlayer = false;

    // Use this for initialization
    void Start () {
        sb = GameObject.Find("SpawnBehavior");
        if (!isPlayer)
        {
            StartCoroutine(setLifeTime());
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking Damage");
        health -= damage;
        if (health <= 0 && !isDead && !isPlayer)
        {
            isDead = true;
            sb.GetComponent<SpawnBehavior>().decreaseSpawnCount();
            Destroy(gameObj, 7);
        }
        
    }

    public bool CheckHealth()
    {
        return isDead;
    }

    private IEnumerator setLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        sb.GetComponent<SpawnBehavior>().decreaseSpawnCount();
        Destroy(gameObj);

    }
}
