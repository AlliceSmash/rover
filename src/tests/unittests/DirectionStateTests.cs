using common.models;
using NUnit.Framework;
using StateMachine;

namespace unittests
{
    public class DirectionStateTests
    {      
        private DirectionManager _directionManager;

        [OneTimeSetUp]
        public void Setup()
        {
            _directionManager = new DirectionManager();
        }

        [Test]
        public void GetNextDirection_ShouldReturnSameDirection_WhenCommandIsEmptyChar()
        {
            var direction = Direction.East;
            var command = ' ';
            var expected = Direction.East;

            var result = _directionManager.GetNextDirection(direction, command);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetNextDirection_ShouldReturnSameDirection_WhenNotLNorR()
        {
            var direction = Direction.North;
            var command= 'M';
            var expected = Direction.North;

            var result = _directionManager.GetNextDirection(direction, command);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetNextDirection_ShouldReturnCorrectDirection_WhenCommand_L()
        {
            var direction = Direction.North;
            var command = 'L';
            var expected = Direction.West;

            var result = _directionManager.GetNextDirection(direction, command);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetNextDirection_ShouldReturnCorrectDirection_WhenCommand_R()
        {
            var direction = Direction.North;
            var command = 'R';
            var expected = Direction.East;

            var result = _directionManager.GetNextDirection(direction, command);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetNextDirection_ShouldReturnCorrectDirection_WhenCommand_r()
        {
            var direction = Direction.North;
            var command = 'r';
            var expected = Direction.East;

            var result = _directionManager.GetNextDirection(direction, command);

            Assert.AreEqual(expected, result);
        }
    }
}
