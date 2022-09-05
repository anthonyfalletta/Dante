using System.Collections.Generic;

public class PlayerStateFactory 
{
   PlayerStateMachine _context;
   Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

   public PlayerStateFactory(PlayerStateMachine currentContext)
   {
       _context = currentContext;
       _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
       _states[PlayerStates.move] = new PlayerMoveState(_context, this);
       _states[PlayerStates.dash] = new PlayerDashState(_context, this);
       _states[PlayerStates.shoot] = new PlayerShootState(_context, this);
       _states[PlayerStates.attack] = new PlayerAttackState(_context, this);
       _states[PlayerStates.special] = new PlayerSpecialState(_context, this);
   }

   public PlayerBaseState Idle()
   {
       return _states[PlayerStates.idle];
   }
   public PlayerBaseState Move()
   {
       return _states[PlayerStates.move];
   }
   public PlayerBaseState Dash()
   {
       return _states[PlayerStates.dash];
   }
   public PlayerBaseState Shoot()
   {
       return _states[PlayerStates.shoot];
   }
   public PlayerBaseState Attack()
   {
       return _states[PlayerStates.attack];
   }
   public PlayerBaseState Special()
   {
       return _states[PlayerStates.special];
   }

}

enum PlayerStates{idle,move,dash,shoot,attack,special}