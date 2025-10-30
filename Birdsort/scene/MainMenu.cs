using Godot;
using System;
using Partita;
using Core;
using System.Threading.Tasks; 

public partial class MainMenu : Control {
	private static bool firstTime = true;

	public override void _Ready() {
		
		if(firstTime) {
			GameInfo.readCurrentLevel();
			firstTime = false;
		}
		string outline = "Gioca LV." + GameInfo.currentLevel;
		GetNode<Button>("MarginContainer/VBoxContainer/Control/Gioca").Text = outline;
		_StartMusic();
	}

	public async void _OnGiocaPressed() {
		_Click();
		await WaitForTimeout(0.1f);  // Attende 0.1 secondi
		var nextScene = (PackedScene)ResourceLoader.Load("res://scene/gioco.tscn", "", ResourceLoader.CacheMode.Replace);
		var a = (Gioco)nextScene.Instantiate();
		GetTree().ChangeSceneToPacked(nextScene);
	}
	
	public async void _OnEsciPressed() {
		_Click();
		await WaitForTimeout(0.4f);  // Attende 0.4 secondi
		GetTree().Quit(); 
	}
	
	public void _StartMusic() {
		GetNode<AudioStreamPlayer2D>("MenuMusicStreamPlayer2D").Play();
	}
	
	public void _Click() {
		GetNode<AudioStreamPlayer2D>("ButtonStreamPlayer2D").Play();
	}
	
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
