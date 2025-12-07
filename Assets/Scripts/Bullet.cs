using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    public event Action<Bullet> Collided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collided?.Invoke(this);
    }

    public void Initialize(Vector3 direction)
    {
        _rigidbody.linearVelocity = direction * _speed;
    }

    public void ResetSettings()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
