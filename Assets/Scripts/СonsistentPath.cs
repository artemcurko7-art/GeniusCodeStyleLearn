using UnityEngine;

public class ÑonsistentPath : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] float _speed;

    private Vector3 _direction;
    private int _index;

    void Start()
    {
        _direction = GetDirection();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _direction)
            _direction = GetDirection();

        transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
    }

    private Vector3 GetDirection()
    {
        _index++;

        if (_index == _point.childCount)
            _index = 0;

        var direction = _point.GetChild(_index).transform.position;

        return direction;
    }
}
