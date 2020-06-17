using common.models;

namespace common.interfaces
{
    public interface IStateMachine
    {
        /// <summary>
        /// Get the final state based on initialState after following commandSquence
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="commandSequence"></param>
        /// <param name="resetGrid"></param>
        /// <returns>GetStateResponse {Status, Result}</returns>
        GetStateResponse GetState(State initialState, string commandSequence, bool resetGrid = false);

        /// <summary>
        /// Set upperBound of Plateau
        /// </summary>
        /// <param name="upperBound"></param>
        void SetGridBoundary(GridPosition upperBound);

        /// <summary>
        /// Reset the Plateau upper bound to int.max, also remove all Rovers on the grid
        /// </summary>
        void Reset();
    }
}
