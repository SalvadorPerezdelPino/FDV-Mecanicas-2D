using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private float _direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction = gameObject.GetComponent<SpriteRenderer>().flipY ? -1.0f : 1.0f;
        transform.position += new Vector3(_direction * _speed * Time.deltaTime, 0, 0);
    }
}
