using UnityEngine;
using UnityEngine.UI;

public class TestGame : MonoBehaviour
{
    [SerializeField] private Button Button;

    private Health _health;

    private void Start()
    {
        SearchHealth();
        Button.onClick.AddListener(Damage);
    }

    private void SearchHealth()
    {
        EnemyMover enemy = FindAnyObjectByType<EnemyMover>();
        Health health = enemy.GetComponent<Health>();

        _health = health;
    }

    private void Damage()
    {
        _health.TakeDamage(1000);
        SearchHealth();
    }
}