using Godot;
using System;
using System.Collections.Generic;


public partial class GameData : Node
{
	public static GameData Instance { get; private set; }

	// Initial Volume Slider Values
	public float MasterVolume { get; set; } = 0.75f;
	public float MusicVolume { get; set; } = 0.0f;
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


	// Shop Metadata
	// Shopkeeper Greeting Text
	public List<string> ShopkeeperGreetingText = new List<string>();
	// Shopkeeper Lore Text
	public List<string> ShopkeeperLoreText = new List<string>();
	// Shopkeeper Advice Text
	public List<string> ShopkeeperAdviceText = new List<string>();
	

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
	//public bool IsMainMenuWorldAlive { get; set; } = true;

	// Player Metadata
	// Amount of Runes the Player has currently
	public int CurrentPlayerRunes { get; set; } = 0;
	// Amount of Mana Cores the Player has currently
	public int CurrentManaCores { get; set; } = 0;
	// Amount of Morph Slime the Player has currently
	public int CurrentMorphSlime { get; set; } = 0;

	// Dictionary of all Units the player has
	//public IDictionary<string, Unit> UnitList { get; set; } = new Dictionary<string, Unit>();
	//public Godot.Collections.Dictionary UnitList = new Godot.Collections.Dictionary();
	public Dictionary<string, Unit> UnitList = new Dictionary<string, Unit>();

	// Amount of enemy units defeated
	// Amount of bosses defeated
	// Amount of runes collected
	// Amount of player units lost

	// Methods
	// Method to reset all game variables to default values
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
		CurrentMorphSlime = 0;

	}

	// Method to initialize the Shopkeeper text lists
	public void InitializeShopkeeperText()
	{
		// Greeting text options
		ShopkeeperGreetingText.Add
		(
			"Welcome to my shop stranger. I sell only the finest wares."
		);

		ShopkeeperGreetingText.Add
		(
			"Greetings stranger. Looking to spend your runes?"
		);

		ShopkeeperGreetingText.Add
		(
			"Hello stranger. Please feel free to browse my merchandise."
		);

		// Advice text options
		ShopkeeperAdviceText.Add
		(
			"Fighter class minions are general purpose units. They are good at taking and dealing damage. You should prioritize placing them in the first row to form a strong frontline."
		);

		ShopkeeperAdviceText.Add
		(
			"Rogue class minions are agile raid units. They trade defense for improved initiative, critical hit chance, and evasion. Use them to take out high priority targets or to perform devastating first strikes."
		);

		ShopkeeperAdviceText.Add
		(
			"Caster class minions are powerful support units. They have access to a wide array of offensive and defensive spells. Place them in the last row to keep them safe from enemy melee attacks."
		);

		ShopkeeperAdviceText.Add
		(
			"Fusion class minions are formed by combining two Tier III minions. Their abilities depend on the Soul type of the minions that were combined. They can use Tier V spells and skills to obliterate enemies!"
		);

		ShopkeeperAdviceText.Add
		(
			"Your minions have the ability to shapeshift! A minion can change to another minion of the same Soul type and Tier. For example, a Tier II Nature minion can change to any Tier II Nature minion regardless of Class type."
		);

		ShopkeeperAdviceText.Add
		(
			"After you win a battle, all of the dead minions in your party will be revived. This only applies if Minion Permadeath is disabled."
		);

		// Lore text options
		ShopkeeperLoreText.Add
		(
			"Nature lore"
		);

		ShopkeeperLoreText.Add
		(
			"Holy lore"
		);

		ShopkeeperLoreText.Add
		(
			"Undead lore"
		);

		ShopkeeperLoreText.Add
		(
			"Demon lore"
		);

		ShopkeeperLoreText.Add
		(
			"Eldritch lore"
		);
	}



    public override void _Ready()
    {
        Instance = this;
		
    }
}
