using Godot;
using System;
using Core;
using GodotInterface;
using System.Collections.Generic;
using Level;
using BombCounter;



public partial class BranchClick : Area2D {
	public Stack<BirdClick> stackBirdOn;
	public int occupiedSlots;

	public Vector2[] branchSlotsPos;

	public float startingPointX = 0;
	public int signBranch = 0;

	public override void _Ready() {
		setStartPoint();
		setSlotPos();

		stackBirdOn = new Stack<BirdClick>();
		occupiedSlots = 0;
		LevelStruct.AddBranch(this);
	}

	public void setStartPoint() {
		if (GlobalPosition.X < 0) {
			signBranch = 1;
			startingPointX = BranchPosInfo.StartLeft;
		}
		else {
			signBranch = -1;
			startingPointX = BranchPosInfo.StartRight;
		}
	}

	public void setSlotPos() {
		branchSlotsPos = new Vector2[LevelInfo.currentMaxSpots];
		for (int i = 0; i < LevelInfo.currentMaxSpots; i++) {
			float x = startingPointX + signBranch * (i + 1) * BranchPosInfo.Ratio;
			branchSlotsPos[i] = new Vector2(x, GlobalPosition.Y + BranchPosInfo.Offset(x, signBranch));
		}
	}

	public void _OnInputEvent(Node viewport, InputEvent @event, int shapeIdx) {
		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left) {
			if(LevelStruct.birdSelected != null) {
				LevelStruct.branchSelected = this;

				BirdClick possiblePick;

				LevelStruct.birdSelected.branchLink.stackBirdOn.TryPeek(out possiblePick);
				//GD.Print(LevelStruct.birdSelected.sameBirdsCount(this));

				GD.Print("stormo da: ", possiblePick.sameBirdsCount(possiblePick.branchLink));
				GD.Print("Possible Pick: ", possiblePick);

				int flockAmount = possiblePick.sameBirdsCount(possiblePick.branchLink);

				// Se gli spazi liberi del ramo sono >= del numero di uccelli dello stormo ==>
				if ( LevelInfo.currentMaxSpots - occupiedSlots >= flockAmount &&
					//occupiedSlots < LevelStruct.currentMaxSpots &&
					!object.Equals(LevelStruct.birdSelected.branchLink, this) &&
					object.Equals(LevelStruct.birdSelected, possiblePick)) {
					
						BranchClick srcBranch = possiblePick.branchLink;
						BirdClick[] flock = new BirdClick[flockAmount];	

						// Eseguo il trasferimento di flockAmount-uccelli da un ramo all'altro :)
						for(int i=0;i<flockAmount;i++) {
							flock[i] = srcBranch.stackBirdOn.Pop();
							srcBranch.occupiedSlots--;
						}

						// Gli uccelli viaggiano dal primo all'ultimo o dall'ultimo al primo in base a origine e destinazione del branch
						int start = 0, end = flockAmount, iter = 1;
						if(srcBranch.signBranch == this.signBranch) {
							start = flockAmount-1;
							end = -1;
							iter = -1;
						}

						// 2 CASI:
						// i=0, i!=flockAmount, i+=1
						// i=flockAmount-1, i!=-1, i+=-1

						for(int i=start; i!=end; i+=iter) {
							if(flock[i] != null) {
								flock[i].branchLink = this;
								this.stackBirdOn.Push(flock[i]);
								flock[i].StartMovement(branchSlotsPos[occupiedSlots]);
								this.occupiedSlots++;
							}
							else GD.Print("trovato uccello null!");
						}

						//LevelStruct.birdSelected.deselectBird();
						LevelStruct.birdSelected = null;
						if(LevelStruct.bombInGame)
							BombHandler.UpdateCounter();
						CreateLevel.IncrementMoves();
				}
				else {
					LevelStruct.birdSelected.deselectBird();
					LevelStruct.birdSelected = null;
					GD.Print("Qualcosa non gli è piaciuto");
				}
			}
			else {
				GD.Print("nessun uccello selezionato");
			}
		}
	}

	public void _OnMouseEntered() {
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("selected");
	}

	public void _OnMouseExited() {
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("default");
	}

	public Godot.Collections.Dictionary<string, Variant> Save()
{
	return new Godot.Collections.Dictionary<string, Variant>()
	{
		{ "Filename", SceneFilePath }
	};
}

}
