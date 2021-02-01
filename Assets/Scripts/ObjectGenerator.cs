using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    public ObjectPooler enemyPool;

    public float distanceBetweenCoins;

    public void SpawnCoins(Vector3 startPosition)
    {
        int numberOfCoins = Mathf.RoundToInt(Random.Range(0.51f, 3.49f));
        if(numberOfCoins == 1)
        {
            GameObject coin1 = coinPool.GetPooledObject();
            coin1.transform.position = startPosition;
            coin1.SetActive(true);
        }
        else
        {
            if(numberOfCoins == 2)
            {
                GameObject coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
                coin2.SetActive(true);

                GameObject coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
                coin3.SetActive(true);
            }
            else{
                if(numberOfCoins == 3)
                {
                    GameObject coin1 = coinPool.GetPooledObject();
                    coin1.transform.position = startPosition;
                    coin1.SetActive(true);

                    GameObject coin2 = coinPool.GetPooledObject();
                    coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
                    coin2.SetActive(true);

                    GameObject coin3 = coinPool.GetPooledObject();
                    coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
                    coin3.SetActive(true);
                }
            }
        }
        /*
        GameObject coin1 = coinPool.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y , startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);
        */
    }

    public void SpawnEnemy(Vector3 startPosition)
    {
        GameObject enemy = enemyPool.GetPooledObject();
        //enemy.transform.position = startPosition;
        enemy.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        enemy.SetActive(true);
    }
}
