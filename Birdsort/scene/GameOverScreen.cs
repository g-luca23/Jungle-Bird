using Godot;
using System;
using Core;
using GodotInterface;
using Partita;
using System.Threading.Tasks; 
using Level;

namespace gameover{
	public partial class GameOverScreen : Control
	{
		private Control buttonControl = null;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{					
			buttonControl = GetNode<Control>("PanelContainer/VBoxContainer/Control");
				if (buttonControl != null)
					buttonControl.Visible = true;
			
			Hide();
		}

		public async void ShowOver() {
			LevelInfo.disableInput=true;
			await WaitForTimeout(2.2f);  // Attende 0.2 secondi in piu rispetto al movimento dell'uccello
			Show();
			GetTree().Paused = true;
		}

		public void HideOver() {
			Hide();
			GetTree().Paused = false;

		}
		public void _on_esci_pressed()
		{
			//_Click();
			//Global.CurrentBranch = 0;
			//Global.Branches.Clear();  // Elimina tutti gli elementi dall'array
			//Global.CurrentSpot = 1;
			LevelStruct.reset();

			GetTree().Paused = false;
			LevelInfo.disableInput=false;
			GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		}
		
		//public void _Click() {
		//GetNode<AudioStreamPlayer2D>("ButtonStreamPlayer2D").Play();
		//}
		//
		private async Task WaitForTimeout(float seconds) {
			// Timer e TaskCompletionSource
			var timer = GetTree().CreateTimer(seconds);  
			var tcs = new TaskCompletionSource<bool>();  // Crea 

			// Collega l'evento Timeout al TaskCompletionSource
			timer.Timeout += () => tcs.SetResult(true);

			// Attende che il timer scada
			await tcs.Task;
		}
	}
}
