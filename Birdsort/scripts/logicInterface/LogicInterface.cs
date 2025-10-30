using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace LogicInterface {
	public static class Utils{
    public static readonly ImmutableDictionary<string, ushort> BirdToCharMap = new Dictionary<string, ushort>
    {
        { "None", 0 },
        { "Black", 1 << 0 },
        { "Blu", 1 << 1 },
        { "Freaky", 1 << 2 },
        { "Grigio", 1 << 3 },
        { "Marrone", 1 << 4 },
        { "Pingu", 1 << 5 },
        { "Rosso", 1 << 6 },
        { "Verde", 1 << 7 },
        { "White", 1 << 8 },
        { "Goku", 1 << 9 },
        { "Hawktuah", 1 << 10 }
    }.ToImmutableDictionary();

	  public const int bombMaxMoves = 15;
	}

	public enum MalusState
	{
			None = 0,
			Key = 1 << 0,
			Cage = 1 << 1,
			Bomb = 1 << 2,
			Clock = 1 << 3,
			Sleep = 1 << 4
	}

	public class BirdSortState {
		public List<Stack<(ushort, MalusState)>> Branches { get; private set; }
		public int NBranchEmpty{ get; set; } = 0;
		public int TotMoves{ get; set; } = 0;
		
		// malusBranch è un array di 2 nel caso fossimo in un game da 2 malus => 'sleep-clock'
		// altrimenti viene usato solo malusBranches[0] nel caso 'bomb' o nel caso 'cage-key'
		public int[] SleepMalusBranches{ get; set; } = new int[2];
		public int CageMalusBranch{ get; set; } = -1;

		public bool HasBombMalus{ get; set; } = false;
		private readonly int MaxBirdsPerBranch;

		public int getMaxBirdsPerBranch() {
			return MaxBirdsPerBranch;
		}

		public BirdSortState(int branchCount, int MaxBirdsPerBranch) {
			Branches = new List<Stack<(ushort, MalusState)>>();
			this.MaxBirdsPerBranch = MaxBirdsPerBranch;
			for (int i = 0; i < branchCount; i++) {
				Branches.Add(new Stack<(ushort, MalusState)>());
			}
			
			SleepMalusBranches[0] = SleepMalusBranches[1] = -1;
		}

		public BirdSortState Clone() {
				var clone = new BirdSortState(Branches.Count, MaxBirdsPerBranch);

				// Clona i rami
				for (int i = 0; i < Branches.Count; i++) {
						clone.Branches[i] = new Stack<(ushort, MalusState)>(new Stack<(ushort, MalusState)>(Branches[i]));
				}

				// Copia il conteggio dei rami vuoti
				clone.NBranchEmpty = NBranchEmpty;

				// Copia i malus
				Array.Copy(SleepMalusBranches, clone.SleepMalusBranches, SleepMalusBranches.Length);
				clone.CageMalusBranch = CageMalusBranch;
				clone.HasBombMalus = HasBombMalus;

				return clone;
		}


		public bool isExploding() {
			return HasBombMalus && TotMoves >= Utils.bombMaxMoves;
		}

		public void FreeCageBirds()
		{
				// Verifica se l'indice è valido
				if (CageMalusBranch <= 0 || CageMalusBranch > Branches.Count)
				{
						return;
				}

				// Ottieni il ramo corrispondente
				var branch = Branches[CageMalusBranch - 1];

				// Crea una pila aggiornata
				var updatedBranch = new Stack<(ushort, MalusState)>();
				foreach (var bird in branch)
				{
						// Rimuovi il malus Sleep dagli uccelli
						if (bird.Item2 == MalusState.Cage)
						{
								updatedBranch.Push((bird.Item1, MalusState.None));
						}
						else
						{
								updatedBranch.Push(bird);
						}
				}

				// Ripristina l'ordine corretto nello stack
				Branches[CageMalusBranch - 1] = new Stack<(ushort, MalusState)>(updatedBranch);
				CageMalusBranch = -1;
		}


		public void freeBranch(Stack<(ushort, MalusState)> destination) {
			if(destination.Count != MaxBirdsPerBranch) return;

			ushort topBird = destination.Peek().Item1;
			bool wakeClockBirds = false;
			bool openCageBirds = false;
			bool defuse = false;

			foreach(var bird in destination) {
				if(bird.Item1 != topBird || bird.Item2 == MalusState.Cage || bird.Item2 == MalusState.Sleep)
					return;

				if(bird.Item2 == MalusState.Key) 
					openCageBirds = true;

				if(bird.Item2 == MalusState.Clock)
					wakeClockBirds = true;

				if(bird.Item2 == MalusState.Bomb)
					defuse = true;
			}

			if(wakeClockBirds) {
				FreeSleepingBirds();
			}
			
			if(defuse){
				HasBombMalus = false;
			}

			if(openCageBirds) {
				FreeCageBirds();
			}

			destination.Clear(); // Gli uccelli volano via
			NBranchEmpty++;
		}

		public void FreeSleepingBirds()
		{
				foreach (int branchIndex in SleepMalusBranches)
				{
						// Verifica se l'indice è valido
						if (branchIndex == -1)
						{
								continue; // Salta branch non validi
						}

						// Ottieni il ramo corrispondente
						var branch = Branches[branchIndex - 1];

						// Crea una pila aggiornata
						var updatedBranch = new Stack<(ushort, MalusState)>();
						foreach (var bird in branch)
						{
								// Rimuovi il malus Sleep dagli uccelli
								if (bird.Item2 == MalusState.Sleep)
								{
										updatedBranch.Push((bird.Item1, MalusState.None));
								}
								else
								{
										updatedBranch.Push(bird);
								}
						}

						// Ripristina l'ordine corretto nello stack
						Branches[branchIndex - 1] = new Stack<(ushort, MalusState)>(updatedBranch);
				}

				// Resetta le informazioni sul malus Sleep
				SleepMalusBranches[0] = SleepMalusBranches[1] = -1;
		}

		public bool MoveBird(int from, int to)
		{
				var source = Branches[from];
				var destination = Branches[to];

				if (source.Count == 0) return false; // No bird to move
				
				// Identify the bird type to move
				ushort birdToMove = source.Peek().Item1;
				var birdsToMove = new Stack<(ushort, MalusState)>();

				// Collect all adjacent birds of the same type
				if (!CollectBirdsToMove(source, birdToMove, birdsToMove))
						return false;

				// Check if the move is valid
				if (!IsMoveValid(destination, birdsToMove.Count))
						return false;

				// Update the state of empty branches
				UpdateEmptyBranches(source, destination);

				// Execute the move
				PerformMove(birdsToMove, destination);

				// Controlla se il ramo di destinazione è pieno e uniforme
				freeBranch(destination);

				TotMoves++;

				return true;
		}

		private static bool CollectBirdsToMove(Stack<(ushort, MalusState)> source, ushort birdToMove, Stack<(ushort, MalusState)> birdsToMove)
		{
				while (source.Count > 0 && source.Peek().Item1 == birdToMove)
				{
						if (source.Peek().Item2 == MalusState.Cage || source.Peek().Item2 == MalusState.Sleep)
						{
								if (birdsToMove.Count == 0) return false;
								else break;
						}
						birdsToMove.Push(source.Pop());
				}
				return true;
		}

		private bool IsMoveValid(Stack<(ushort, MalusState)> destination, int birdsToMoveCount)
		{
				if (destination.Count + birdsToMoveCount > MaxBirdsPerBranch)
				{
						return false;
				}
				return true;
		}

		private void UpdateEmptyBranches(Stack<(ushort, MalusState)> source, Stack<(ushort, MalusState)> destination)
		{
				if (destination.Count == 0) NBranchEmpty--;
				if (source.Count == 0) NBranchEmpty++;
		}

		private static void PerformMove(Stack<(ushort, MalusState)> birdsToMove, Stack<(ushort, MalusState)> destination)
		{
				while (birdsToMove.Count > 0)
				{
						destination.Push(birdsToMove.Pop());
				}
		}

		public bool IsSolved() {
			return (NBranchEmpty == Branches.Count);
		}

		public string GetFormattedBoard() {
			var boardRepresentation = new List<string>();

			for (int i = 0; i < Branches.Count; i++) {
				string branchContents = string.Join(" ", Branches[i].Reverse());
				boardRepresentation.Add($"Branch {i + 1}: [{branchContents}]");
			}

			return string.Join("\n", boardRepresentation);
		}
	}

}
