using System.Collections.Generic;
using System.Linq;
using common.interfaces;
using common.models;

namespace StateMachine
{
    public sealed class StateMachine : IStateMachine
    {
        private List<GridPosition> _occupiedGrids;
        private IDirectionState _directionManager;
        private GridPosition GridBoundary { get; set;}

        public StateMachine(IDirectionState directionManager)
        {
            _directionManager = directionManager;
            _occupiedGrids = new List<GridPosition>();
            GridBoundary = new GridPosition { XPos = int.MaxValue, YPos = int.MaxValue };
        }

        public GetStateResponse GetState(State initialState, string commandSequence, bool clearRovers =false)
        {
            if (IsPositionOutOfBounds(initialState.Position))
                return new GetStateResponse { StateResult = initialState, StateStatus = GetStateStatus.InvalidInitialState };

            if (clearRovers) _occupiedGrids.Clear();
            var commandCausedOutofBounds = false;
            var commandsUpperCase = commandSequence.ToUpperInvariant();
            var nextDirection = initialState.FaceDirection;
            var nextState = initialState;
            var nextPosition = initialState.Position;

            for (int i = 0; i < commandsUpperCase.Length; i++)
            {
                nextDirection = _directionManager.GetNextDirection(nextDirection, commandsUpperCase[i]);
                nextPosition = GetNextPosition(commandsUpperCase[i], nextState);

                if (!IsPositionOutOfBounds(nextPosition))
                {
                    nextState.FaceDirection = nextDirection;
                    nextState.Position = nextPosition;
                }
                else
                {
                    commandCausedOutofBounds = true;
                }              
            }

            _occupiedGrids.Add(nextPosition);

            return new GetStateResponse { StateResult = nextState,
                StateStatus = commandCausedOutofBounds ? GetStateStatus.CommandCouldCauseOffGrid : GetStateStatus.Success };
        }

        private bool IsPositionOutOfBounds(GridPosition gridPosition)
        {
            return gridPosition.XPos > GridBoundary.XPos || gridPosition.XPos < 0
                || gridPosition.YPos > GridBoundary.YPos || gridPosition.YPos < 0;
        }

        private GridPosition GetNextPosition(char command, State currentState)
        {
            var next = currentState.Position.Copy();

            if (command != 'M') return next;

            var direction = currentState.FaceDirection;
            
            return getNextGridNotOccupied(next, direction);
        }

        private GridPosition getNextGridNotOccupied(GridPosition currentPos, Direction direction )
        {
            var next = currentPos;
         
            switch (direction)
            {
                case Direction.North:
                    next.YPos = _occupiedGrids.Any(o => o.YPos == currentPos.YPos + 1
                        && o.XPos == currentPos.XPos) ? currentPos.YPos : currentPos.YPos + 1;
                    break;
                case Direction.South:
                    next.YPos = _occupiedGrids.Any(o => o.YPos == currentPos.YPos - 1
                         && o.XPos == currentPos.XPos) ? currentPos.YPos : currentPos.YPos - 1;
                    break;
                case Direction.East:
                    next.XPos = _occupiedGrids.Any(o => o.YPos == currentPos.YPos
                        && o.XPos == currentPos.XPos +1 ) ? currentPos.XPos : currentPos.XPos + 1;
                    break;
                case Direction.West:
                    next.XPos = _occupiedGrids.Any(o => o.YPos == currentPos.YPos
                         && o.XPos == currentPos.XPos - 1) ? currentPos.XPos : currentPos.XPos - 1; ;
                    break;
            }

            return next;
        }

        public void SetGridBoundary(GridPosition upperBound)
        {
            this.GridBoundary = upperBound;
        }

        public void Reset()
        {
            _occupiedGrids.Clear();
            this.GridBoundary.XPos = int.MaxValue;
            this.GridBoundary.YPos = int.MaxValue;
        }
    }
}
