using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFactory{
    EnemyStateMachine _context;
   Dictionary<EnemyStates, EnemyBaseState> _states = new Dictionary<EnemyStates, EnemyBaseState>();

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
       _context = currentContext;
       _states[EnemyStates.wander] = new WandererWanderState(_context, this);
       _states[EnemyStates.follow] = new WandererFollowState(_context, this);
       _states[EnemyStates.lunge] = new WandererLungeState(_context, this);
   }

   public EnemyBaseState Wander()
   {
       return _states[EnemyStates.wander];
   }
   public EnemyBaseState Follow()
   {
       return _states[EnemyStates.follow];
   }
 
   public EnemyBaseState Lunge()
   {
       return _states[EnemyStates.lunge];
   }
  
}

enum EnemyStates{wander,follow,lunge}

