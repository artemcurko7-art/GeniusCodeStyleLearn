using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBullet : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeShooting;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bullet),
            actionOnGet: (bullet) => ActionOnGet(bullet),
            actionOnRelease: (bullet) => ActionOnRelease(bullet));
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.Collided += OnRelease;
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.ResetSettings();
        bullet.gameObject.SetActive(false);
    }

    private void OnRelease(Bullet bullet)
    {
        _pool.Release(bullet);
        bullet.Collided -= OnRelease;
    }

    private IEnumerator Shoot()
    {
        while (enabled)
        {
            var direction = (_target.position - transform.position).normalized;
            var bullet = _pool.Get();

            bullet.Rigidbody.linearVelocity = direction * _speed;

            yield return new WaitForSeconds(_timeShooting);
        }
    }
}
