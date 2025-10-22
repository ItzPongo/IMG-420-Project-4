using Godot;

public partial class CoinSpawner : Node2D
{
	[Export]
	public PackedScene CoinScene;
	
	[Export]
	public float SpawnInterval = 2.0f;
	
	private Timer _spawnTimer;
	private Vector2 _screenSize;
	
	public override void _Ready()
	{
		// Get the viewport size
		_screenSize = GetViewportRect().Size;
		
		// Create and configure timer
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.Timeout += SpawnCoin;
		AddChild(_spawnTimer);
		_spawnTimer.Start();
		
		// Spawn first coin immediately
		SpawnCoin();
	}
	
	private void SpawnCoin()
	{
		if (CoinScene == null)
			return;
		
		// Instantiate the coin
		var coin = CoinScene.Instantiate() as Area2D;
		if (coin != null)
		{
			// Random position within screen bounds (with 20 pixel margin)
			float randomX = GD.Randf() * (_screenSize.X - 40) + 20;
			float randomY = GD.Randf() * (_screenSize.Y - 40) + 20;
			
			coin.GlobalPosition = new Vector2(randomX, randomY);
			GetParent().CallDeferred("add_child", coin);
		}
	}
}
