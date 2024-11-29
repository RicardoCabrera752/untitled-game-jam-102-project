using Godot;
using System;

public partial class CustomSignals : Node
{
	// Signals
	// Emitted when the Player starts a new game, sent to kill MainMenuWorld
	[Signal] 
	public delegate void KillMainMenuWorldEventHandler();
	// Emitted when the GameMapWorld needs to be killed
	[Signal] 
	public delegate void KillGameMapWorldEventHandler();
	// Emitted when the GameMapWorld is loaded for a new game
	[Signal] 
	public delegate void FirstTimeLoadGameMapWorldEventHandler();
	// Emitted when the Player chooses to abandon the current game
	//[Signal] public delegate void AbandonGameEventHandler();

	// Emitted to load the MainMenuWorldMap
	[Signal] public delegate void LoadMainMenuWorldEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
