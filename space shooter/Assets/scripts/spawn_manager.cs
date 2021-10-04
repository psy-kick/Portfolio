using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemycontainer;
    private bool _stopspawn = false;
    [SerializeField]
    private GameObject[] _powerups;
    public void spawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);
        while(_stopspawn==false)
        {
            Vector3 pos = new Vector3(Random.Range(-8f,8f),7f,0);
            GameObject enemycon =  Instantiate(_enemyprefab, pos, Quaternion.identity);
            enemycon.transform.parent = _enemycontainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (_stopspawn==false)
        {
            Vector3 pos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            int randompower = Random.Range(0, 3);
            GameObject triplecon = Instantiate(_powerups[randompower], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
    public void onplayerdeath()
    {
        _stopspawn = true;
    }
}
