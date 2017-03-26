using System.Collections.ObjectModel;
using System.Linq;

namespace Assets.Scripts
{
    public class StatDataContainer
    {
        public Collection<StatData> Stats { get; set; }

        public StatDataContainer()
        {
            Stats = new Collection<StatData>();
        }

        public float? GetStatValue(StatId statId)
        {
            var stat = Stats.SingleOrDefault(st => st.StatId == statId);

            return stat != null ? stat.Value : (float?)null;
        }
    }

    public class StatData
    {
        public StatId StatId { get; set; }

        public float Value { get; set; }
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
