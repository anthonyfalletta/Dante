using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFactory{
    EnemyStateMachine _context;
   Dictionary<EnemyStates, EnemyBaseState> _states = new Dictionary<EnemyStates, EnemyBaseState>();

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
       _context = currentContext;
       _states[EnemyStates.movementZero] = new EnemyMovementZeroState(_context, this);
       _states[EnemyStates.movementOne] = new EnemyMovementOneState(_context, this);
       _states[EnemyStates.movementTwo] = new EnemyMovementTwoState(_context, this);
       _states[EnemyStates.attackZero] = new EnemyAttackZeroState(_context, this);
       _states[EnemyStates.attackOne] = new EnemyAttackOneState(_context, this);
       _states[EnemyStates.attackTwo] = new EnemyAttackTwoState(_context, this);
       _states[EnemyStates.actionZero] = new EnemyActionZeroState(_context, this);
       _states[EnemyStates.actionOne] = new EnemyActionOneState(_context, this);
       _states[EnemyStates.actionTwo] = new EnemyActionTwoState(_context, this);
   }

   public EnemyBaseState MovementZero()
   {
       return _states[EnemyStates.movementZero];
   }
   public EnemyBaseState MovementOne()
   {
       return _states[EnemyStates.movementOne];
   }
   public EnemyBaseState MovementTwo()
   {
       return _states[EnemyStates.movementTwo];
   }
   public EnemyBaseState AttackZero()
   {
       return _states[EnemyStates.attackZero];
   }
   public EnemyBaseState AttackOne()
   {
       return _states[EnemyStates.attackOne];
   }
   public EnemyBaseState AttackTwo()
   {
       return _states[EnemyStates.attackTwo];
   }
   public EnemyBaseState ActionZero()
   {
       return _states[EnemyStates.attackZero];
   }
   public EnemyBaseState ActionOne()
   {
       return _states[EnemyStates.attackOne];
   }
    public EnemyBaseState ActionTwo()
   {
       return _states[EnemyStates.attackTwo];
   }
}

enum EnemyStates{movementZero,movementOne,movementTwo,attackZero,attackOne,attackTwo, actionZero, actionOne, actionTwo}

