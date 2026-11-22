using UnityEngine;

public class Lanzador : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] GameObject _bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player.OnShootEvent.AddListener(Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(_bullet);
        newBullet.transform.position = gameObject.transform.position;
        newBullet.GetComponent<SpriteRenderer>().flipY = gameObject.GetComponent<SpriteRenderer>().flipX;
        newBullet.SetActive(true);
        gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        //StartCoroutine(DisableAfterTime(3.0f));
        Destroy(newBullet, 5.0f);
    }
}
