using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Partita;
using GodotInterface;
using BombCounter;


namespace Level {
	public partial class LevelConst {
		public static readonly int minBranches = 5;
		public static readonly int maxBranches = 14;
		public static readonly int minSpots = 4;
		public static readonly int maxSpots = 7;
		public static readonly int maxDifficulty = 10;
		public static readonly int branchSpacing = 75; // Spazio tra i branch
		public static readonly int defaultBranches = 14;
		public static readonly int defaultBirds = 15;

		public static readonly Vector2 flyoutL= new Vector2 (-700,-700);//cambiare

		public static readonly Vector2 flyoutR= new Vector2 (700,-700);//cambiare

	}

	public static class DifficultyCalculator {
		public static int CalcMaxSpots(int difficulty) {
			return difficulty switch {
				< 2 => 4,
				<= 4 => 5,
				<= 7 => 6,
				_ => 7
			};
		}

		public static int CalcNBirdType(int difficulty) {
			return difficulty switch {
				<= 4 => difficulty + 4,
				<= 7 => difficulty + 3,
				_ => difficulty + 2
			};
		}
	}

	public partial class CreateLevel {
		private static readonly Random RandomGenerator = new Random();
		private static Timer LevelTimer;
		public static float ElapsedTime;
		public static int numberOfMoves = 0;

		private static Gioco currentGame;

		public static int Difficulty { get; set; } = 0;
		public static int NumberOfBranches { get; private set; } = LevelConst.defaultBranches;
		public static int NumberOfBirds { get; private set; } = LevelConst.defaultBirds;
		public static int LevelXP { get; set; } = 0;

		public static void Load(Gioco partita, int D, int Nrami, List<Godot.Collections.Dictionary<string, Variant>> birddatalist){
			currentGame = partita;
			Difficulty = D;
			numberOfMoves = 0;

			LevelTimer = new Timer();
			LevelTimer.WaitTime = 1.0f;
			LevelTimer.OneShot = false;
			LevelTimer.Autostart = false;

			currentGame.AddChild(LevelTimer);

			LevelTimer.Timeout += OnTimerTimeout;

			GD.Print("Timer aggiunto alla scena e segnale connesso.");


			ElapsedTime = 0.0f;
			StartLevelTimer();

			LevelInfo.setLevelInfo(
				DifficultyCalculator.CalcMaxSpots(Difficulty),
				DifficultyCalculator.CalcNBirdType(Difficulty)
			);

			NumberOfBranches = Nrami;
			CreateBranches(LevelConst.branchSpacing);

			foreach (Godot.Collections.Dictionary<string, Variant> data in birddatalist) {
				BirdType pluh = Utility.GetBirdType((string) data["typeBird"]);
				partita.DisplayBird(pluh);
				if( (bool) data["check"] ) {
					if( (bool) data["key"] )
						partita.setLastMalus("key");
					else if( (bool) data["cage"] )
						partita.setLastMalus("cage");
					else if( (bool) data["clock"] )
						partita.setLastMalus("clock");
					else if( (bool) data["sleep"] )
						partita.setLastMalus("sleep");
					else if( (bool) data["bomb"] ) {
						partita.setLastMalus("bomb");
						// BombHandler.setup(partita.GetNode<Bomba>("Bomba"));
						// BombHandler.SetCounter(RandomGenerator.Next(18, 21));
					}
				}
			}

		}


		public static void setupLevel(Gioco partita) {
			currentGame = partita;
			numberOfMoves = 0;
			
			LevelTimer = new Timer();
			LevelTimer.WaitTime = 1.0f;
			LevelTimer.OneShot = false;
			LevelTimer.Autostart = false;

			currentGame.AddChild(LevelTimer);

			LevelTimer.Timeout += OnTimerTimeout;
			GD.Print("Timer aggiunto alla scena e segnale connesso.");
		

			ElapsedTime = 0.0f;
			StartLevelTimer();

			CreateMalus.SetupMalusData();
			//se ci sono i malus, la difficolta max è -1
			if(CreateMalus.NMalus == 0)
				Difficulty = RandomGenerator.Next(LevelConst.maxDifficulty);
			else if(CreateMalus.NMalus == 1)
				Difficulty = RandomGenerator.Next(LevelConst.maxDifficulty-1);
			else
				Difficulty = RandomGenerator.Next(6, LevelConst.maxDifficulty-1);
			GD.Print($"Difficoltà: {Difficulty}");

			NumberOfBranches = Difficulty < 10 ? Difficulty + 5 : LevelConst.defaultBranches;
			GD.Print($"Numero branch: {NumberOfBranches}");

			LevelInfo.setLevelInfo(
				DifficultyCalculator.CalcMaxSpots(Difficulty),
				DifficultyCalculator.CalcNBirdType(Difficulty)
			);

			GD.Print($"Numero max spots: {LevelInfo.currentMaxSpots}");
			GD.Print($"Numero tipi di uccelli: {LevelInfo.numBirdTypes}");

			NumberOfBirds = LevelInfo.currentMaxSpots * LevelInfo.numBirdTypes;

			CreateBranches(LevelConst.branchSpacing);
			CreateBirds();
			CreateMalus.GenerateMalus(partita);
		}

		public static void resetLevel(bool test) {
			if(!test)
				StopLevelTimer();
			Difficulty = 0;
			NumberOfBranches = LevelConst.defaultBranches;
			NumberOfBirds = LevelConst.defaultBirds;
		}

		public static void StartLevelTimer() {
			if (LevelTimer != null) {
				ElapsedTime = 0.0f;
				LevelTimer.Start(); // Questo avvia il timer
				GD.Print("Timer avviato");
			} else {
				GD.PrintErr("ERRORE : Il Timer non è inizializzato.");
			}
		}


		public static void StopLevelTimer() {
			if (LevelTimer != null) {
				LevelTimer.Stop();
				GD.Print($"Timer fermato: Tempo totale {ElapsedTime} secondi");
			} else {
				GD.PrintErr("Tentativo di fermare un timer non avviato.");
			}
		}


		public static void OnTimerTimeout() {
			ElapsedTime += 1.0f; // Incrementa di 1 ogni volta che il timeout del timer scatta
		}


		public static float GetElapsedTime() {
			return ElapsedTime;
		}

		private static void CreateBranches(int branchSpacing) {
			int centerY = Gioco.GetGameHeight() / 2;

			GD.Print("Numero malus: " + CreateMalus.NMalus);

			int nb = 0;

			if(CreateMalus.NMalus >= 1)
				nb = NumberOfBranches + 1;
			else
				nb = NumberOfBranches;

			

			for (int i = 0; i < nb; i++) {
				int yPosition = centerY + (i - NumberOfBranches / 2) * branchSpacing;
				char side = (i % 2 == 0) ? 'r' : 'l';
				currentGame.DisplayBranch(side, yPosition);
			}
		}

		private static void CreateBirds() {
			Array types = Enum.GetValues(typeof(BirdType));
			int[] shuffledIndices = Enumerable.Range(0, NumberOfBirds).ToArray();
			Shuffle(RandomGenerator, shuffledIndices);

			foreach (int i in shuffledIndices) {
				var birdType = (BirdType)types.GetValue(i / LevelInfo.currentMaxSpots);
				currentGame.DisplayBird(birdType);
			}
		}

		public static void Shuffle<T>(Random rng, T[] array) {
			int n = array.Length;
			while (n > 1) {
				int k = rng.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}

		public static int CalculateXP(int timeTaken, int moves, int difficulty) {
			const int baseXP = 100; // XP base per completare un livello
			const int timeBonusFactor = 10; // Bonus per ogni secondo risparmiato rispetto a un tempo massimo
			const int movePenaltyFactor = 5; // Penalità per ogni mossa in più
			const int branchBonus = 20; // Bonus per ogni ramo completato
			const int birdTypeBonus = 15; // Bonus per ogni tipo di uccello diverso nel livello

			// Fattori basati su LevelInfo
			int totalBranchBonus = LevelInfo.totalBranch * branchBonus;
			int birdTypeBonusTotal = LevelInfo.numBirdTypes * birdTypeBonus;

			// Calcola un tempo massimo teorico in base alla difficoltà e agli spot
			int maxTime = LevelInfo.currentMaxSpots * LevelConst.maxDifficulty;

			// Calcola il tempo bonus, evitando valori negativi
			int timeBonus = Math.Max(0, (maxTime - timeTaken) * timeBonusFactor);

			// Calcola la penalità per le mosse, evitando valori negativi
			int movesPenalty = Math.Max(0, moves * movePenaltyFactor);

			// Calcola il moltiplicatore basato sulla difficoltà
			int difficultyMultiplier = difficulty + 1;

			// Somma tutti i valori
			int xpGained = baseXP + timeBonus + totalBranchBonus + birdTypeBonusTotal - movesPenalty;

			// Applica il moltiplicatore della difficoltà e assicura che non sia mai negativo
			int tot = Math.Max(0, xpGained * difficultyMultiplier);
			return tot;
		}

		public static int GetXpGained() {
			return LevelXP;
		}

		public static void endLevel(bool test) {
			LevelInfo.finished = true;
			if(!test)
				CreateLevel.StopLevelTimer();

			int timeTaken = (int)CreateLevel.GetElapsedTime();
			LevelXP = CalculateXP(timeTaken, numberOfMoves, Difficulty); // Salva l'XP calcolata

			if(!test) {
				currentGame.AddExperience(LevelXP);

				GD.Print($"Livello completato! Hai guadagnato {LevelXP} XP.");
				GD.Print($"Tempo impiegato per completare il livello: {timeTaken} secondi");
				GD.Print($"Numero di mosse effettuate: {numberOfMoves}");
			}
		}

		public static void IncrementMoves() {
			numberOfMoves++;
		}
	}

	public partial class CreateMalus {
		private static readonly Random RandomGenerator = new Random();
		public static int NMalus { get; set; } = 0;
		public static List<string> ExcludedTypes = new List<string>();
		public static List<BranchClick> ExcludedBranches = new List<BranchClick>();

		public static void SetupMalusData() {
			//Calcola N malus
			if(GameInfo.currentLevel % 8 == 0)
				NMalus = 1;
			else if(GameInfo.currentLevel % 15 == 0)
				NMalus = 2;
			else return;
			
			//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
			//Bisogna rimettere la parte giusta, lascio cosi per test
			//NMalus = 2;
		}
		public static void GenerateMalus(Gioco partita) {
			List<string> malusOptions = new List<string> { "cage", "sleep", "bomb" };
			for(int i=0; i<NMalus; i++) {
				//Lo stesso malus non deve ripetersi piu di una volta
				int index = RandomGenerator.Next(malusOptions.Count);
				string malus = malusOptions[index];
				malusOptions.RemoveAt(index);

				if(malus.Equals("sleep") || malus.Equals("cage")) {
					//i bird con il malus sleep possono essere 1 o 2
					int MalusIstance = 1;
					if(malus.Equals("sleep")) {
						MalusIstance = RandomGenerator.Next(1,3);
					}

					GD.Print("Uccelli con malus: " + MalusIstance);

					//trova rami diversi tra di loro
					List<BranchClick> malusB = Enumerable.Repeat<BranchClick>(null, MalusIstance).ToList();
					BranchClick bonusB = null;
					FillBranches(malusB, ref bonusB);

					GD.Print(bonusB);
					foreach(var branch in malusB)
						GD.Print(branch);


					//trova uccello/i malus
					List<BirdClick> targetMalus = new List<BirdClick>();
					for(int j=0; j<MalusIstance; j++) {
						AllocateMalus(malusB[j], targetMalus);
					}
					foreach(BranchClick branch in malusB)
						ExcludedBranches.Add(branch);

					//trova uccello bonus
					int BonusSlot = RandomGenerator.Next(LevelInfo.currentMaxSpots);
					List<BirdClick> bonusL = GetBirdList(bonusB);
					BirdClick TargetBonus = FindBonusBird(bonusL, BonusSlot);

					while(TargetBonus == null) {
						bonusB = FindBackup();
						bonusL = GetBirdList(bonusB);
						TargetBonus = FindBonusBird(bonusL, BonusSlot);
					}
					ExcludedBranches.Add(bonusB);


					//setta bonus/malus
					if (malus.Equals("sleep")) {
						for (int z = 0; z < targetMalus.Count; z++) {
							targetMalus[z].SetMalus("sleep");
						}
						TargetBonus.SetMalus("clock");
					} else {
						for (int z = 0; z < targetMalus.Count; z++) {
							targetMalus[z].SetMalus("cage");
						}
						TargetBonus.SetMalus("key");
					}


				}
				else if(malus.Equals("bomb")) {
					BranchClick b = null;
					do {
						b = LevelStruct.branches[RandomGenerator.Next(LevelStruct.branches.Count)];
					}
					while(b.stackBirdOn.Count == 0 || ExcludedBranches.Contains(b));
					ExcludedBranches.Add(b);

					int limit = LevelInfo.currentMaxSpots / 2;
					int malusBird = RandomGenerator.Next(limit);

					//prendi il bird su cui applicare il malus
					BirdClick target = null;
					int current = 0;
					IEnumerator<BirdClick> enumerator = b.stackBirdOn.GetEnumerator();

					while (enumerator.MoveNext()) {
						if(current == malusBird) {
							target = enumerator.Current;
							break;
						}

						current ++;
					}

					target.SetMalus("bomb");

					BombHandler.setup(partita.GetNode<Bomba>("Bomba"));
					BombHandler.SetCounter(CreateLevel.NumberOfBranches * 6);
				}
			}
		}

		private static void FillBranches(List<BranchClick> malusB, ref BranchClick bonusB) {
			//sceglie un ramo non vuoto su cui mettere il bonus
			do {
				bonusB = LevelStruct.branches[RandomGenerator.Next(LevelStruct.branches.Count)];
			}
			while(bonusB.stackBirdOn.Count == 0);

			GD.Print("ramo bonus: " + bonusB);
			
			//sceglie uno o piu rami non vuoti per i malus diversi
			bool MalusCond = false;
			do {
				MalusCond = false;
				for(int i=0; i<malusB.Count; i++) {
					malusB[i] = LevelStruct.branches[RandomGenerator.Next(LevelStruct.branches.Count)];
					if(malusB[i].Equals(bonusB) || malusB[i].stackBirdOn.Count == 0) {
						MalusCond = true;
						break;
					}
				}

				//true se ci sono duplicati, false altrimenti
				if(!MalusCond)
					MalusCond = malusB.GroupBy(p => p).Any(g => g.Count() > 1);

				GD.Print("Cercando rami malus");

 			}
			while(MalusCond);	

			GD.Print("Trovati rami malus");		
		}

		private static void AllocateMalus(BranchClick branch, List<BirdClick> targetMalus) {
			int Limit = LevelInfo.currentMaxSpots / 2;

			//foreach(BranchClick branch in malusB) {
				int MalusSlot = RandomGenerator.Next(Limit, LevelInfo.currentMaxSpots);
				IEnumerator<BirdClick> enumerator = branch.stackBirdOn.GetEnumerator();
				
				//aggiungi il malus alla lista ed escludi i tipi
				//che gli stanno dietro per la generazione dei bonus
				bool MalusFound = false;
				int current = 0;
				while(enumerator.MoveNext()) {
					if(current == MalusSlot) {
						targetMalus.Add(enumerator.Current);
						MalusFound = true;
						GD.Print("aggiunto");
					}

					if(MalusFound)
						ExcludedTypes.Add(enumerator.Current.typeBird);

					current ++;
				}
				ExcludedBranches.Add(branch);
			//}
		}

		private static BranchClick FindBackup() {
			BranchClick backup = null;

			do {
				backup = LevelStruct.branches[RandomGenerator.Next(LevelStruct.branches.Count)];
			}
			while(backup.stackBirdOn.Count == 0 || ExcludedBranches.Contains(backup));

			return backup;
		}

		private static List<BirdClick> GetBirdList(BranchClick b) {
			IEnumerator<BirdClick> enumerator = b.stackBirdOn.GetEnumerator();
			List<BirdClick> l = new List<BirdClick>();
			while (enumerator.MoveNext()) {
				l.Add(enumerator.Current);
			}
			return l;
		}

		private static BirdClick FindBonusBird(List<BirdClick> l, int suggested) {
			BirdClick res = null;
			if( !ExcludedTypes.Contains(l[suggested].typeBird) ) {
				res = l[suggested];
			}
			else {
				foreach(BirdClick bird in l) {
					if(!ExcludedTypes.Contains(bird.typeBird)) {
						res = bird;
						break;
					}
				}
			}

			return res;
		}
	}
}
