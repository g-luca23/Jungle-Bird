using Godot;
using System;
using Core;
using GodotInterface;
using System.Collections.Generic;
using Level;
using BombCounter;



public partial class DummyBranch {
	public Stack<DummyBird> stackBirdOn = new Stack<DummyBird>();
	public int occupiedSlots;

	public int currentMaxspots = 0; //variabile di levelstruct messa qui per i test

	public List<DummyBird> malusbirds = new List<DummyBird>();

	public Vector2[] branchSlotsPos;

	public float startingPointX = 0;
	public int signBranch = 0;

	public void addMalusBird(DummyBird bird){
		malusbirds.Add(bird);
	}

	public DummyBird delmalusbird(DummyBird bird){//dovrebbe andar bene(spero), elimina l'uccello
		malusbirds.Remove(bird);
		return(bird);
	}

	// public void setStartPoint() {
	// 	if (GlobalPosition.X < 0) {
	// 		signBranch = 1;
	// 		startingPointX = BranchPosInfo.StartLeft;
	// 	}
	// 	else {
	// 		signBranch = -1;
	// 		startingPointX = BranchPosInfo.StartRight;
	// 	}
	// }

	// public void setSlotPos() {
	// 	branchSlotsPos = new Vector2[LevelInfo.currentMaxSpots];
	// 	for (int i = 0; i < LevelInfo.currentMaxSpots; i++) {
	// 		float x = startingPointX + signBranch * (i + 1) * BranchPosInfo.Ratio;
	// 		branchSlotsPos[i] = new Vector2(x, GlobalPosition.Y + BranchPosInfo.Offset(x, signBranch));
	// 	}
	// }

// 	public Godot.Collections.Dictionary<string, Variant> Save()
// {
// 	return new Godot.Collections.Dictionary<string, Variant>()
// 	{
// 		{ "Filename", SceneFilePath }
// 	};
// }

}
