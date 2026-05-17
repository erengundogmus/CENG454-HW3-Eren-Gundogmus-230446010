namespace CoreBreach.Weapons
{
    public class FireRateDecorator : WeaponDecorator
    {
        private readonly float fireRateMultiplier;

        public override float FireRate => wrappedWeapon.FireRate*fireRateMultiplier;

        public FireRateDecorator(IWeapon weapon, float multiplier) : base(weapon)
        {
            fireRateMultiplier =multiplier;
        }
    }
}
