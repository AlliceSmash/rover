using common.models;

namespace common.interfaces
{
    public interface IDirectionState
    {
        /// <summary>
        /// Get the next direction based on input command from the current direction
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Direction GetNextDirection(Direction currentDirection, char command);
    }
}
