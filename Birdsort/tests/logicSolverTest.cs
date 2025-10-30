using System;
using System.Collections.Generic;
using Xunit;
using LogicInterface;

namespace LogicInterface.Tests
{
    public class AStarSolverTests 
    {
        private BirdSortState fillState(BirdSortState s) {
            s.Branches[0].Push(((ushort)1, MalusState.None));
            s.Branches[0].Push(((ushort)1, MalusState.None));
            s.Branches[0].Push(((ushort)2, MalusState.None));
            s.Branches[1].Push(((ushort)2, MalusState.None));
            s.Branches[2].Push(((ushort)1, MalusState.None));
            s.Branches[2].Push(((ushort)2, MalusState.None));
            return s;
        }


        [Fact]
        public void Test_SolvePuzzleAStar_FindsSolution()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );

            // Act
            initialState = fillState(initialState);
            var result = AStarSolver.SolvePuzzleAStar(initialState);

            // Assert
            Assert.False(initialState.IsSolved());
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Test_SolvePuzzleAStar_NoSolutionForImpossibleState()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );

            // Act
            initialState.Branches[0].Push(((ushort)1, MalusState.None));
            var result = AStarSolver.SolvePuzzleAStar(initialState);

            // Assert
            Assert.Empty(result); // No solution should be found for an impossible state
        }

        [Fact]
        public void Test_MoveBird_AppliesValidMove()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );

            // Act
            initialState.Branches[0].Push(((ushort)1, MalusState.None));
            bool moved = initialState.MoveBird(0, 1);

            // Assert
            Assert.True(moved);
            Assert.NotEmpty(initialState.Branches[1]);
            Assert.Equal(((ushort)1, MalusState.None), initialState.Branches[1].Peek());
        }

        [Fact]
        public void Test_MoveBird_FailsForInvalidMove()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );

            // Act
            initialState = fillState(initialState);
            bool moved = initialState.MoveBird(1, 0);

            // Assert
            Assert.False(moved);
        }

        [Fact]
        public void Test_Heuristic_CalculatesCorrectValue()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );

            // Act
            initialState.Branches[0].Push(((ushort)1, MalusState.None));
            initialState.Branches[0].Push(((ushort)1, MalusState.None));
            initialState.Branches[1].Push(((ushort)2, MalusState.None));
            var heuristic = AStarSolver.Heuristic(initialState);

            // Assert
            // Expected heuristic value needs to be calculated based on the current logic
            int expectedHeuristic = 2*(Optimization.HeuristicMultiplier - Optimization.UniformReward);
            Assert.Equal(expectedHeuristic, heuristic);
        }

        // [Fact]
        // public void Test_CalculateCost()
        // {
        //     var nextState = new BirdSortState(
        //         branchCount: 3,
        //         MaxBirdsPerBranch: 3
        //     );

        //     // Act
        //     nextState.Branches[0].Push(((ushort)1, MalusState.None));
        //     nextState.Branches[0].Push(((ushort)1, MalusState.None));
        //     nextState.Branches[1].Push(((ushort)2, MalusState.None));
        //     var cost = AStarSolver.CalculateCost([(0,2),(0,2)], nextState);

        //     // Assert
        //     int expectedCost = 2 + 2*(Optimization.HeuristicMultiplier - Optimization.UniformReward);
        //     Assert.Equal(expectedCost, cost);
        // }

        [Fact]
        public void Test_InitializeOpenSet()
        {
            // Arrange
            var initialState = new BirdSortState(
                branchCount: 3,
                MaxBirdsPerBranch: 3
            );
            initialState = fillState(initialState);
            var openSet = new PriorityQueue<(BirdSortState state, List<(int, int)> moves), int>();
            var visitedStates = new HashSet<string>();

            // Act
            AStarSolver.InitializeOpenSet(initialState, openSet, visitedStates);

            // Assert
            Assert.NotEmpty(visitedStates);
            Assert.Equal(openSet.Peek().Item1, initialState);
        }
    }
}
