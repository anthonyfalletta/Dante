public class ProjectileStateFactory
{
  ProjectileStateMachine _context;

   public ProjectileStateFactory(ProjectileStateMachine currentContext)
   {
       _context = currentContext;
   }

   public ProjectileBaseState Held()
   {
       return new ProjectileHeldState(_context, this);
   }
   public ProjectileBaseState Charge()
   {
       return new ProjectileChargeState(_context, this);
   }
   public ProjectileBaseState Target()
   {
       return new ProjectileTargetingState(_context, this);
   }
   public ProjectileBaseState Special()
   {
       return new ProjectileSpecialState(_context, this);
   }
   public ProjectileBaseState Free()
   {
       return new ProjectileFreeState(_context, this);
   }
}
