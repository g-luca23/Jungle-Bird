using Xunit;
using LogicInterface;
using System.Collections.Generic;
using System.Linq;

namespace LogicInterface.Tests
{
    public class DevUtilsTests
    {
        [Fact]
        public void FormatSolution_ShouldReturnNoSolutionFound_WhenSolutionIsNullOrEmpty()
        {
            // Arrange
            var state = new BirdSortState(2, 5);

            // Act
            string resultNull = DevUtils.FormatSolution(null, state);
            string resultEmpty = DevUtils.FormatSolution(new List<(int, int)>(), state);

            // Assert
            Assert.Equal("No solution found", resultNull);
            Assert.Equal("No solution found", resultEmpty);
        }

        [Fact]
        public void FormatSolution_ShouldFormatSolutionCorrectly()
        {
            // Arrange
            var initialState = new BirdSortState(2, 5);
            initialState.Branches[0].Push((1, MalusState.None));
            initialState.Branches[0].Push((1, MalusState.None));

            var solution = new List<(int from, int to)>
            {
                (0, 1),
                (0, 1)
            };

            // Act
            string result = DevUtils.FormatSolution(solution, initialState);

            // Assert
            Assert.Contains("Move from Branch 1 to Branch 2", result);
            Assert.Contains("Total moves: 2", result);
        }

        [Fact(Skip = "GD.Print cannot be tested in this context")]
        public void DisplayState_ShouldPrintCorrectly()
        {
        }

        [Fact]
        public void FormatArray_ShouldReturnCorrectStringRepresentation()
        {
            // Arrange
            int[] array = { -1, 2, 0 };

            // Act
            string result = typeof(DevUtils)
                .GetMethod("FormatArray", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] { array }) as string;

            // Assert
            Assert.Equal("None, 2, None", result);
        }

        [Fact]
        public void FormatBird_ShouldReturnFormattedString()
        {
            // Arrange
            var bird = ((ushort)1, MalusState.Bomb | MalusState.Cage);  // Cast to ushort

            // Act
            string result = typeof(DevUtils)
                .GetMethod("FormatBird", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] { bird }) as string;

            // Assert
            Assert.Equal("Black (Bomb, Cage)", result);
        }

        
    }
        
}
