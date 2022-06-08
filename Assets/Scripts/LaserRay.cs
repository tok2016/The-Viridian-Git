using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    public static LaserRay instance;

    public ParticleSystem ray;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ray = gameObject.GetComponent<ParticleSystem>();
        foreach (var enemy in LevelManager.lvlManager.enemies)
            ray.trigger.AddCollider(enemy.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
