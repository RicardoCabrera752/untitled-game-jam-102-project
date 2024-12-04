using Godot;
using System;

public partial class Main : Node
{
	// Signals

	// Exports

	// Properties
	// Audio Bus Indices
	public int MasterVolumeIndex = AudioServer.GetBusIndex("Master");
	public int MusicVolumeIndex = AudioServer.GetBusIndex("Music");
	public int SFXVolumeIndex = AudioServer.GetBusIndex("SFX");

	// Initial Volume Slider Values
	//public float MasterVolume = 1;
	//public float MusicVolume = 1;
	//public float SFXVolume = 1;

	// Access to the GameData variables
	private GameData _gameData;

	// Access to the CustomSignals signals
	private CustomSignals _customSignals;

	// Methods

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameData = GetTree().Root.GetNode<GameData>("GameData");
		//var gameData = GetTree().Root.GetNode<GameData>("GameData");
		//var MasterVolume = gameData.MasterVolume;
		//var MusicVolume = gameData.MusicVolume;
		//var SFXVolume = gameData.SFXVolume;
		_customSignals = GetTree().Root.GetNode<CustomSignals>("CustomSignals");

		var MasterVolume = _gameData.MasterVolume;
		var MusicVolume = _gameData.MusicVolume;
		var SFXVolume = _gameData.SFXVolume;

		// Initialize the Volume Slider Values
		GetNode<HSlider>("UIManager/OptionsUI/MasterVolumeSlider").Value = MasterVolume;
		GetNode<HSlider>("UIManager/OptionsUI/MusicVolumeSlider").Value = MusicVolume;
		GetNode<HSlider>("UIManager/OptionsUI/SoundEffectsVolumeSlider").Value = SFXVolume;

		// Initialize Shopkeeper Text
		_gameData.InitializeShopkeeperText();

		// Play the Main Menu Music
		GetNode<AudioStreamPlayer>("AudioManager/MainMenuMusic").Play();


		//GetNode<Node3D>("WorldManager").AddChild(mainMenuWorld);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Handle Game Exit
	private void OnExitGame()
	{
		GetTree().Quit();
	}

	// Handle Master Volume Slider Changes
	private void OnChangeMasterVolume(float value)
	{
		AudioServer.SetBusVolumeDb(MasterVolumeIndex, Mathf.LinearToDb(value));
		GetTree().Root.GetNode<GameData>("GameData").MasterVolume = value;
	}

	// Handle Music Volume Slider Changes
	private void OnChangeMusicVolume(float value)
	{
		AudioServer.SetBusVolumeDb(MusicVolumeIndex, Mathf.LinearToDb(value));
		GetTree().Root.GetNode<GameData>("GameData").MusicVolume = value;
	}

	// Handle SFX Volume Slider Changes
	private void OnChangeSoundEffectsVolume(float value)
	{
		AudioServer.SetBusVolumeDb(SFXVolumeIndex, Mathf.LinearToDb(value));
		GetTree().Root.GetNode<GameData>("GameData").SFXVolume = value;
	}

	// Handle Game Start
	private void OnStartGame(string clanName)
	{
		//GetNode<AudioStreamPlayer>("AudioManager/MainMenuMusic").Stop();
		
		//GetTree().ChangeSceneToFile("res://game_world.tscn");

		_customSignals.EmitSignal(nameof(CustomSignals.KillMainMenuWorld));
		GD.Print("Starting Run with Clan: " + clanName);
		_customSignals.EmitSignal(nameof(CustomSignals.FirstTimeLoadGameMapWorld));

	}

	// Handle Abandon Current Game
	private void OnAbandonGame()
	{
		// Kill GameMapWorld
		_customSignals.EmitSignal(nameof(CustomSignals.KillGameMapWorld));
		GD.Print("Game Abandoned, Returning to Main Menu");

		// Load MainMenuWorld again
		_customSignals.EmitSignal(nameof(CustomSignals.LoadMainMenuWorld));

		// Reset all Game Variables
		_gameData.ResetAllGameVariables();

		// Play the Main Menu Music
		GetNode<AudioStreamPlayer>("AudioManager/MainMenuMusic").Play();

		GetTree().Paused = false;
	}


	private void KillMenu()
	{
		_customSignals.EmitSignal(nameof(CustomSignals.KillMainMenuWorld));
	}

	private void LoadMenu()
	{
		_customSignals.EmitSignal(nameof(CustomSignals.LoadMainMenuWorld));
	}
}
