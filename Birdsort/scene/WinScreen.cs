using Godot;
using Core;
using GodotInterface;
using Partita;
using System;
using System.Threading.Tasks; 
using Level;

namespace Win {
	public partial class WinScreen : Control {
		private Control shareControl = null;
		private Control victoryControl = null;
		private Control buttonControl = null;
		
		private RichTextLabel levelText = null;
		private RichTextLabel FirstPointText = null;
		private RichTextLabel pointText = null;
		
		private int xpGained = 0;
		
		public override void _Ready() {
			// inizializzo nodi control
			shareControl = GetNode<Control>("ShareControl");
			if (shareControl != null)
				shareControl.Visible = false;
				
			victoryControl = GetNode<Control>("PanelContainer/VBoxContainer/VictoryControl");
			if (victoryControl != null)
				victoryControl.Visible = false;
				
			buttonControl = GetNode<Control>("PanelContainer/VBoxContainer/ButtonControl");
			if (buttonControl != null)
				buttonControl.Visible = true;
				
			// inizializzo nodi richtextlabel
			levelText = GetNode<RichTextLabel>("PanelContainer/VBoxContainer/VictoryControl/LivelloCompletato");
			pointText = GetNode<RichTextLabel>("PanelContainer/VBoxContainer/VictoryControl/Punteggio");
			FirstPointText = GetNode<RichTextLabel>("PanelContainer/VBoxContainer/ButtonControl/PointsLabel");
			FirstPointText.BbcodeEnabled = true;
			
			xpGained = CreateLevel.GetXpGained();
			FirstPointText.Text = $"[center]Punteggio: {xpGained}[/center]";
			
			Hide();
		}

		public void ShowScreen() {
			Show();
		}

		public void HideScreen() {
			Hide();
		}

		public void _OnMenuPressed() {
			LevelStruct.reset();
			
			GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		}
	
		public void _OnNextPressed() {
			LevelStruct.reset();

			var nextScene = (PackedScene)ResourceLoader.Load("res://scene/gioco.tscn", "", ResourceLoader.CacheMode.Replace);
			var a = (Gioco)nextScene.Instantiate();
			GetTree().ChangeSceneToPacked(nextScene);
		}
		
		public void _OnSharePressed() {
			if (shareControl != null && shareControl.Visible == false){
				shareControl.Visible = true;
			} else if (shareControl != null && shareControl.Visible == true){
				shareControl.Visible = false;
			}
		}
		
		public async Task showScore() {
			if (victoryControl != null && victoryControl.Visible == false){
				shareControl.Visible = false;
				buttonControl.Visible = false;
				// scrivo i punteggi
				levelText.BbcodeEnabled = true;
				pointText.BbcodeEnabled = true;
				// Ottieni l'XP guadagnata dal livello
				uint lv = (GameInfo.currentLevel)-1;
				levelText.Text = $"[center]Livello: {lv}[/center]";
				pointText.Text = $"[center]Punteggio: {xpGained}[/center]";
				//mostro la scena
				victoryControl.Visible = true;
				await WaitForTimeout(0.1f);
			} else if (victoryControl != null && victoryControl.Visible == true){
				victoryControl.Visible = false;
				buttonControl.Visible = true;
				shareControl.Visible = true;
				await WaitForTimeout(0.1f);
			}
		}
		
		// Funzione per creare uno screenshot e restituire il percorso del file
		public async Task<string> TakeScreenshot() {
			await showScore();
			// Breve delay per dare il tempo alle modifiche di renderizzarsi
			await WaitForTimeout(0.3f); 
			
			// Genera il percorso dello screenshot
			string screenshotPath = $"user://screenshot_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";

			// Effettua lo screenshot
			Viewport root = GetViewport();
			Image screenshot = root.GetTexture().GetImage();
			screenshot.SavePng(screenshotPath);
			await showScore();

			// Restituisce il percorso del file creato
			return ProjectSettings.GlobalizePath(screenshotPath);
		}

		// Funzione per condividere tramite Instagram
		public async void _OnInstagramPressed() {
			// Scatta lo screenshot e aprilo
			string screenshotPath = await TakeScreenshot();
			OS.ShellOpen(screenshotPath);

			// Apre Instagram Web nel browser predefinito
			string instagramUrl = "https://www.instagram.com/";
			OS.ShellOpen(instagramUrl);

			// Debug
			GD.Print($"Screenshot salvato in: {screenshotPath}");
			GD.Print("Lo screenshot è stato aperto insieme a Instagram Web. Per favore, carica manualmente lo screenshot su Instagram.");
		}

		// Funzione per condividere tramite Telegram
		public async void _OnTelegramPressed() {
			// Scatta lo screenshot e aprilo
			string screenshotPath = await TakeScreenshot();
			OS.ShellOpen(screenshotPath);

			// Crea il messaggio da inviare
			uint lv = (GameInfo.currentLevel)-1;
			int xpGained = CreateLevel.GetXpGained();
			string message = $"Ho completato il livello {lv} con {xpGained} punti, prova a battermi! #JnglBird";
			string webTelegramUrl = $"https://t.me/share/url?url={Uri.EscapeDataString(" ")}&text={Uri.EscapeDataString(message)}";

			// Apri Telegram nel browser
			OS.ShellOpen(webTelegramUrl);

			// Debug
			GD.Print($"Screenshot salvato in: {screenshotPath}");
			GD.Print("Telegram (web) aperto. Puoi inviare il messaggio insieme allo screenshot manualmente.");
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
}
