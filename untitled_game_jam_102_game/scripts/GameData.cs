using Godot;
using System;
using System.Collections.Generic;


public partial class GameData : Node
{
	public static GameData Instance { get; private set; }

	// Initial Volume Slider Values
	public float MasterVolume { get; set; } = 0.75f;
	public float MusicVolume { get; set; } = 0.5f;
	public float SFXVolume { get; set; } = 0.65f;

	// Game Variables

	// General Metadata
	// Is a run in progress
	public bool IsGameInProgress { get; set; } = false;
	// Can the player pause the game
	public bool IsGamePausable { get; set; } = false;
	// Is the game paused
	public bool IsGamePaused { get; set; } = false;
	// Has the player won the game
	public bool IsGameWon { get; set; } = false;
	// Has the player lost the game
	public bool IsGameLost { get; set; } = false;
	// Is the shop open
	public bool IsShopOpen { get; set; } = false;

	// Shopkeeper Lore Text
	public List<string> ShopLoreText = new List<string>();
	// Shopkeeper Advice Text
	public List<string> ShopAdviceText = new List<string>();
	

	// Battle Metadata
	// Has the player won the battle
	public bool IsBattleWon { get; set; } = false;
	// Has the player lost the battle
	public bool IsBattleLost { get; set; } = false;

	// Worlds Metadata
	// Has the MainMenuWorld been killed
	public bool IsMainMenuWorldDead { get; set; } = false;
	// Has the GameMapWorld been killed
	public bool IsGameMapWorldDead { get; set; } = false;
	//
	public bool IsMainMenuWorldAlive { get; set; } = true;

	// Player Metadata
	// Amount of Runes the Player has currently
	public int CurrentPlayerRunes { get; set; } = 0;
	// Amount of Mana Cores the Player has currently
	public int CurrentManaCores { get; set; } = 0;

	// Dictionary of all Units the player has
	//public IDictionary<string, Unit> UnitList { get; set; } = new Dictionary<string, Unit>();
	//public Godot.Collections.Dictionary UnitList = new Godot.Collections.Dictionary();
	public Dictionary<string, Unit> UnitList = new Dictionary<string, Unit>();

	// Amount of enemy units defeated
	// Amount of bosses defeated
	// Amount of runes collected
	// Amount of player units lost

	public void ResetAllGameVariables()
	{
		// General Metadata
		IsGameInProgress = false;
		IsGamePausable = false;
		IsGamePaused = false;
		IsGameWon = false;
		IsGameLost = false;
		IsShopOpen = false;

		// Battle Metadata
		IsBattleWon = false;
		IsBattleLost = false;

		// Worlds Metadata
		IsMainMenuWorldDead = false;
		IsGameMapWorldDead = false;

		// Player Metadata
		CurrentPlayerRunes = 0;
		CurrentManaCores = 0;

	}


    public override void _Ready()
    {
        Instance = this;
		
    }
}
