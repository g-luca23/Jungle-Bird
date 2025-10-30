using Godot;
using GodotInterface;
using Core;
using Level;
using System;
using Win;
using Pause;
using BombCounter;
using SaveSystem;

namespace Partita {
	public partial class Gioco : Node2D {
		public PauseMenu pauseMenu;
		public WinScreen winScreen;
		private PackedScene branchL;
		private PackedScene branchR;
		private PackedScene Black;
		private PackedScene Blu;
		private PackedScene Freaky;
		private PackedScene Grigio;
		private PackedScene Marrone;
		private PackedScene Pingu;
		private PackedScene Rosso;
		private PackedScene Verde;
		private PackedScene White;
		private PackedScene Goku;
		private PackedScene Hawktuah;
		private BirdClick tmp;
		private int experiencePoints = 0;



		public override void _Ready() {
			//Load branch
			branchL = GD.Load<PackedScene>("res://scene/branchL.tscn");
			branchR = GD.Load<PackedScene>("res://scene/branchR.tscn");

			//Load birds
			Black = GD.Load<PackedScene>("res://scene/Black.tscn");
			Blu = GD.Load<PackedScene>("res://scene/Blu.tscn");
			Freaky = GD.Load<PackedScene>("res://scene/Freaky.tscn");
			Grigio = GD.Load<PackedScene>("res://scene/Grigio.tscn");
			Marrone = GD.Load<PackedScene>("res://scene/Marrone.tscn");
			Pingu = GD.Load<PackedScene>("res://scene/Pingu.tscn");
			Rosso = GD.Load<PackedScene>("res://scene/Rosso.tscn");
			Verde = GD.Load<PackedScene>("res://scene/Verde.tscn");
			White = GD.Load<PackedScene>("res://scene/White.tscn");
			Goku = GD.Load<PackedScene>("res://scene/Goku.tscn");
			Hawktuah = GD.Load<PackedScene>("res://scene/Hawktuah.tscn");
			
			GameInfo.readCurrentState();
			GD.Print("+++++++++++++++Livello creato:" + GameInfo.levelcreated);
			//Setup level
			if(GameInfo.levelcreated == 1) {
				SaveLoad.Load(this);

				// CreateLevel.setupLevel(this);
				// GameInfo.levelcreated = 1;	
				// GameInfo.writeCurrentLevel();	
				// SaveLoad.Save();
			}
			else {
				CreateLevel.setupLevel(this);
				GameInfo.levelcreated = 1;	
				GameInfo.writeCurrentState();	
				SaveLoad.Save();
			}
			_StartMusic();
			//Inizializza il timer per il cooldown del suggerimento
			LevelStruct.InitializeHintTimer();
		}
		
		public void AddExperience(int xp) {
			experiencePoints += xp;
			GD.Print($"XP Totale: {experiencePoints}");
		}

	public void _onHintbuttonPressed(){
		if(!LevelInfo.disableInput){
		GD.Print("Eseguo la mossa migliore...");
		LevelStruct.ExecuteBestMove();
		}
	}

	public int GetExperiencePoints() {
		return experiencePoints;
	}

	public override void _Input(InputEvent @event)
	{
		// Verifica se l'evento è una pressione di un tasto
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if(keyEvent.Keycode == Key.P) {
				GD.Print("--livello skippato--!");
				LevelInfo.finished = true;
			}

		}
	}

		public override void _Process(double delta) {
			if(LevelInfo.finished) {
				if(winScreen == null) {
					var winScreenScene = GD.Load<PackedScene>("res://scene/win_screen.tscn");
					winScreen = (WinScreen)winScreenScene.Instantiate();
					AddChild(winScreen);
					GameInfo.currentLevel ++;
					GameInfo.levelcreated = 0;
					GameInfo.writeCurrentLevel();
					GameInfo.writeCurrentState();
				}

				winScreen.ShowScreen();
			}
		}

		public void _OnPausaPressed() {
			if(!LevelInfo.disableInput){
				if(pauseMenu == null) {
					var pauseMenuScene = GD.Load<PackedScene>("res://scene/pause_menu.tscn");
					pauseMenu = (PauseMenu)pauseMenuScene.Instantiate();
					AddChild(pauseMenu);
				}
				
				//pauseMenu.Call("ShowMenu");
				pauseMenu.ShowMenu(this);
			}
		}

		public void DisplayBranch(char direction, int y) {
			if(direction == 'l') {
				var b = (Node2D)branchL.Instantiate();
				b.Position = new Vector2(BranchPosInfo.LeftBranchX, y);
				AddChild(b);
			}
			else {
				var b = (Node2D)branchR.Instantiate();
				b.Position = new Vector2(BranchPosInfo.RightBranchX, y);
				AddChild(b);
			}
		}

		public void DisplayBird(BirdType bt) {
			BirdClick b = null;
			switch(bt) {
				case BirdType.Black:
					b = (BirdClick)Black.Instantiate();
					b.typeBird = "Black";
					AddChild(b);
					break;
					
				case BirdType.Blu:
					b = (BirdClick)Blu.Instantiate();
					b.typeBird = "Blu";
					AddChild(b);
					break;

				case BirdType.Freaky:
					b = (BirdClick)Freaky.Instantiate();
					b.typeBird = "Freaky";
					AddChild(b);
					break;

				case BirdType.Grigio:
					b = (BirdClick)Grigio.Instantiate();
					b.typeBird = "Grigio";
					AddChild(b);
					break;

				case BirdType.Marrone:
					b = (BirdClick)Marrone.Instantiate();
					b.typeBird = "Marrone";
					AddChild(b);
					break;

				case BirdType.Pingu:
					b = (BirdClick)Pingu.Instantiate();
					b.typeBird = "Pingu";
					AddChild(b);
					break;

				case BirdType.Rosso:
					b = (BirdClick)Rosso.Instantiate();
					b.typeBird = "Rosso";
					AddChild(b);
					break;

				case BirdType.Verde:
					b = (BirdClick)Verde.Instantiate();
					b.typeBird = "Verde";
					AddChild(b);
					break;

				case BirdType.White:
					b = (BirdClick)White.Instantiate();
					b.typeBird = "White";
					AddChild(b);
					break;
				
				case BirdType.Goku:
					b = (BirdClick)Goku.Instantiate();
					b.typeBird = "Goku";
					AddChild(b);
					break;

				case BirdType.Hawktuah:
					b = (BirdClick)Hawktuah.Instantiate();
					b.typeBird = "Hawktuah";
					AddChild(b);
					break;

				default:
					break;
			}

			tmp = b;
		}

		public void setLastMalus(string malus) {
			tmp.SetMalus(malus);
		}

		public static int GetGameHeight() {
				// Usa il Viewport globale per ottenere l'altezza dello schermo
				return (int)ProjectSettings.GetSetting("display/window/size/height");
		}
		
		public void _StartMusic() {
			GD.Print("Start Game Music");
			GetNode<AudioStreamPlayer2D>("GameMusicStreamPlayer2D").Play();
		}

	}
}
