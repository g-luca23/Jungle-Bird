using Godot;
using System;
using Core;
using Level;
using Xunit;
using System.Linq;

namespace Level.Tests {
    public class DifficultyCalculatorTests {
        [Theory]
        [InlineData(0, 4)]
        [InlineData(1, 4)]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        [InlineData(4, 5)]
        [InlineData(5, 6)]
        [InlineData(6, 6)]
        [InlineData(7, 6)]
        [InlineData(8, 7)]
        [InlineData(10, 7)]
        public void CalcMaxSpotsTest(int difficulty, int expectedMaxSpots) {
            // Act
            var result = DifficultyCalculator.CalcMaxSpots(difficulty);

            // Assert
            Assert.Equal(expectedMaxSpots, result);
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(1, 5)]
        [InlineData(2, 6)]
        [InlineData(3, 7)]
        [InlineData(4, 8)]
        [InlineData(5, 8)]
        [InlineData(6, 9)]
        [InlineData(7, 10)]
        [InlineData(8, 10)]
        [InlineData(10, 12)]
        public void CalcNBirdTypeTest(int difficulty, int expectedNBirdType) {
            // Act
            var result = DifficultyCalculator.CalcNBirdType(difficulty);

            // Assert
            Assert.Equal(expectedNBirdType, result);
        }
    }

    public class CreateLevelTest {
        [Fact(Skip = "Save system cannot be tested in this context.")]
        public void LoadTest() {
        }

        [Fact (Skip = "Node creation cannot be tested in this context.")]
        public void setupLevelTest() {
        }

        [Fact]
        public void resetLevelTest() {
            //Act
            CreateLevel.resetLevel(true);

            //Assert
            Assert.Equal(0, CreateLevel.Difficulty);
            Assert.Equal(LevelConst.defaultBranches, CreateLevel.NumberOfBranches);
            Assert.Equal(LevelConst.defaultBirds, CreateLevel.NumberOfBirds);
        }

        [Fact (Skip = "Timer cannot be tested in this context.")]
        public void StartLevelTimerTest() {
        }

        [Fact (Skip = "Timer cannot be tested in this context.")]
        public void StopLevelTimerTest() {
        }

        [Fact]
        public void ElapsedTimeTest() {
            //Arrange
            CreateLevel.ElapsedTime = 0f;

            //Act
            CreateLevel.OnTimerTimeout();

            //Assert
            Assert.Equal(1.0f, CreateLevel.GetElapsedTime());
        }

        [Fact]
        public void Shuffle_ShufflesArrayCorrectly() {
            // Arrange
            int[] originalArray = { 1, 2, 3, 4, 5 };
            int[] arrayToShuffle = (int[])originalArray.Clone();
            Random rng = new Random();

            // Act
            CreateLevel.Shuffle(rng, arrayToShuffle);

            // Assert
            Assert.Equal(originalArray.OrderBy(x => x), arrayToShuffle.OrderBy(x => x));

            Assert.False(originalArray.SequenceEqual(arrayToShuffle), "L'array non è stato mescolato.");
        }

        [Fact]
        public void Shuffle_ProducesDifferentResultsWithDifferentSeeds() {
            // Arrange
            int[] arrayToShuffle1 = { 1, 2, 3, 4, 5 };
            int[] arrayToShuffle2 = { 1, 2, 3, 4, 5 };

            Random rng1 = new Random(12345); // Seme fisso
            Random rng2 = new Random(54321); // Seme diverso

            // Act
            CreateLevel.Shuffle(rng1, arrayToShuffle1);
            CreateLevel.Shuffle(rng2, arrayToShuffle2);

            // Assert
            Assert.NotEqual(arrayToShuffle1, arrayToShuffle2);
        }


        [Theory]
        [InlineData(50, 10, 1, 6, 5, 4, 490)]    // Caso con difficoltà bassa e condizioni medie
        [InlineData(200, 50, 5, 10, 8, 6, 1020)] // Penalità alte per tempo e mosse in difficoltà media
        [InlineData(5, 1, 1, 6, 5, 4, 1280)]     // Bonus massimo in difficoltà bassa
        [InlineData(10, 2, 9, 14, 11, 7, 11350)] // Bonus massimo in difficoltà alta
        public void CalculateXPTest(
            int timeTaken, 
            int moves, 
            int difficulty, 
            int totalBranch, 
            int numBirdTypes, 
            int currentMaxSpots, 
            int expectedXP
        ) {
            //Arrange
            LevelInfo.totalBranch = totalBranch;
            LevelInfo.numBirdTypes = numBirdTypes;
            LevelInfo.currentMaxSpots = currentMaxSpots;

            //Act
            CreateLevel.LevelXP = CreateLevel.CalculateXP(timeTaken, moves, difficulty);

            //Assert
            Assert.Equal(expectedXP, CreateLevel.GetXpGained());
        }

        [Theory]
        [InlineData(50, 10, 1, 6, 5, 4, 490)]    // Caso con difficoltà bassa e condizioni medie
        [InlineData(200, 50, 5, 10, 8, 6, 1020)] // Penalità alte per tempo e mosse in difficoltà media
        [InlineData(10, 2, 9, 14, 11, 7, 11350)] // Bonus massimo in difficoltà alta
        public void EndLevelTest(
            int timeTaken,
            int moves,
            int difficulty,
            int totalBranch,
            int numBirdTypes,
            int currentMaxSpots,
            int expectedXP
        ) {
            // Arrange
            LevelInfo.totalBranch = totalBranch;
            LevelInfo.numBirdTypes = numBirdTypes;
            LevelInfo.currentMaxSpots = currentMaxSpots;
            CreateLevel.ElapsedTime = timeTaken;
            CreateLevel.numberOfMoves = moves;
            CreateLevel.Difficulty = difficulty;

            // Act
            CreateLevel.endLevel(true); // Esegui la funzione con il parametro `test = true`

            // Assert
            int actualXP = CreateLevel.LevelXP; // Recupera l'XP calcolata
            Assert.Equal(expectedXP, actualXP);

            // Opzionale: verificare se lo stato del livello è aggiornato correttamente
            Assert.True(LevelInfo.finished);
            Assert.Equal(timeTaken, (int)CreateLevel.GetElapsedTime()); // Controlla che il tempo sia stato salvato correttamente
            Assert.Equal(moves, CreateLevel.numberOfMoves);       // Controlla che il numero di mosse sia corretto
        }

        [Fact]
        public void IncrementMovesTest() {
            //Arrange
            CreateLevel.numberOfMoves = 0;

            //Act
            CreateLevel.IncrementMoves();

            //Assert
            Assert.Equal(1, CreateLevel.numberOfMoves);
        }

    }

    public class CreateMalusTests {
        [Theory]
        [InlineData(5, 0)]
        [InlineData(8, 1)]
        [InlineData(15, 2)]
        [InlineData(120, 1)]
        public void SetupMalusDataTest(uint currentLevel, int expected) {
            //Arrange
            GameInfo.currentLevel = currentLevel;

            //Act
            CreateMalus.SetupMalusData();

            //Arrange
            Assert.Equal(expected, CreateMalus.NMalus);

            CreateMalus.NMalus = 0;
        }
    }
}