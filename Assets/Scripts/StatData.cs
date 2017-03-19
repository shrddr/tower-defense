namespace Assets.Scripts
{
    public class StatData<T>
    {
        public StatId StatId { get; set; }
        public T Value { get; set; }
    }

    public enum StatId
    {
        /// <summary>
        /// Movespeed in points. 
        /// </summary>
        MoveSpeed = 0,

        /// <summary>
        /// Change attack delay in %.
        /// </summary>
        AttackSpeed = 1,

        /// <summary>
        /// Base value of attack delay.
        /// </summary>
        BaseAttackDelay = 2,

        AttackRange = 3,

        /// <summary>
        /// Flat damage in points.
        /// </summary>
        Damage = 4,

        /// <summary>
        /// Mulitplicates base damage.
        /// </summary>
        DamagePercent = 5,

        ProjectilesPerAttack = 6,

        ProjectileChainAttackDelay = 7
    }
}
