using Godot;
using System;

public partial class UIManager : Control
{
	// Signals
	// Emitted when the player wants to quit the game
	[Signal] 
	public delegate void ExitGameEventHandler();

	// Emitted when the player starts the game
	[Signal] 
	public delegate void StartGameEventHandler(string clanName);

	// Emitted when the player chooses to abandon the current game
	[Signal] 
	public delegate void AbandonGameEventHandler();

	// Emitted when the player changes the Master Volume
	[Signal] 
	public delegate void ChangeMasterVolumeEventHandler(float value);

	// Emitted when the player changes the Music Volume
	[Signal] 
	public delegate void ChangeMusicVolumeEventHandler(float value);

	// Emitted when the player changes the SFX Volume
	[Signal] 
	public delegate void ChangeSoundEffectsVolumeEventHandler(float value);

	// Properties
	public bool ShowMainMenuScreen = true;
	public bool ShowStartScreen = false;
	public bool ShowControlsScreen = false;
	public bool ShowOptionsScreen = false;
	public bool ShowCreditsScreen = false;


	// Access to the GameData variables
	private GameData _gameData;

	// Access to the CustomSignals signals
	private CustomSignals _customSignals;

	// Methods

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_gameData = GetTree().Root.GetNode<GameData>("GameData");
		_customSignals = GetTree().Root.GetNode<CustomSignals>("CustomSignals");

		// Safety check to display correct UI
		ShowOnlyMainMenuUI();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check for Player Inputs
		// Pause Game
		if (Input.IsActionJustPressed("pause_game"))
        {
            if (_gameData.IsGameInProgress && !_gameData.IsGameLost && !_gameData.IsGameWon && !_gameData.IsGamePaused)
			{
				GetTree().Paused = true;

				GetNode<CanvasLayer>("PauseMenuUI").Show();
				_gameData.IsGamePaused = true;
		
				GetNode<Button>("MenuBarUI/PauseGameButton").Disabled = true;

				GetNode<CanvasLayer>("ControlsUI").Hide();
				GetNode<CanvasLayer>("OptionsUI").Hide();
				GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
				//GetNode<Control>("PauseMenuUI/AbandonGameUI").Hide();
				
			}else 
			{
				GetTree().Paused = false;

				GetNode<CanvasLayer>("PauseMenuUI").Hide();
				_gameData.IsGamePaused = false;
		
				GetNode<Button>("MenuBarUI/PauseGameButton").Disabled = false;

				GetNode<CanvasLayer>("ControlsUI").Hide();
				GetNode<CanvasLayer>("OptionsUI").Hide();
				GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
				//GetNode<Control>("PauseMenuUI/AbandonGameUI").Hide();
			}

			
        }

		// Open Shop
		if (Input.IsActionJustPressed("open_shop"))
		{
			if (_gameData.IsGameInProgress && !_gameData.IsGameLost && !_gameData.IsGameWon && !_gameData.IsGamePaused && !_gameData.IsShopOpen)
			{
				GetNode<CanvasLayer>("ShopUI").Show();
				_gameData.IsShopOpen = true;

				GetNode<CanvasLayer>("InventoryUI").Hide();

				//GetNode<Button>("MenuBarUI/OpenShopButton").Disabled = true;
			}else if (_gameData.IsGameInProgress && !_gameData.IsGameLost && !_gameData.IsGameWon && !_gameData.IsGamePaused && _gameData.IsShopOpen)
			{
				GetNode<CanvasLayer>("ShopUI").Hide();
				_gameData.IsShopOpen = false;
			}
		}

	}

	// Method to hide all UIs except the MainMenuUI
	private void ShowOnlyMainMenuUI()
	{
		// Main Menu
		GetNode<CanvasLayer>("MainMenuUI").Show();
		GetNode<CanvasLayer>("StartUI").Hide();
		GetNode<CanvasLayer>("ControlsUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Hide();
		GetNode<CanvasLayer>("BackButtonUI").Hide();

		// In-Game Menus
		GetNode<CanvasLayer>("PauseMenuUI").Hide();
		
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
		GetNode<CanvasLayer>("InventoryUI").Hide();
		GetNode<CanvasLayer>("ShopUI").Hide();
		GetNode<CanvasLayer>("GameOverUI").Hide();
		GetNode<CanvasLayer>("GameWinUI").Hide();
		GetNode<CanvasLayer>("MenuBarUI").Hide();
	}

	// Method to hide all UIs except for MenuBarUI
	private void ShowOnlyMenuBarUI()
	{
		// Main Menu
		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("StartUI").Hide();
		GetNode<CanvasLayer>("ControlsUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Hide();
		GetNode<CanvasLayer>("BackButtonUI").Hide();

		// In-Game Menus
		GetNode<CanvasLayer>("MenuBarUI").Show();
		GetNode<CanvasLayer>("PauseMenuUI").Hide();
		
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
		GetNode<CanvasLayer>("InventoryUI").Hide();
		GetNode<CanvasLayer>("ShopUI").Hide();
		GetNode<CanvasLayer>("GameOverUI").Hide();
		GetNode<CanvasLayer>("GameWinUI").Hide();

	}

	// Method to hide all UIs except for for MenuBarUI and ShopUI

	// Handle Start Button being pressed
	private void OnStartButtonPressed()
	{
		GD.Print("Start Button Pressed");
		GetNode<CanvasLayer>("BackButtonUI").Show();
		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("StartUI").Show();
		ShowStartScreen = true;
		GetNode<AudioStreamPlayer>("../AudioManager/ButtonSoundEffect").Play();
	}

	// Handle Controls Button being pressed
	private void OnControlsButtonPressed()
	{
		GD.Print("Controls Button Pressed");
		GetNode<CanvasLayer>("BackButtonUI").Show();
		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("ControlsUI").Show();

		GetNode<ColorRect>("ControlsUI/ControlsBackground").Hide();
		GetNode<Button>("ControlsUI/CloseControls").Hide();

		ShowControlsScreen = true;
		GetNode<AudioStreamPlayer>("../AudioManager/ButtonSoundEffect").Play();
	}

	// Handle Options Button being pressed
	private void OnOptionsButtonPressed()
	{
		GD.Print("Options Button Pressed");
		GetNode<CanvasLayer>("BackButtonUI").Show();
		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("OptionsUI").Show();

		GetNode<Button>("OptionsUI/CloseOptions").Hide();

		ShowOptionsScreen = true;
		GetNode<AudioStreamPlayer>("../AudioManager/ButtonSoundEffect").Play();
	}

	// Handle Credits Button being pressed
	private void OnCreditsButtonPressed()
	{
		GD.Print("Credits Button Pressed");
		GetNode<CanvasLayer>("BackButtonUI").Show();
		GetNode<CanvasLayer>("MainMenuUI").Hide();
		GetNode<CanvasLayer>("CreditsUI").Show();
		ShowCreditsScreen = true;
		GetNode<AudioStreamPlayer>("../AudioManager/ButtonSoundEffect").Play();
	}

	// Handle Exit Button being pressed
	private void OnExitButtonPressed()
	{
		EmitSignal(SignalName.ExitGame);
	}

	// Handle Back Button being pressed
	private void OnBackButtonPressed()
	{
		GD.Print("Back Button Pressed");
		GetNode<CanvasLayer>("BackButtonUI").Hide();

		if(ShowStartScreen)
		{
			GetNode<CanvasLayer>("StartUI").Hide();
			ShowStartScreen = false;
		}

		if(ShowControlsScreen)
		{
			GetNode<CanvasLayer>("ControlsUI").Hide();
			ShowControlsScreen = false;
		}

		if(ShowOptionsScreen)
		{
			GetNode<CanvasLayer>("OptionsUI").Hide();
			ShowOptionsScreen = false;
		}

		if(ShowCreditsScreen)
		{
			GetNode<CanvasLayer>("CreditsUI").Hide();
			ShowCreditsScreen = false;
		}


		GetNode<CanvasLayer>("MainMenuUI").Show();
		GetNode<AudioStreamPlayer>("../AudioManager/ButtonSoundEffect").Play();
	}

	// Handle Master Volume Slider Changes
	private void OnMasterVolumeSliderValueChanged(float value)
	{
		//GD.Print(value);
		EmitSignal(SignalName.ChangeMasterVolume, value);
	}

	// Handle Music Volume Slider Changes
	private void OnMusicVolumeSliderValueChanged(float value)
	{
		EmitSignal(SignalName.ChangeMusicVolume, value);
	}

	// Handle SFX Volume Slider Changes
	private void OnSoundEffectsVolumeSliderValueChanged(float value)
	{
		EmitSignal(SignalName.ChangeSoundEffectsVolume, value);
	}

	// Handle Select Clan button being pressed
	private void OnSelectClanButtonPressed(string clanName)
	{
		GetNode<CanvasLayer>("StartUI").Hide();
		ShowStartScreen = false;
		GetNode<CanvasLayer>("BackButtonUI").Hide();

		GD.Print("Selected Clan: " + clanName);
		EmitSignal(SignalName.StartGame, clanName);

		ShowOnlyMenuBarUI();
		ResetPlayerRunesAndManaCoresText();

		_gameData.IsGameInProgress = true;

		_gameData.UnitList.Add("A", new Unit());

		GD.Print("Starting Units: ");
		GD.Print(_gameData.UnitList["A"].UnitName);
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////////////
	// In-Game UI

	// Reset Player Runes and Mana Cores
	private void ResetPlayerRunesAndManaCoresText()
	{
		// Reset Runes
		UpdatePlayerRunesText(0);

		// Reset Mana Cores
		UpdatePlayerManaCoresText(0);
	}

	// Method to update Player Runes
	private void UpdatePlayerRunesText(int runes)
	{
		_gameData.CurrentPlayerRunes = runes;
		GetNode<Label>("MenuBarUI/PlayerRunesCount").Text = runes.ToString();


	}

	// Method to update Player Mana Cores
	private void UpdatePlayerManaCoresText(int manaCores)
	{
		_gameData.CurrentManaCores = manaCores;
		//string manaCoresText = GetNode<Label>("MenuBarUI/PlayerRunesCount").Text.Substring(0,1);
		GetNode<Label>("MenuBarUI/PlayerManaCoresCount").Text = manaCores.ToString() + "/5";

	}

	// Pause Menu Buttons
	// Handle Pause Menu Button being pressed
	private void OnPauseGameButtonPressed()
	{
		GetTree().Paused = true;

		GetNode<CanvasLayer>("PauseMenuUI").Show();
		_gameData.IsGamePaused = true;
		
		GetNode<Button>("MenuBarUI/PauseGameButton").Disabled = true;

		
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
	}

	// Handle Resume Game Button being pressed
	private void OnResumeGameButtonPressed()
	{
		GetTree().Paused = false;

		GetNode<CanvasLayer>("PauseMenuUI").Hide();
		_gameData.IsGamePaused = false;
		
		GetNode<Button>("MenuBarUI/PauseGameButton").Disabled = false;

		
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
	}

	// Handle Pause Menu Controls Button being pressed
	private void OnPauseMenuControlsButtonPressed()
	{
		GetNode<CanvasLayer>("ControlsUI").Show();
		
		GetNode<ColorRect>("ControlsUI/ControlsBackground").Show();
		GetNode<Button>("ControlsUI/CloseControls").Show();

		GetNode<CanvasLayer>("PauseMenuUI").Hide();
	}

	// Handle Pause Menu Close Controls Button being pressed
	private void OnCloseControlsButtonPressed()
	{
		GetNode<CanvasLayer>("ControlsUI").Hide();
		GetNode<CanvasLayer>("PauseMenuUI").Show();
	}

	// Handle Pause Menu Options Button being pressed
	private void OnPauseMenuOptionsButtonPressed()
	{
		GetNode<CanvasLayer>("OptionsUI").Show();
		
		GetNode<Button>("OptionsUI/CloseOptions").Show();

		GetNode<CanvasLayer>("PauseMenuUI").Hide();
	}

	// Handle Pause Menu Close Options Button being pressed
	private void OnCloseOptionsPressed()
	{
		GetNode<CanvasLayer>("OptionsUI").Hide();
		GetNode<CanvasLayer>("PauseMenuUI").Show();
	}

	// Handle Abandon Game Button being pressed
	private void OnAbandonGameButtonPressed()
	{
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Show();
	}

	// Handle Confirm Abandon Game Button being pressed
	private void OnConfirmAbandonGameButtonPressed()
	{
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
		GetNode<Button>("MenuBarUI/PauseGameButton").Disabled = false;
		EmitSignal(SignalName.AbandonGame);

		ShowOnlyMainMenuUI();
	}

	// Handle Cancel Abandon Game Button being pressed
	private void OnCancelAbandonGameButtonPressed()
	{
		GetNode<CanvasLayer>("PauseMenuUI/AbandonGameUI").Hide();
	}


	// Shop Menu Buttons
	// Handle Shop Menu Button being pressed
	private void OnOpenShopButtonPressed()
	{
		if (_gameData.IsGameInProgress && !_gameData.IsGameLost && !_gameData.IsGameWon && !_gameData.IsGamePaused && !_gameData.IsShopOpen)
		{
			GetNode<CanvasLayer>("ShopUI").Show();
			_gameData.IsShopOpen = true;

			GetNode<CanvasLayer>("InventoryUI").Hide();

			//GetNode<Button>("MenuBarUI/OpenShopButton").Disabled = true;
		}else if (_gameData.IsGameInProgress && !_gameData.IsGameLost && !_gameData.IsGameWon && !_gameData.IsGamePaused && _gameData.IsShopOpen)
		{
			GetNode<CanvasLayer>("ShopUI").Hide();
			_gameData.IsShopOpen = false;
		}
	}
}
