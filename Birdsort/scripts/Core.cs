using Godot;
using System;
using Level;
using System.Collections.Generic;

namespace Core {
	public enum BirdType{
		Black,
		Blu, 
		Freaky,
		Grigio,
		Marrone,
		Pingu,
		Rosso,
		Verde,
		White,
		Goku,
		Hawktuah,
		Invalid
	}

	public partial class LevelInfo {
		public static int totalBranch = 0; //viene calcolata da sola quando aggiungi i rami alla scena
		public static int currentMaxSpots = 7;
		public static int numBirdTypes = 0;
		public static bool finished = false;

		public static bool disableInput = false;

		public static void reset(bool test) {
			totalBranch = 0;
			currentMaxSpots = 7;
			numBirdTypes = 0;
			CreateLevel.resetLevel(test);
			finished = false;
		}

		public static void setLevelInfo(int currentmaxspots, int numbirdtypes) {
			currentMaxSpots = currentmaxspots;
			numBirdTypes = numbirdtypes;
			finished = false;
		}
	}

	public partial class Utility{
		public static BirdType GetBirdType(string birdTypeName){
			switch (birdTypeName.ToLower()){
				case "black":
					return BirdType.Black;
				case "blu":
					return BirdType.Blu;
				case "freaky":
					return BirdType.Freaky;
				case "grigio":
					return BirdType.Grigio;
				case "marrone":
					return BirdType.Marrone;
				case "pingu":
					return BirdType.Pingu;
				case "rosso":
					return BirdType.Rosso;
				case "verde":
					return BirdType.Verde;
				case "white":
					return BirdType.White;
				case "goku":
					return BirdType.Goku;
				case "hawktuah":
					return BirdType.Hawktuah;
				default:
					return BirdType.Invalid;
			}
		}
	}

	public partial class GameInfo {
		public static uint levelcreated = 0;
		public static uint currentLevel = 1;
		private static readonly string filePath = "user://level_num.save";
		private static readonly string lcPath = "user://lc.save";


		public static void readCurrentLevel() {
			if(FileAccess.FileExists(filePath)) {
				var File = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
				currentLevel = File.Get32();
				File.Close();
			}
			else {
				GD.Print("File non trovato");
			}
		}

		public static void writeCurrentLevel() {
			var File = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
			File.Store32(currentLevel);
			File.Close();
		}

		public static void readCurrentState() {
			if(FileAccess.FileExists(lcPath)) {
				var File = FileAccess.Open(lcPath, FileAccess.ModeFlags.Read);
				levelcreated = File.Get32();
				GD.Print("Level created:" + levelcreated);
				File.Close();
			}
			else {
				GD.Print("File non trovato");
			}
		}

		public static void writeCurrentState() {
			var File = FileAccess.Open(lcPath, FileAccess.ModeFlags.Write);
			File.Store32(levelcreated);
			File.Close();
		}
	}
}
