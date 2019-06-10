namespace Combat
{
    public class CombatContext : Context
    {
        public override void ResetContext()
        {
            CurrentState = new CombatStart();
        }
    }
}