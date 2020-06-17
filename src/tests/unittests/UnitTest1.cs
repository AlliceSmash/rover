using common.models;
using NUnit.Framework;
using StateMachine;
using common.interfaces;

namespace unittests
{
    public class IntegrationTests
    {
        private IDirectionState _directionManager;
        private StateMachine.StateMachine stateMachine;

        [OneTimeSetUp]
        public void Setup()
        {
            _directionManager = new DirectionManager();
            stateMachine = new StateMachine.StateMachine(_directionManager);
        }

        [SetUp]
        public void Init()
        {
            stateMachine.Reset();
        }

        [Test]
        public void GetState_ShouldReturnCorrectPosition()
        {
            var initialState = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 1, YPos = 1 }
            };
            var commandSequence = "LLLLM";
            var expected = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 1, YPos = 2 }
            };

            var result = stateMachine.GetState(initialState, commandSequence);

            Assert.AreEqual(expected, result.StateResult);
            Assert.AreEqual(GetStateStatus.Success, result.StateStatus);
        }

        [Test]
        public void GetState_ShouldReturnCorrectPosition_TestInput1()
        {
            var initialState = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 1, YPos = 2 }
            };
            var commandSequence = "LMLMLMLMM";
            var expected = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 1, YPos = 3 }
            };

            var result = stateMachine.GetState(initialState, commandSequence);

            Assert.AreEqual(expected, result.StateResult);
            Assert.AreEqual(GetStateStatus.Success, result.StateStatus);
        }

        [Test]
        public void GetState_ShouldReturnCorrectPosition_TestInput1_2()
        {
            var initialState = new State
            {
                FaceDirection = Direction.East
                    ,
                Position = new GridPosition { XPos = 3, YPos = 3 }
            };
            var commandSequence = "MMRMMRMRRM";
            var expected = new State
            {
                FaceDirection = Direction.East,
                Position = new GridPosition { XPos = 5, YPos = 1 }
            };

            var result = stateMachine.GetState(initialState, commandSequence);

            Assert.AreEqual(expected, result.StateResult);
            Assert.AreEqual(GetStateStatus.Success, result.StateStatus);
        }

        [Test]
        public void GetState_ShouldReturnCorrectPosition_TestInput2_1()
        { 
            var initialState = new State
            {
                FaceDirection = Direction.South
                    ,
                Position = new GridPosition { XPos = 0, YPos = 0 }
            };
            var commandSequence = "LMMLM";
            var expected = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 2, YPos = 1 }
            };

            var result = stateMachine.GetState(initialState, commandSequence);

            Assert.AreEqual(expected, result.StateResult);
            Assert.AreEqual(GetStateStatus.Success, result.StateStatus);
        }

        [Test]
        public void GetState_ShouldReturnCorrectPosition_TestInput2_2()
        {
            var initialState = new State
            {
                FaceDirection = Direction.South
                   ,
                Position = new GridPosition { XPos = 0, YPos = 0 }
            };
            var commandSequence = "LMMLM";
            var expected = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 2, YPos = 1 }
            };

            stateMachine.GetState(initialState, commandSequence);
            var secondState = new State
            {
                FaceDirection = Direction.West
                    ,
                Position = new GridPosition { XPos = 1, YPos = 2 }
            };
            commandSequence = "LMLMRM";
            expected = new State
            {
                FaceDirection = Direction.South,
                Position = new GridPosition { XPos = 1, YPos = 0 }
            };

            var result = stateMachine.GetState(secondState, commandSequence);

            Assert.AreEqual(expected, result.StateResult);
            Assert.AreEqual(GetStateStatus.Success, result.StateStatus);
        }

        [Test]
        public void GetState_ShouldReturn_CommandWouldCauseOffGridStatus()
        {
            var initialState = new State
            {
                FaceDirection = Direction.South
                  ,
                Position = new GridPosition { XPos = 0, YPos = 0 }
            };
            var commandSequence = "RMMLM";
            var expected = new GetStateResponse
            {
                StateResult = initialState,
                StateStatus = GetStateStatus.CommandCouldCauseOffGrid
            };

            stateMachine.SetGridBoundary(new GridPosition { XPos = 1, YPos = 1 });
            var result = stateMachine.GetState(initialState, commandSequence);
            Assert.AreEqual(GetStateStatus.CommandCouldCauseOffGrid, result.StateStatus);

        }

        [Test]
        public void GetState_ShouldReturn_CommandWouldCauseOffGridStatus_UpperBound()
        {
            var initialState = new State
            {
                FaceDirection = Direction.North,
                Position = new GridPosition { XPos = 2, YPos = 1 }
            };
            var commandSequence = "RMMLM";
            var expected = new GetStateResponse
            {
                StateResult = new State { FaceDirection = Direction.North, Position = new GridPosition { XPos = 3, YPos = 2 } },
                StateStatus = GetStateStatus.CommandCouldCauseOffGrid
            };

            stateMachine.SetGridBoundary(new GridPosition { XPos = 3, YPos = 3 });
            var result = stateMachine.GetState(initialState, commandSequence);
            Assert.AreEqual(GetStateStatus.CommandCouldCauseOffGrid, result.StateStatus);
            Assert.AreEqual(expected.StateResult, result.StateResult);
        }
    }
}