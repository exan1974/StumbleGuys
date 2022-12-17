using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float explosionDelay = 5f;
    public GameObject explosionPrefab;
    public GameObject WoodBreakingPrefab;
    public float BlastRadius = 1f;
    public int BlastDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExplosionCoroutine()
    {
        yield return new WaitForSeconds(explosionDelay);

        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);
        
        foreach(Collider collider in colliders)
        {
            GameObject hitObject = collider.gameObject;
            if(hitObject.CompareTag("Platform"))
            {
            Life life = hitObject.GetComponent<Life>();
            if(life != null)
            {
                float distance = (hitObject.transform.position - transform.position).magnitude;
                float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                int damage = (int) Mathf.Ceil(damageRate * BlastDamage);
                life.health -= damage;

                if(life.health <= 0)
                {
                    Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);
                    Destroy(hitObject);
                }
            }
            }
        }

        Destroy(gameObject);
    }

}
