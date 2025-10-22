using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public float Speed = 60f;
	
	[Export]
	public NodePath TargetPath;
	
	[Export]
	public PackedScene DeathParticles;
	
	private NavigationAgent2D _navAgent;
	private Node2D _target;
	private AnimatedSprite2D _anim;
	
	public override void _Ready()
	{
		_navAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		if (TargetPath != null)
		{
			_target = GetNode<Node2D>(TargetPath);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_target == null || _navAgent == null)
			return;
		
		_navAgent.TargetPosition = _target.GlobalPosition;
		Vector2 nextPoint = _navAgent.GetNextPathPosition();
		Vector2 direction = (nextPoint - GlobalPosition).Normalized();
		
		Velocity = direction * Speed;
		MoveAndSlide();
		
		// Check for collision with player
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			if (collision.GetCollider() is Player player)
			{
				// Spawn death particles at player position
				if (DeathParticles != null)
				{
					var particles = DeathParticles.Instantiate() as GpuParticles2D;
					if (particles != null)
					{
						particles.GlobalPosition = player.GlobalPosition;
						GetParent().AddChild(particles);
						particles.Emitting = true;
					}
				}
				// Delete the player and clear the target reference
				player.QueueFree();
				_target = null;
				return; // Stop processing this frame
			}
		}
		
		if (_anim != null && direction.Length() > 0.01f)
		{
			if (Mathf.Abs(direction.Y) > Mathf.Abs(direction.X))
			{
				if (direction.Y < 0)
				{
					_anim.Play("Up");
				}
				else
				{
					_anim.Play("Down");
				}
			}
			else
			{
				_anim.Play("Sideways");
				_anim.FlipH = direction.X < 0;
			}
		}
	}
}
