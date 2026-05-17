namespace CoreBreach.Weapons
{
    public class BasicWeapon : IWeapon
    {
        public float Damage { get; }
        public float FireRate { get; }

        public BasicWeapon(float damage, float fireRate)
        {
            Damage =damage;
            FireRate =fireRate;
        }
    }
}
