using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 200f;
	
	private AnimatedSprite2D _anim;
	
	public override void _Ready()
	{
		_anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = direction * Speed;
		MoveAndSlide();
		
		if (_anim != null)
		{
			if (direction.Length() > 0.01f)
			{
				if (Mathf.Abs(direction.Y) > Mathf.Abs(direction.X))
				{
					if (direction.Y < 0)
					{
						_anim.Play("WalkUp");
					}
					else
					{
						_anim.Play("WalkDown");
					}
				}
				else
				{
					_anim.Play("WalkSideways");
					_anim.FlipH = direction.X < 0;
				}
			}
			else
			{
				_anim.Stop();
			}
		}
	}
}
