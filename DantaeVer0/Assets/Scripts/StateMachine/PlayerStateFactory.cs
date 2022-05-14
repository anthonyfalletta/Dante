
public class PlayerStateFactory 
{
   PlayerStateMachine _context;

   public PlayerStateFactory(PlayerStateMachine currentContext)
   {
       _context = currentContext;
   }

   public PlayerBaseState Idle()
   {
       return new PlayerIdleState(_context, this);
   }
   public PlayerBaseState Move()
   {
       return new PlayerMoveState(_context, this);
   }
   public PlayerBaseState Dash()
   {
       return new PlayerDashState(_context, this);
   }
   public PlayerBaseState Shoot()
   {
       return new PlayerShootState(_context, this);
   }
   public PlayerBaseState Attack()
   {
       return new PlayerAttackState(_context, this);
   }

}
