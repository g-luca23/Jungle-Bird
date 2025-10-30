using Godot;
using System;
using Core;
using Level;
using GodotInterface;
using gameover;

namespace BombCounter{
	public partial class Bomba : Node2D {
		private GameOverScreen overScreen;
		private AnimatedSprite2D esplosione;
		public int counter { get; set; } = 6;//da fare un setcounterbomb
	


		public override void _Ready(){
			esplosione = GetNode<AnimatedSprite2D>("explosive");
		}

		//Forse posso fare che in process si starta e stoppa ma al di fuori non me lo fa fare

		public void UpdateCounter(){
			if(LevelStruct.bombInGame){
				esplosione.Play("bomboclat");
				if(counter <= 1 ){//secondo check serve bombingame perche potrebbe esplodere prima della creazione del livello
					esplosione.Play("esplodi");

					if(overScreen == null) {
					var overScreenScene = GD.Load<PackedScene>("res://scene/GameOver_screen.tscn");
					overScreen = (GameOverScreen)overScreenScene.Instantiate();
					AddChild(overScreen);
					}
					overScreen.ShowOver(); // Chiamata sul riferimento all'istanza
					// else
					// {
					// 	GD.Print("GameOverscreen non trovato nella scena!");
					// }

				}
				counter--;
				GetNode<TextEdit>("TextEdit").Text = "" + counter;
			}else{
				esplosione.Play("off");
				GetNode<TextEdit>("TextEdit").Text = "";
			}
		}

		
	}

	public partial class BombHandler {
		public static Bomba bomb = null;
		private static int InitialCounter = -1;

		public static void setup(Bomba b){//AGGIUNGERE COUNTER 
			bomb = b;
		}

		public static void setup() {
			bomb = new Bomba();
		}

		public static void UpdateCounter(){
			bomb?.UpdateCounter();
		}

		public static int GetCounter(){
			if(bomb != null)
				return bomb.counter;
			return 0;
		}

		public static int GetInitialCounter() {
			if(bomb != null)
				return InitialCounter;
			return -1;
		}

		public static void SetCounter(int c){
			if(bomb != null) {
				bomb.counter = c;
				InitialCounter = c;
			}
			else {
				GD.Print("null");
				InitialCounter = -1;
			}

			//quello che c'è qui sotto è da decommentare se vuoi testare l'esplosione della bomba(commenta quello scritto sopra)
			// bomb.counter=3;
			// InitialCounter=3;
		}
	}

}
