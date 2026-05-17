namespace CoreBreach.Weapons
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon wrappedWeapon;

        public virtual float Damage => wrappedWeapon.Damage;
        public virtual float FireRate => wrappedWeapon.FireRate;

        protected WeaponDecorator(IWeapon weapon)
        {
            wrappedWeapon =weapon;
        }
    }
}
