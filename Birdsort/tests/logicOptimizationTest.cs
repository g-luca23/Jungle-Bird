using System;
using System.Collections.Generic;
using Xunit;
using LogicInterface;

namespace LogicInterface.Tests
{
    public class OptimizationLogicTests
    {
        // Create an instance of BirdSortState (replace with actual constructor and parameters)
        private BirdSortState _birdSortState;

        public OptimizationLogicTests()
        {
            // Initialize the BirdSortState object (example parameters)
            _birdSortState = new BirdSortState(10, 5); // Adjust parameters as needed
        }

        [Fact(Skip = "can't now the best parameters")]
        public void Test_FineTuneParameters_CorrectBestParameters()
        {
        }


        [Fact]
        public void Test_EvaluateParameters_ReturnsCorrectScore()
        {
            // Define the solver function (same as before, this would be the actual solver logic)
            Func<BirdSortState, List<(int, int)>> solverFunc = state =>
            {
                return new List<(int, int)> { (1, 2), (3, 4) };
            };

            // Call EvaluateParameters from the Optimization class
            int score = Optimization.EvaluateParameters(solverFunc, _birdSortState, 5, 1, 3);

            // Assert that the score is calculated correctly
            int expectedScore = 5 * 1 - 1 * 2 + 3 + 5 * 3 - 1 * 4 + 3;
            Assert.Equal(expectedScore, score);
        }

        [Fact]
        public void Test_GenerateRange_ReturnsCorrectRange()
        {
            // Test the GenerateRange method to ensure it produces the correct range of numbers
            var range = Optimization.GenerateRange(1, 3);

            // Assert that the range contains the expected values
            var expectedRange = new List<int> { 1, 2, 3 };
            Assert.Equal(expectedRange, new List<int>(range));
        }
    }
}
