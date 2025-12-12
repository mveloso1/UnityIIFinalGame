using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Transform attackTransform;
    public float attackRadius;
    public float attackDamage = 10.0f;
    public bool attackEnabled = false;

    public GameObject projectilePrefab;

    void Update()
    {
        if (attackEnabled)
        {
            Collider[] attackHits = Physics.OverlapSphere(attackTransform.position, attackRadius);

            foreach (var attackHit in attackHits)
            {
                if (attackHit.gameObject.CompareTag("Player"))
                {
                    attackHit.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                    attackEnabled = false;
                }
            }
        }
    }

    public void AttackOn()
        { attackEnabled = true; }
    public void AttackOff()
        { attackEnabled = false; }
    public void ShootFireball()
    {
        GameObject proj = Instantiate(projectilePrefab, transform.parent.position, transform.parent.rotation);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
}
