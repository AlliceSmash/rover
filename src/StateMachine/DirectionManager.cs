using System.Collections.Generic;
using common.interfaces;
using common.models;

namespace StateMachine
{
    public class DirectionManager : IDirectionState
    {
        private LinkedList<Direction> InternalStates;
        private LinkedListNode<Direction> _currentDirection;

        public DirectionManager()
        {
            var listOfStates = new List<Direction> { Direction.North, Direction.East, Direction.South, Direction.West };
            //next is turning right, previous is turning left
            InternalStates = new LinkedList<Direction>(listOfStates);
        }

        public Direction GetNextDirection(Direction currentDirection, char command)
        {
            if (_currentDirection == null || _currentDirection.Value != currentDirection)
                _currentDirection = InternalStates.Find(currentDirection);
            var next = _currentDirection;
            if (command == 'L' || command == 'l')
            {
                next = next.Previous;
                if (next == null) next = InternalStates.Last;
            }
            else if (command == 'R' || command == 'r')
            {
                next = next.Next;
                if (next == null) next = InternalStates.First;
            }
            return next.Value;
        }
    }
}
