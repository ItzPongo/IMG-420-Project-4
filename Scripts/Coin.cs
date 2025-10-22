using Godot;

public partial class Coin : Area2D
{
	private AnimatedSprite2D _anim;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		
		// Start the coin animation
		_anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (_anim != null)
		{
			_anim.Play("default");
		}
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			// Find the ScoreManager in the scene and add points
			var scoreManager = GetTree().Root.GetNode<ScoreManager>("Main/ScoreManager");
			if (scoreManager != null)
			{
				scoreManager.AddScore(2);
			}
			
			// Delete the coin
			QueueFree();
		}
	}
}
