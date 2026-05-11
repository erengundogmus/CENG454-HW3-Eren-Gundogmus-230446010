namespace CoreBreach.Interfaces
{
    public interface IDamageable
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsAlive { get; }

        void TakeDamage(float amount);
        void Heal(float amount);
    }
}
