using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Transform attackTransform;
    public float attackRadius;
    public float attackDamage = 10.0f;
    public bool attackEnabled = false;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
    }
}
