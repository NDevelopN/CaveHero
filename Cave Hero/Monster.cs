namespace Cave {
    public class Monster : Creature
    {
        protected Creature? Target; 

/**
 * Choose next creature to attack.
 * Overriding this function allows for varied target selection behaviour such as:
 *      - RandomEnemy       -   Target a random enemy to be the next target.
 *      - RandomCreature    -   Target any random creature, friend or foe.
 *      - RecentAttacker    -   Target last creature to have dealt damage (needs fallback).
 *      - Soft target       -   Target weakest foe.
 *      - Hard target       -   Target toughest foe.
 *      - Leader's target   -   Target the target of an ally.
 */
        public virtual void SelectTarget(List<Creature> allies, List<Creature> enemies) {
            Random rnd = new();
            Target = enemies[rnd.Next(0, enemies.Count)];
        }

        public Creature? GetTarget() {
            return Target;
        }
    }
}