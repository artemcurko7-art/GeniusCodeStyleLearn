using UnityEngine;

public class ConsistentPath : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _speed;

    private Vector3 _position;
    private int _index;

    private void Start()
    {
        _position = GetPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _position)
            _position = GetPosition();

        transform.position = Vector3.MoveTowards(transform.position, _position, _speed * Time.deltaTime);
    }

    private Vector3 GetPosition()
    {
        _index++;

        if (_index == _targetPoint.childCount)
            _index = 0;

        var position = _targetPoint.GetChild(_index).transform.position;
        transform.forward = position - transform.position;

        return position;
    }
}
