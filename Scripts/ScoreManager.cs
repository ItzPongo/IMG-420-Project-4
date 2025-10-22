using Godot;

public partial class ScoreManager : Node
{
	private int _score = 0;
	private Label _scoreLabel;
	
	public override void _Ready()
	{
		// Find the score label by searching the entire tree
		_scoreLabel = GetTree().Root.FindChild("Score", true, false) as Label;
		if (_scoreLabel == null)
		{
			GD.PrintErr("Could not find Score label!");
		}
		UpdateScoreDisplay();
	}
	
	public void AddScore(int points)
	{
		_score += points;
		UpdateScoreDisplay();
	}
	
	private void UpdateScoreDisplay()
	{
		if (_scoreLabel != null)
		{
			_scoreLabel.Text = "Score: " + _score;
		}
	}
	
	public int GetScore()
	{
		return _score;
	}
}
