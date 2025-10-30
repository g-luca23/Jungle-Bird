using Godot;
using System;
using Core;
using Level;
using Xunit;

namespace Core.Tests {
    public class LevelInfoTests {
        [Fact]
        public void resetTest() {
            //Act
            LevelInfo.reset(true);

            //Assert
            Assert.Equal(0, LevelInfo.totalBranch);
            Assert.Equal(7, LevelInfo.currentMaxSpots);
            Assert.Equal(0, LevelInfo.numBirdTypes);
            Assert.False(LevelInfo.finished);
            Assert.Equal(0, CreateLevel.Difficulty);
            Assert.Equal(LevelConst.defaultBranches, CreateLevel.NumberOfBranches);
            Assert.Equal(LevelConst.defaultBirds, CreateLevel.NumberOfBirds);
        }

        [Fact]
        public void setLevelInfoTest() {
            //Act
            LevelInfo.setLevelInfo(6, 4);

            //Assert
            Assert.Equal(6, LevelInfo.currentMaxSpots);
            Assert.Equal(4, LevelInfo.numBirdTypes);
            Assert.False(LevelInfo.finished);
        }
    }

    public class UtilityTests {
        [Theory]
        [InlineData("black", BirdType.Black)]
        [InlineData("blu", BirdType.Blu)]
        [InlineData("freaky", BirdType.Freaky)]
        [InlineData("grigio", BirdType.Grigio)]
        [InlineData("marrone", BirdType.Marrone)]
        [InlineData("pingu", BirdType.Pingu)]
        [InlineData("rosso", BirdType.Rosso)]
        [InlineData("verde", BirdType.Verde)]
        [InlineData("white", BirdType.White)]
        [InlineData("goku", BirdType.Goku)]
        [InlineData("hawktuah", BirdType.Hawktuah)]
        [InlineData("ROsSO", BirdType.Rosso)]
        public void GetBirdTypeTestValidInputs(string input, BirdType expected) {
            // Act
            var result = Utility.GetBirdType(input);

            // Assert
            Assert.IsType<BirdType>(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("invalid", BirdType.Invalid)]
        [InlineData("", BirdType.Invalid)]
        [InlineData("UNKNOWN", BirdType.Invalid)]
        public void GetBirdTypeTestInvalid(string input, BirdType expected) {
            // Act
            var result = Utility.GetBirdType(input);

            // Assert
            Assert.IsType<BirdType>(result);
            Assert.Equal(expected, result);
        }
    }

    public class GameInfoTests {
        [Fact(Skip = "Godot.FileAccess cannot be tested in this context. Cannot call function.")]
        public void readCurrentLevelTest() {
        }

        [Fact(Skip = "Godot.FileAccess cannot be tested in this context. Cannot call function.")]
        public void writeCurrentLevelTest() {
        }

        [Fact(Skip = "Godot.FileAccess cannot be tested in this context. Cannot call function.")]
        public void readCurrentStateTest() {
        }

        [Fact(Skip = "Godot.FileAccess cannot be tested in this context. Cannot call function.")]
        public void writeCurrentStateTest() {
        }
    }

}