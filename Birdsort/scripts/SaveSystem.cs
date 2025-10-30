using Godot;
using System;
using GodotInterface;
using Level;
using System.Collections.Generic;
using Partita;
using BombCounter;


namespace SaveSystem {
	public partial class SaveLoad{
		public static void Save(){
			var filePath = "user://save_game.save";
			var File = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
			File.Store32(Convert.ToUInt32(CreateLevel.Difficulty));
			List<BranchClick> Branches = LevelStruct.branches;
			uint contatore = 0;

			foreach (BranchClick branch in Branches){
				contatore++;
			}
			File.Store32(contatore);

			int bc = BombHandler.GetInitialCounter();
    		File.Store32(unchecked((uint)bc));

			foreach (BranchClick branch in Branches){
				var birds = branch.stackBirdOn.ToArray(); // Copia gli uccelli in un array.
				for (int i = birds.Length - 1; i >= 0; i--) // Itera al contrario.
				{
					BirdClick bird = birds[i];

					// Call the node's save function.
					var nodeData = bird.Call("Save");

					// Json provides a static method to serialized JSON string.
					var jsonString = Json.Stringify(nodeData);

					// Store the save dictionary as a new line in the save file.
					File.StoreLine(jsonString);
				}
			}
			File.Close();
			GD.Print("File salvato !");
		}

		public static void Load(Gioco partita){
			var filePath = "user://save_game.save"; // Custom save file path
			if(FileAccess.FileExists(filePath)) {
				var File = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
				uint Difficolta = File.Get32();
				uint contatore = File.Get32();
				int bc = unchecked((int)File.Get32());
				if(bc >= 0) {
					BombHandler.setup(partita.GetNode<Bomba>("Bomba"));
					BombHandler.SetCounter(bc);
				}

				List<Godot.Collections.Dictionary<string, Variant>> birddatalist = new List<Godot.Collections.Dictionary<string, Variant>>();

				while (File.GetPosition() < File.GetLength()){
					var jsonString = File.GetLine();

					// Creates the helper class to interact with JSON.
					var json = new Json();
					var parseResult = json.Parse(jsonString);
					if (parseResult != Error.Ok)
					{
						GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
						continue;
					}
					var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
					birddatalist.Add(nodeData);
					
				}
				CreateLevel.Load(partita, Convert.ToInt32(Difficolta), Convert.ToInt32(contatore), birddatalist);
				GD.Print("File caricato !");			
			}
			else {
				GD.Print("File non trovato");
			}
			
		}
	}
}
