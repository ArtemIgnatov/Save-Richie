using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(Collider2D))]

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private Animator _chestAnimator;
    private Collider2D _chestCollider;

    private void Awake()
    {
        _chestAnimator = GetComponent<Animator>();
        _chestCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _chestAnimator.SetTrigger("Open");
            _chestCollider.enabled = false;

            if (_prefab != null) StartCoroutine(SpawnReward());
        }
    }

    private IEnumerator SpawnReward()
    {
        yield return new WaitForSeconds(1f);

        GameObject reward = Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}
