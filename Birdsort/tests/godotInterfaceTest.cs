using System;
using Xunit;
using Core;

namespace GodotInterface.Tests {
    public class BranchPosInfoTests {
        [Theory]
        [InlineData(4, 69)]  // Se currentMaxSpots è 4
        [InlineData(6, 49)]  // Se currentMaxSpots è 6
        [InlineData(0, 347)] // Se currentMaxSpots è 0 (valore minimo gestibile)
        public void Ratio_CalculatesCorrectly(int currentMaxSpots, int expectedRatio) {
            // Arrange
            LevelInfo.currentMaxSpots = currentMaxSpots;

            // Act
            float actualRatio = GodotInterface.BranchPosInfo.Ratio;

            // Troncare il risultato
            int roundedRatio = (int)Math.Truncate(actualRatio);

            // Assert che il valore arrotondato sia uguale a quello atteso
            Assert.Equal(expectedRatio, roundedRatio);
        }


        [Theory]
        [InlineData(100, 1, 100 * (-8.0f / 165) * 1 - 50)]  // Caso positivo
        [InlineData(-100, -1, -100 * (-8.0f / 165) * -1 - 50)] // Caso negativo
        [InlineData(0, 1, -50)]                              // Caso neutro
        public void OffsetTest(float x, int sign, float expectedOffset) {
            // Act
            float actualOffset = GodotInterface.BranchPosInfo.Offset(x, sign);

            // Assert
            Assert.Equal(expectedOffset, actualOffset, 1); // Confronto con precisione decimale
        }
    }
}