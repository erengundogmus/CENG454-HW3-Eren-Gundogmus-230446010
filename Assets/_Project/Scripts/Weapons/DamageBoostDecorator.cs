namespace CoreBreach.Weapons
{
    public class DamageBoostDecorator : WeaponDecorator
    {
        private readonly float damageBonus;

        public override float Damage => wrappedWeapon.Damage+damageBonus;

        public DamageBoostDecorator(IWeapon weapon, float bonus) : base(weapon)
        {
            damageBonus =bonus;
        }
    }
}
