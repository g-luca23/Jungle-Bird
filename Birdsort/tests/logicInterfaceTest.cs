using Xunit;
using LogicInterface;
using System.Collections.Generic;
using System.Linq;

namespace LogicInterface.Tests
{
    public class LogicInterfaceTests
    {
        // Test per verificare che il BirdToCharMap contenga "Black"
        [Fact]
        public void BirdToCharMap_ShouldContainBlack()
        {
            // Arrange & Act
            var containsBlack = Utils.BirdToCharMap.ContainsKey("Black");

            // Assert
            Assert.True(containsBlack, "BirdToCharMap should contain 'Black'.");
        }

        // Test per verificare che il valore associato a "Black" sia corretto
        [Fact]
        public void BirdToCharMap_Black_ShouldHaveValueOne()
        {
            // Arrange & Act
            var blackValue = Utils.BirdToCharMap["Black"];

            // Assert
            Assert.Equal(1 << 0, blackValue); // 1 << 0 = 1
        }

        // Test per verificare che il valore associato a "Blu" sia corretto
        [Fact]
        public void BirdToCharMap_Blu_ShouldHaveValueTwo()
        {
            // Arrange & Act
            var bluValue = Utils.BirdToCharMap["Blu"];

            // Assert
            Assert.Equal(1 << 1, bluValue); // 1 << 1 = 2
        }

        // Test per verificare che la creazione di un oggetto BirdSortState avvenga correttamente
        [Fact]
        public void BirdSortState_ShouldBeCreatedCorrectly()
        {
            // Arrange & Act
            var birdSortState = new BirdSortState(3, 5);

            // Assert
            Assert.Equal(3, birdSortState.Branches.Count); // Dovrebbero esserci 3 rami
            Assert.Equal(0, birdSortState.NBranchEmpty);  // Nessun ramo dovrebbe essere vuoto inizialmente
            Assert.Equal(-1, birdSortState.SleepMalusBranches[0]); // I malus Sleep dovrebbero essere inizializzati a -1
            Assert.Equal(-1, birdSortState.SleepMalusBranches[1]);
            Assert.Equal(-1, birdSortState.CageMalusBranch); // Il malus Cage dovrebbe essere inizializzato a -1
            Assert.False(birdSortState.HasBombMalus); // Non ci dovrebbe essere malus Bomb inizialmente
        }

        // Test per verificare che la funzione Clone() funzioni correttamente
        [Fact]
        public void BirdSortState_Clone_ShouldCreateCopy()
        {
            // Arrange
            var birdSortState = new BirdSortState(3, 5);
            birdSortState.NBranchEmpty = 2;

            // Act
            var clonedState = birdSortState.Clone();

            // Assert
            Assert.NotSame(birdSortState, clonedState); // Il clone non dovrebbe essere lo stesso oggetto
            Assert.Equal(birdSortState.NBranchEmpty, clonedState.NBranchEmpty); // Il valore di NBranchEmpty dovrebbe essere uguale
            Assert.NotSame(birdSortState.Branches, clonedState.Branches); // I rami dovrebbero essere riferimenti diversi
        }

        // Test per verificare che isExploding() ritorni il valore corretto
        [Fact]
        public void BirdSortState_IsExploding_ShouldReturnTrue_WhenHasBombMalusAndMaxMovesReached()
        {
            // Arrange
            var birdSortState = new BirdSortState(3, 5)
            {
                HasBombMalus = true,
                TotMoves = Utils.bombMaxMoves
            };

            // Act
            var result = birdSortState.isExploding();

            // Assert
            Assert.True(result, "The game should be exploding when the bomb malus is present and max moves are reached.");
        }

        // Test per verificare che FreeCageBirds() rimuova correttamente il malus Cage
        [Fact]
        public void BirdSortState_FreeCageBirds_ShouldRemoveCageMalus()
        {
            // Arrange
            var birdSortState = new BirdSortState(3, 5);
            birdSortState.CageMalusBranch = 1;
            birdSortState.Branches[0].Push((1, MalusState.Cage));

            // Act
            birdSortState.FreeCageBirds();

            // Assert
            Assert.Equal(MalusState.None, birdSortState.Branches[0].Peek().Item2); // Il malus Cage dovrebbe essere rimosso
            Assert.Equal(-1, birdSortState.CageMalusBranch); // Il malus CageBranch dovrebbe essere resettato a -1
        }

        // Test per verificare che freeBranch() svuoti correttamente il ramo e incrementi NBranchEmpty
        [Fact]
        public void BirdSortState_FreeBranch_ShouldEmptyBranchAndIncrementNBranchEmpty()
        {
            // Arrange
            var state = new BirdSortState(3, 5); // Aggiusta i parametri se necessario
            var branch = state.Branches[0];

            // Aggiungi il numero massimo di uccelli al ramo
            while (branch.Count < state.getMaxBirdsPerBranch()) 
            {
                branch.Push((1, MalusState.None)); // Aggiungi un uccello con stato 'None'
            }

            // Act
            state.freeBranch(branch); // Chiama freeBranch che svuota il ramo solo se ha il numero massimo di uccelli

            // Assert
            Assert.Empty(branch);  // Verifica che il ramo sia vuoto
            Assert.Equal(1, state.NBranchEmpty);  // Verifica che NBranchEmpty sia stato incrementato
        }

        // Test per verificare che MoveBird() ritorni il valore corretto e aggiorni lo stato
        [Fact]
        public void BirdSortState_MoveBird_ShouldMoveBirdCorrectly()
        {
            // Arrange
            var birdSortState = new BirdSortState(3, 5);
            var sourceBranch = birdSortState.Branches[0];
            var destinationBranch = birdSortState.Branches[1];

            sourceBranch.Push((1, MalusState.None));

            // Act
            var result = birdSortState.MoveBird(0, 1);

            // Assert
            Assert.True(result, "Move should return true when the move is valid.");
            Assert.Empty(sourceBranch); // Il ramo di origine dovrebbe essere vuoto
            Assert.Single(destinationBranch); // Il ramo di destinazione dovrebbe contenere un solo uccello
        }

        // Test per verificare che IsSolved() ritorni true quando il gioco è risolto
        [Fact]
        public void BirdSortState_IsSolved_ShouldReturnTrue_WhenAllBranchesEmpty()
        {
            // Arrange
            var birdSortState = new BirdSortState(3, 5);
            birdSortState.NBranchEmpty = 3;

            // Act
            var result = birdSortState.IsSolved();

            // Assert
            Assert.True(result, "The game should be solved when all branches are empty.");
        }

        // Test per verificare la rappresentazione formattata del board
        [Fact]
        public void BirdSortState_GetFormattedBoard_ShouldReturnCorrectRepresentation()
        {
            // Arrange
            var birdSortState = new BirdSortState(2, 5);
            birdSortState.Branches[0].Push((1, MalusState.None));
            birdSortState.Branches[1].Push((2, MalusState.None));

            // Act
            var result = birdSortState.GetFormattedBoard();

            // Assert
            Assert.Contains("Branch 1: [(1, None)]", result);
            Assert.Contains("Branch 2: [(2, None)]", result);
        }

    }
        
}
