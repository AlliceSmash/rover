using common.models;

namespace common.interfaces
{
    public interface IDirectionState
    {
        Direction GetNextDirection(Direction currentDirection, char command);
    }
}
