using Godot;
using System;
using Partita;
using GodotInterface;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace Pause{
	public partial class PauseMenu : Control
	{
		private Gioco partita = null;
		private Control panelContainer = null; // Riferimento al nodo "PanelControl"
		private Control volumeMenu = null; // Riferimento al nodo "VolumeMenu"

		// Riferimenti agli slider per il volume
		private HSlider masterSlider = null;
		private HSlider soundEffectsSlider = null;
		private HSlider musicSlider = null;
		
		public override void _Ready()
		{	
			// Inizializzo i due nodi figli principali
			panelContainer = GetNode<Control>("PanelContainer");
			if (panelContainer != null)
				panelContainer.Visible = true;
			volumeMenu = GetNode<Control>("VolumeMenu");
			if (volumeMenu != null)
				volumeMenu.Visible = false;
				
			// Recupera gli slider per ciascun tipo di volume (Master, Sound Effects, Music)
			masterSlider = GetNode<HSlider>("VolumeMenu/PanelContainerVolume/VolumeBox/Master");
			soundEffectsSlider = GetNode<HSlider>("VolumeMenu/PanelContainerVolume/VolumeBox/Sound_Effects");
			musicSlider = GetNode<HSlider>("VolumeMenu/PanelContainerVolume/VolumeBox/Music");
			
			/*
			// Carica i valori dei volumi salvati o usa i valori di default (1.0)
			masterSlider.Value = (float)ProjectSettings.GetSetting("audio/master_volume", 1.0f);
			soundEffectsSlider.Value = (float)ProjectSettings.GetSetting("audio/sound_effects_volume", 1.0f);
			musicSlider.Value = (float)ProjectSettings.GetSetting("audio/music_volume", 1.0f);
			*/
			masterSlider.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
			soundEffectsSlider.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("SFX")));
			musicSlider.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music")));

			// Imposta i valori iniziali ai canali audio
			_OnMasterSliderChanged(masterSlider.Value);
			_OnSoundEffectsSliderChanged(soundEffectsSlider.Value);
			_OnMusicSliderChanged(musicSlider.Value);
				
			Hide();  // Nasconde il menu di pausa all'inizio
		}
	
		public void ShowMenu(Gioco partita)
		{
			GD.Print("Menu Pausa");
			this.partita = partita;
			Show();
			GetTree().Paused = true;
		}
	
		public void HideMenu()
		{
			Hide();
			GetTree().Paused = false;
		}
	
		public async void _OnRiprendiPressed()
		{
			_Click();
			await WaitForTimeout(0.1f);  // Attende 0.1 secondi
			HideMenu();
		}
	
		public async void _OnVolumePressed()
		{
			_Click();
			await WaitForTimeout(0.1f);  // Attende 0.1 secondi
			GD.Print("Menu Volume");
			// Nascondi "PanelControl" e mostra "VolumeMenu"
			if (panelContainer != null)
				panelContainer.Visible = false;
			
			if (volumeMenu != null)
				volumeMenu.Visible = true;
		}

		private async void _OnOkPressed()
		{
			_Click();
			await WaitForTimeout(0.1f);  // Attende 0.1 secondi
			GD.Print("Menu Pausa");
			
			// Nascondi "VolumeMenu" e mostra "PanelControl"
			if (panelContainer != null)
				panelContainer.Visible = true;

			if (volumeMenu != null)
				volumeMenu.Visible = false;

			/*
			// Salva i valori dei volumi nei ProjectSettings
			ProjectSettings.SetSetting("audio/master_volume", masterSlider.Value);
			ProjectSettings.SetSetting("audio/sound_effects_volume", soundEffectsSlider.Value);
			ProjectSettings.SetSetting("audio/music_volume", musicSlider.Value);

			// Salva le impostazioni dei volumi su disco
			ProjectSettings.Save();
			*/
		}

		
		public async void _OnEsciPressed()
		{
			_Click();
			await WaitForTimeout(0.2f);  // Attende 0.2 secondi
			//Global.CurrentBranch = 0;
			//Global.Branches.Clear();  // Elimina tutti gli elementi dall'array
			//Global.CurrentSpot = 1;
			LevelStruct.reset();
			
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
		}
		
		/*GESTIONE VOLUMI*/

		// Gestore del cambio di valore dello slider Master
		private void _OnMasterSliderChanged(double value) {
			float masterVolume = (float)value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), Mathf.LinearToDb(masterVolume));
			// GD.Print("Master volume impostato a: ", masterVolume);
		}

		// Gestore del cambio di valore dello slider Sound Effects
		private void _OnSoundEffectsSliderChanged(double value) {
			float effectsVolume = (float)value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SFX"), Mathf.LinearToDb(effectsVolume));
			// GD.Print("Sound Effects volume impostato a: ", effectsVolume);
		}
		
		// Gestore del cambio di valore dello slider Music
		private void _OnMusicSliderChanged(double value) {
			float musicVolume = (float)value;
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb(musicVolume));
			// GD.Print("Music volume impostato a: ", musicVolume);
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
}
