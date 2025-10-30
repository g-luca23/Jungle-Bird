using Godot;
using System;
using Core;
using GodotInterface;
using Level;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using BombCounter;
using System.Collections.Generic;

public partial class DummyBird {

	public Node node;
	public string typeBird;
	public DummyBranch branchLink;
	

	public partial class Malus{ 				//classe per gestire quale malus è presente in un oggetto
		public bool check; 	//è true se c'è almeno un malus
		public bool key;
		public bool cage;
		public bool bomb;
		public bool clock;
		public bool sleep;

		public Malus(bool check = false, bool key = false, bool cage = false, bool bomb = false, bool clock = false, bool sleep = false){
			this.check = check;
			this.cage = cage;
			this.sleep = sleep;
			this.key = key;
			this.clock = clock;
			this.bomb = bomb;
		}

		public void setsleep(){
			if(!this.check){
				this.sleep = true;
				this.check = true;
			}
		}
		public void setclock(){
			if(!this.check){
				this.clock = true;
				this.check = true;
			}
		}

		public void setkey(){
			if(!this.check){
				this.key = true;
				this.check = true;
			}
		}

		public void setcage(){
			if(!this.check){
				this.cage = true;
				this.check = true;
			}
		}

		public void setbomb(){
			if(!this.check){
				this.bomb = true;
				this.check = true;
			}
		}

		public void unset(){
			this.check = false;
			this.cage = false;
			this.sleep = false;
			this.key = false;
			this.clock = false;
			this.bomb = false;
		}

		public void sblocca(){
			this.check=false;
			//if(this.cage)// si potrebbe mettere anche senza if
				this.cage = false;
			//if(this.sleep)
				this.sleep = false;
		}
		
		/* Al momento non utilizzata
		public String currentStatus() {
			if(this.cage) return "cage";
			else if(this.sleep) return "sleep";
			else if(this.key) return "key";
			else if(this.clock) return "clock";
			else if(this.bomb) return "bomb";
			else return "idle";
		}
		*/
	};

	
	private bool isMoving = false; // Indica se l'uccello si sta muovendo
	private static bool IsMovementFinished(float movementProgress) => (movementProgress >= 1.0f);
	public Malus Modificatore = new Malus();

	

	public void SetMalus(string malus) {
		Modificatore.unset();

		if(malus.Equals("bomb"))
			Modificatore.setbomb();
		else if(malus.Equals("cage"))
			Modificatore.setcage();
		else if(malus.Equals("key"))
			Modificatore.setkey();
		else if(malus.Equals("sleep"))
			Modificatore.setsleep();
		else if(malus.Equals("clock"))
			Modificatore.setclock();

		//AddMalusArray(branchLink);// tolto per testare
	}

	public void AddMalusArray(DummyBranch branch) { //messo argomento per testare piu velocemente
		if(Modificatore.cage || Modificatore.sleep){
			branch.addMalusBird(this);
		}
		if(Modificatore.bomb){
			branch.addMalusBird(this);
			//sLevelStruct.bombInGame = true;
		}
	}

	// public bool inFlock() {
	// 	int selectables = sameBirdsCount(branchLink);
	// 	DummyBird[] branchArray = branchLink.stackBirdOn.ToArray();

	// 		bool found = false;
	// 		for(int i=0;i<selectables;i++) {
	// 			if(this.Equals(branchArray[i])) {
	// 				found = true;
	// 				break;
	// 			}
	// 		}
	// 	return found;
	// }



	// public override void _Process(double delta)
	// {
	// 	if(LevelStruct.birdSelected != null && LevelStruct.birdSelected != this 
	// 	&& GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation == "selected" && !isMoving && !inFlock())
	// 	{
	// 		setanimations();
	// 	}
		
	// 	updateDirection();
	// 	// Gestisci il movimento
	// 	if (isMoving) {
	// 		elapsedTime += (float)delta;
	// 		float movementProgress = elapsedTime / moveDuration;

	// 		if (!IsMovementFinished(movementProgress)) 
	// 		{
	// 			var newPosition = startPosition.Lerp(targetPosition, movementProgress);
	// 			GlobalPosition = newPosition;
	// 		}
	// 		else
	// 		{
	// 			GlobalPosition = targetPosition;
	// 			isMoving = false;
	// 			setanimations();
	// 			SetProcess(false); // Disabilita l'elaborazione continua
	// 			if(branchCompleted(branchLink) && this.Equals(this.branchLink.stackBirdOn.Peek())){//
	// 				flyAway(branchLink);
	// 			}
				
				
	// 		}

	// 	}
	// 	si puo eliminare, tanto viene eliminato alla fine della partita
	// 	else { //non funzionava si puo anche togliere
	// 		if (Mathf.IsEqualApprox(this.Position.X, -100f, 0.01f)) // 0.01 è il margine di tolleranza
	// 		{
	// 			this.node.QueueFree();
	// 			this.node = null;
	// 			GD.Print("lo elimino", this);
	// 		}
	// 	}
	// 	updateDirection();
	// }

	// public void updateDirection(){
	// 	if(isMoving){
	// 		if(targetPosition.X > 0){
	// 			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true; //sinistra
	// 		}else{
	// 			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false; //destra
	// 		}
	// 	}
	// 	else {
	// 		if(branchLink.signBranch >= 0)
	// 			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
	// 		else
	// 			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
	// 	}
	// }


	public void removemalus( bool key, bool clock){//tolto tutta la bomba dal test
		// if(bomb){
		// 	// Reset counter
		// 	//BombHandler.SetCounter(0);
		// 	//LevelStruct.bombInGame = false;
		// }

		// Crea una lista temporanea per raccogliere gli uccelli da rimuovere
		List<DummyBird> birdsToRemove = new List<DummyBird>();

		foreach (DummyBird bird in branchLink.malusbirds ) {
			if (key && bird.Modificatore.cage) {
				bird.Modificatore.sblocca(); // Sblocca uccello ingabbiato
				birdsToRemove.Add(bird);
			}
			if (clock && bird.Modificatore.sleep) {
				bird.Modificatore.sblocca(); // Sblocca uccello addormentato
				birdsToRemove.Add(bird);
			}
		}
		
		// Rimuovi gli uccelli accumulati
		foreach (DummyBird bird in birdsToRemove) {

			branchLink.delmalusbird(bird);
		}
	}

	public void flyAway(DummyBranch branch)
	{
		bool hasclock=false;
		bool hasbomb=false;
		bool haskey=false;


		while (branch.stackBirdOn.Count > 0)
		{
			DummyBird bird = branch.stackBirdOn.Pop();

			// if (bird.Modificatore.bomb){
			// 	hasbomb = true;
			// 	bird.Modificatore.bomb = false;
			// 	bird.Modificatore.check= false;
			// }
			if(bird.Modificatore.key){
				haskey = true;
				bird.Modificatore.key = false;
				bird.Modificatore.check = false;
			}
			else if(bird.Modificatore.clock){
				hasclock = true;
				bird.Modificatore.clock = false;
				bird.Modificatore.check= false;
			}
		}


		branch.occupiedSlots = branch.stackBirdOn.Count;

		if(hasbomb || hasclock || haskey){
			removemalus(haskey, hasclock);
		}
	}

	public int sameBirdsCount(DummyBranch branch) {
		string color = this.typeBird;//branch.stackBirdOn.Peek().typeBird;
		int tot = 0;
		foreach(var bird in branch.stackBirdOn) {

			if (bird == null){
				GD.PrintErr("Trovato un uccello nullo nello stack!");
				return -1;
			}

			if(bird.typeBird == color && !(bird.Modificatore.cage || bird.Modificatore.sleep)) tot++;
			else break;

		}

		return tot;
	}

	public bool branchCompleted(DummyBranch branch)
	{			
			// Controlla che il numero di uccelli nel ramo sia uguale al numero massimo consentito
			if (branch.stackBirdOn.Count != branch.currentMaxspots)
			{
					return false;
			}
			// Ottieni il tipo del primo uccello nello stack
			string birdType = null;
			foreach (var bird in branch.stackBirdOn)
			{
					if (bird == null)
					{
							GD.PrintErr("Trovato un uccello nullo nello stack!");
							return false;
					}

					// Imposta il tipo dell'uccello se non è stato ancora definito
					if (birdType == null)
					{
							birdType = bird.typeBird;
					}

					// Confronta il tipo corrente con il tipo del primo uccello
					if (bird.typeBird != birdType || bird.Modificatore.cage || bird.Modificatore.sleep)
					{
							return false;
					}
			}

			// Se tutti i controlli sono passati, il ramo è completo
			return true;
	}


	public Godot.Collections.Dictionary<string, Variant> Save(){
		return new Godot.Collections.Dictionary<string, Variant>(){
			{ "typeBird",typeBird },
			{ "check", Modificatore.check },
			{ "clock", Modificatore.clock },
			{ "sleep", Modificatore.sleep },
			{ "key", Modificatore.key },
			{ "cage", Modificatore.cage },
			{ "bomb", Modificatore.bomb }
		};
	}
}
