using Godot;
using Core;
using System;
using System.Collections.Generic;
using LogicInterface;
using System.Linq;
using Level;
using BombCounter;


namespace GodotInterface {
	public static class BranchPosInfo {
		//Variabili per le posizioni nella mappa
		public static readonly float LeftBranchX = -257;
		public static readonly float RightBranchX = 259;
		public static readonly float Length = 347;
		public static readonly float AngularCoeff = -8.0f / 165;

		public static float StartLeft => LeftBranchX - Length / 2;
		public static float StartRight => RightBranchX + Length / 2;

		public static float Ratio => Length / (LevelInfo.currentMaxSpots + 1);


		public static float Offset(float x, int sign) {
			return x * AngularCoeff * sign - 50;
		}
	}

	public partial class LevelStruct {

		public static List<BranchClick> branches = new List<BranchClick>();
		public static List<BirdClick> malusbirds = new List<BirdClick>();

		public static bool bombInGame = false;//viene settato true
		public static int currentBranch = 0;
		public static int currentSpot = 0;

		public static BirdClick birdSelected = null;
		public static BranchClick branchSelected = null;
		public static bool canRequestHint = true;
		private static Timer hintCooldownTimer;

		private static void OnHintCooldownFinished() {
				canRequestHint = true;
				GD.Print("Puoi richiedere un nuovo suggerimento.");
		}

		public static void InitializeHintTimer() {
    // Se il timer esiste e appartiene a una scena eliminata, liberalo
    if (hintCooldownTimer != null && Godot.GodotObject.IsInstanceValid(hintCooldownTimer)) {
        hintCooldownTimer.QueueFree();
        hintCooldownTimer = null;
    }

    // Crea un nuovo Timer
    hintCooldownTimer = new Timer();
    hintCooldownTimer.OneShot = true;
    hintCooldownTimer.WaitTime = 2.0f;

    // Connetti il segnale timeout
    hintCooldownTimer.Timeout += OnHintCooldownFinished;

    // Aggiungi il Timer alla scena corrente
    var tree = Godot.Engine.GetMainLoop() as SceneTree;
    var root = tree?.Root;
    if (root != null) {
        root.AddChild(hintCooldownTimer);
        GD.Print("Timer creato e aggiunto alla scena corrente.");
    } else {
        GD.PrintErr("Errore: Nessun nodo root trovato per aggiungere il Timer.");
    }
	}


		public static void reset() {
			//branches.Clear();
			branches = new List<BranchClick>();
			malusbirds = new List<BirdClick>();
			bombInGame = false;
			birdSelected = null;
			branchSelected = null;
			currentBranch = 0;
			currentSpot = 0;
			LevelInfo.reset(false);
		}

		public static void AddBranch(BranchClick b) {
			branches.Add(b);
			LevelInfo.totalBranch ++;
		}

		public static void setSpawn(BirdClick bird) {
			currentSpot = branches[currentBranch].occupiedSlots;
			if (currentSpot > LevelInfo.currentMaxSpots - 1) {
				currentSpot = 0;
				currentBranch++;
			}

			bird.branchLink = branches[currentBranch];
			branches[currentBranch].stackBirdOn.Push(bird);
			branches[currentBranch].occupiedSlots ++;
			bird.GlobalPosition = branches[currentBranch].branchSlotsPos[currentSpot];
		}

		public static void addMalusBird(BirdClick bird){
			malusbirds.Add(bird);
		}

		public static BirdClick delmalusbird(BirdClick bird){//dovrebbe andar bene(spero), elimina l'uccello
			malusbirds.Remove(bird);
			return(bird);
		}

		public static bool checkWin() {
			foreach (BranchClick b in branches) {
				if (b.occupiedSlots != 0) {
					return false;
				}		
			}
			return true;
		}

		public static BirdSortState ToLogicState() {
			int branchCount = branches.Count;
			var logicState = new BirdSortState(branchCount, LevelInfo.currentMaxSpots);

			for (int i = 0; i < branchCount; i++) {
					var uiBranch = branches[i];

					// Inverti l'ordine degli uccelli nel ramo
					List<BirdClick> birdsInBranch = uiBranch.stackBirdOn.ToList().AsEnumerable().Reverse().ToList();

					// Aggiungi gli uccelli nel ramo con i relativi malus
					foreach (var bird in birdsInBranch) {
							var malusState = ConvertToMalusState(bird.Modificatore);
							var birdType = Utils.BirdToCharMap[bird.typeBird];

							// Aggiungi l'uccello al ramo logico
							logicState.Branches[i].Push((birdType, malusState));

							// Aggiorna le informazioni sui malus
							if (malusState.HasFlag(MalusState.Sleep)) {
									if (logicState.SleepMalusBranches[0] == -1) {
											logicState.SleepMalusBranches[0] = i + 1; // Primo branch con Sleep
									} else {
											logicState.SleepMalusBranches[1] = i + 1; // Secondo branch con Sleep
									}
							}

							if (malusState.HasFlag(MalusState.Cage)) {
									logicState.CageMalusBranch = i + 1; // Branch con Cage
							}
					}
			}

			// Conta i rami vuoti
			foreach (var branch in branches) {
					if (branch.stackBirdOn.Count == 0) {
							logicState.NBranchEmpty++;
					}
			}

			return logicState;
	}


		private static MalusState ConvertToMalusState(BirdClick.malus modificatore) {
				MalusState malusState = MalusState.None;

				if (modificatore.bomb) malusState |= MalusState.Bomb;
				if (modificatore.cage) malusState |= MalusState.Cage;
				if (modificatore.key) malusState |= MalusState.Key;
				if (modificatore.clock) malusState |= MalusState.Clock;
				if (modificatore.sleep) malusState |= MalusState.Sleep;

				return malusState;
		}

		public static (int from, int to)? GetHint() {
			if (!canRequestHint) {
					GD.Print("Devi aspettare 2 secondi prima di richiedere un nuovo suggerimento.");
					return null;
			}

			// Imposta il cooldown
			canRequestHint = false;
			hintCooldownTimer.Start();

			// Ottieni lo stato logico corrente
			BirdSortState state = ToLogicState();

			// Calcola la soluzione ottimale utilizzando l'A* Solver
			var solution = AStarSolver.SolvePuzzleAStar(state);

			// Controlla se esiste una soluzione
			if (solution != null && solution.Count > 0) {
					// Restituisci solo la prima mossa
					return solution[0];
			}

			// Se non c'è una soluzione valida, restituisci null
			return null;
	}


		public static void ExecuteBestMove() {
			// Ottieni la prima mossa suggerita dall'algoritmo
			var bestMove = GetHint();

			// Controlla che ci sia una mossa valida
			if (bestMove.HasValue) {
					var (from, to) = bestMove.Value;

					// Ottieni i riferimenti ai rami sorgente e destinazione
					var sourceBranch = branches[from];
					var destinationBranch = branches[to];

					// Verifica che il ramo sorgente abbia almeno un uccello
					if (sourceBranch.stackBirdOn.Count > 0) {
							var birdToMove = sourceBranch.stackBirdOn.Peek();

							// Calcola il numero di uccelli dello stesso tipo che devono essere spostati
							int flockSize = birdToMove.sameBirdsCount(sourceBranch);

							// Assicurati che ci sia abbastanza spazio nel ramo di destinazione
							if (LevelInfo.currentMaxSpots - destinationBranch.occupiedSlots >= flockSize) {
									// Esegui lo spostamento degli uccelli
									for (int i = 0; i < flockSize; i++) {
											var bird = sourceBranch.stackBirdOn.Pop();
											sourceBranch.occupiedSlots--;

											bird.branchLink = destinationBranch;
											destinationBranch.stackBirdOn.Push(bird);
											bird.StartMovement(destinationBranch.branchSlotsPos[destinationBranch.occupiedSlots]);
											destinationBranch.occupiedSlots++;
									}

									GD.Print($"Mossa eseguita: da ramo {from + 1} a ramo {to + 1}");

									// Incrementa il contatore delle mosse (se usato)
									CreateLevel.IncrementMoves();
									BombHandler.UpdateCounter();
							} else {
									GD.Print($"Mossa non valida: spazio insufficiente sul ramo {to + 1}");
							}
					} else {
							GD.Print($"Mossa non valida: ramo {from + 1} vuoto");
					}
			} else {
					GD.Print("Nessuna mossa suggerita disponibile.");
			}
	}

	}
}
