using UnityEngine;

public class DustructableObject : MonoBehaviour, IDamagable
{
    [SerializeField] int _health;
    public int health { get { return _health; } set { _health = value; } }
    [SerializeField] GameObject destroyedState;

    public void DoDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= 0)
        {
            OnKill();
        }
    }

    void OnKill()
    {
        Instantiate(destroyedState, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
