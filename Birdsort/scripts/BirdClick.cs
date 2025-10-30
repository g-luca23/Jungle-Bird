using Godot;
using System;
using Core;
using GodotInterface;
using Level;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using BombCounter;
using System.Collections.Generic;

public partial class BirdClick : Node2D {

	public Node node;
	public string typeBird;
	public BranchClick branchLink;
	

	public partial class malus{ 				//classe per gestire quale malus è presente in un oggetto
		public bool check; 	//è true se c'è almeno un malus
		public bool key;
		public bool cage;
		public bool bomb;
		public bool clock;
		public bool sleep;

		public malus(bool check = false, bool key = false, bool cage = false, bool bomb = false, bool clock = false, bool sleep = false){
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

	
	// Variabili di movimento
	private float moveDuration = 2.0f; // Durata del movimento in secondi
	private Vector2 startPosition;
	private Vector2 targetPosition;
	private float elapsedTime = 0.0f;
	private bool isMoving = false; // Indica se l'uccello si sta muovendo
	private static bool IsMovementFinished(float movementProgress) => (movementProgress >= 1.0f);
	public malus Modificatore = new malus();
	

	public override void _Ready() {
		LevelStruct.setSpawn(this);
		node = this;
		AddMalusArray();
		//typeBird = Regex.Replace(node.Name, @"\d+$", "");// prende il nome del nodo e rimuove i numeri alla fine
		setanimations();
	}

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

		AddMalusArray();
		setanimations();
	}

	public void AddMalusArray() {
		if(Modificatore.cage || Modificatore.sleep){
			LevelStruct.addMalusBird(this);
			GD.Print("uccello aggiunto in malus:", this);
		}
		if(Modificatore.bomb){//(ho messo qui il setbombcount, molto probabilmente non va bene)
			//setbombcount()
			LevelStruct.addMalusBird(this);
			LevelStruct.bombInGame = true;
		}
	}

	public bool inFlock() {
		int selectables = sameBirdsCount(branchLink);
		BirdClick[] branchArray = branchLink.stackBirdOn.ToArray();

			bool found = false;
			for(int i=0;i<selectables;i++) {
				if(this.Equals(branchArray[i])) {
					found = true;
					break;
				}
			}
		return found;
	}

	public override void _Input(InputEvent @event) {
		if (isMoving)
			return;

		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && !LevelInfo.disableInput){
			// foreach(BirdClick bird in LevelStruct.malusbirds){
			// 	GD.Print(bird);
			// }
			var globalMousePos = GetGlobalMousePosition(); //prendi posizione globale del cursore
			var localMousePos = GetNode<CollisionShape2D>("CollisionShape2D").ToLocal(globalMousePos); //prendi posizione locale del cursore
			
			var collisionshape = GetNode<CollisionShape2D>("CollisionShape2D");
			if(collisionshape.Shape is CircleShape2D) //controlla che sia effettivamente l'hitbox
			{
				CircleShape2D circleShape = (CircleShape2D)collisionshape.Shape;
				float raggioCerchio = circleShape.Radius; //zzzzzzzzzzzzz

				if (localMousePos.DistanceTo(Vector2.Zero) <= raggioCerchio && LevelStruct.birdSelected != this){ 
					//calcola distanza del mouse dall'uccello 
					
					var currentBird = this;

					//PRIMA: 
					//if (currentBird.Equals(currentBird.branchLink.stackBirdOn.Peek()) ){ //seleziona solo il primo uccello del branch
					if(inFlock() && !currentBird.branchLink.stackBirdOn.Peek().isMoving ) { 
					//ADESSO: seleziona il gruppo di uccelli più esterno, clickando su uno qualsiasi di loro
							
							// Deseleziona altri uccelli selezionati
							if (LevelStruct.birdSelected != null){
								LevelStruct.birdSelected.deselectBird();
							}

							int selectables = sameBirdsCount(branchLink);
							BirdClick[] branchArray = branchLink.stackBirdOn.ToArray();
							
							//GD.Print("Bird selected: ", this);

							for(int i=0;i<selectables;i++) {
								//GD.Print(branchArray[i]);
								
								AnimatedSprite2D sprite = branchArray[i].GetNode<AnimatedSprite2D>("AnimatedSprite2D");
								//GD.Print(sprite);
								sprite.Play("selected");
							}
							LevelStruct.birdSelected = branchArray[0];

							//GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("selected");
							GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
						}
				}
			}
		}
	}

	public void deselectBird() {
		int selectables = sameBirdsCount(branchLink);
		BirdClick[] branchArray = branchLink.stackBirdOn.ToArray();
		for(int i=0;i<selectables;i++) {

			var animatedSprite = branchArray[i].GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			if (animatedSprite.Animation == "selected")
				branchArray[i].setanimations();
		}
		LevelStruct.birdSelected = null;
	}

	public void StartMovement(Vector2 destination)
	{
		// if (isMoving)
		// 	return;

		// Imposta le variabili di movimento
		startPosition = GlobalPosition;
		targetPosition = destination;
		elapsedTime = 0.0f;
		isMoving = true;

		// Avvia animazione e process loop
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("flying");
		SetProcess(true);
	}  

	public override void _Process(double delta)
	{
		if(LevelStruct.birdSelected != null && LevelStruct.birdSelected != this 
		&& GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation == "selected" && !isMoving && !inFlock())
		{
			setanimations();
		}
		
		updateDirection();
		// Gestisci il movimento
		if (isMoving) {
			elapsedTime += (float)delta;
			float movementProgress = elapsedTime / moveDuration;

			if (!IsMovementFinished(movementProgress)) 
			{
				var newPosition = startPosition.Lerp(targetPosition, movementProgress);
				GlobalPosition = newPosition;
			}
			else
			{
				GlobalPosition = targetPosition;
				isMoving = false;
				setanimations();
				SetProcess(false); // Disabilita l'elaborazione continua
				if(branchCompleted(branchLink) && this.Equals(this.branchLink.stackBirdOn.Peek())){//
					flyAway(branchLink);
				}
				
				
			}

		}
		//si puo eliminare, tanto viene eliminato alla fine della partita
		// else { //non funzionava si puo anche togliere
		// 	if (Mathf.IsEqualApprox(this.Position.X, -100f, 0.01f)) // 0.01 è il margine di tolleranza
		// 	{
		// 		this.node.QueueFree();
		// 		this.node = null;
		// 		GD.Print("lo elimino", this);
		// 	}
		// }
		updateDirection();
	}

	public void updateDirection(){
		if(isMoving){
			if(targetPosition.X > 0){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true; //sinistra
			}else{
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false; //destra
			}
		}
		else {
			if(branchLink.signBranch >= 0)
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
			else
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
		}
	}

	public void removemalus(bool bomb, bool key, bool clock){ //funziona meglio se metto tutti i bird con malus in un array/lista
		if(bomb){
			// Reset counter
			BombHandler.SetCounter(0);
			LevelStruct.bombInGame = false;
		}

		// Crea una lista temporanea per raccogliere gli uccelli da rimuovere
		List<BirdClick> birdsToRemove = new List<BirdClick>();

		foreach (BirdClick bird in LevelStruct.malusbirds) {
			GD.Print(bird);
			if (key && bird.Modificatore.cage) {
				bird.Modificatore.sblocca(); // Sblocca uccello ingabbiato
				birdsToRemove.Add(bird);
			}
			if (clock && bird.Modificatore.sleep) {
				bird.Modificatore.sblocca(); // Sblocca uccello addormentato
				birdsToRemove.Add(bird);
			}
			bird.setanimations();
		}
		
		// Rimuovi gli uccelli accumulati
		foreach (BirdClick bird in birdsToRemove) {
			if(branchCompleted(bird.branchLink)){
				flyAway(bird.branchLink);
			}
			GD.Print(LevelStruct.delmalusbird(bird));
		}
	}

	public void flyAway(BranchClick branch)
	{
		// Timer timer = new Timer();
		// timer.WaitTime = 0.5f; // Imposta la durata del timer (in secondi)
		// timer.OneShot = false;  // Imposta il timer per attivarsi una sola volta
		// timer.Autostart = false; 
		// AddChild(timer);
		BirdClick[] tofree = new BirdClick[LevelInfo.currentMaxSpots];//non sono sicuro di questa cosa
		//int i=0;
		bool hasclock=false;
		bool hasbomb=false;
		bool haskey=false;


		while (branch.stackBirdOn.Count > 0)
		{
			BirdClick bird = branch.stackBirdOn.Pop();
			//tofree[i]=bird;
			// Verifica e rimuove il nodo animato
			
			if (bird.node != null)
			{	
				GD.Print("vola via uccellino:", bird);
				if(branch.signBranch<=-1)
					bird.StartMovement(LevelConst.flyoutL);//inserire posizione
				else
					bird.StartMovement(LevelConst.flyoutR);
				//i++;
			}

			if (bird.Modificatore.bomb){
				hasbomb = true;
				bird.Modificatore.bomb = false;
				bird.Modificatore.check= false;
			}
			else if(bird.Modificatore.key){
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

		// // Aspetta un certo periodo di tempo prima di rimuovere l'uccello
		// timer.Start();
		// // Aspetta che il timer scada prima di rimuovere l'uccello
		// await ToSignal(timer, "timeout");
		// timer.QueueFree();
		// timer=null;


		// for(int z=0; z<LevelInfo.currentMaxSpots; z++){  //rimuove tutti gli uccelli dal gioco
		// 	BirdClick bird= tofree[z];
		// 	bird.node.QueueFree();
		// 	bird.node = null;
		// }
		//c'è il garbage collector
		//tofree = null; 
		// Sincronizza il conteggio degli slot occupati
		branch.occupiedSlots = branch.stackBirdOn.Count;

		// Riproduco suono fly away
		branch.GetNode<AudioStreamPlayer2D>("AudioStreamFlyAway2D").Play();

		if (LevelStruct.checkWin()) {//forse da cambiare
				GD.Print("Hai vinto!");
				CreateLevel.endLevel(false);
				// Puoi anche usare questo tempo nel calcolo dei punti esperienza
		}

		if(hasbomb || hasclock || haskey){
			removemalus(hasbomb, haskey, hasclock);
		}
	}

	public int sameBirdsCount(BranchClick branch) {
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

	public bool branchCompleted(BranchClick branch)
	{			
			// Controlla che il numero di uccelli nel ramo sia uguale al numero massimo consentito
			if (branch.stackBirdOn.Count != LevelInfo.currentMaxSpots)
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

					if(bird.isMoving){
								GD.Print("si sta muovendo", bird);
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

	public void setanimations(){
		if(!Modificatore.check){
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
		}else{
			if(Modificatore.clock){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("clock");
			}
			if(Modificatore.sleep){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("sleep");
			}
			if(Modificatore.key){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("key");
			}
			if(Modificatore.bomb){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("bomb");
			}
			if(Modificatore.cage){
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("caged");
			}
		}
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
