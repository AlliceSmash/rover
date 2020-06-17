using common.models;

namespace common.interfaces
{
    public interface IStateMachine
    {
        GetStateResponse GetState(State initialState, string commandSequence, bool resetGrid = false);
        void SetGridBoundary(GridPosition upperBound);
        void Reset();
    }
}
