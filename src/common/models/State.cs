using System;
namespace common.models
{
    public enum Direction
    {
        East = 'E',
        South = 'S',
        West = 'W',
        North = 'N'
    }

    public class State : IEquatable<State>
    {

        public Direction FaceDirection { get; set; }
        public GridPosition Position { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(State anotherState)
        {
            return this.FaceDirection
                .Equals(anotherState.FaceDirection)
                && this.Position.Equals(anotherState.Position);
        }

        public override string ToString()
        {
            return string.Concat( Position.ToString(), ' ' , FaceDirection.ToString().Substring(0,1));
        }
    }

    public class GridPosition
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public bool Equals(GridPosition location)
        {
            return this.XPos == location.XPos && this.YPos == location.YPos;
        }

        /// <summary>
        /// Equivalent to MemebershipClone
        /// </summary>
        /// <returns></returns>
        public GridPosition Copy()
        {
            return new GridPosition { XPos = this.XPos, YPos = this.YPos };
        }

        public override string ToString()
        {
            return string.Concat(XPos.ToString(), ' ', YPos.ToString());
        }
    }
}
