using Godot;
using System;

public partial class Bucket : Area2D
{
	// these exports should be set on the LEVEL
	[Export]
	public int BucketScore = 0;

	[Export]
	public PlinkoLevel PlinkoLevelNode;


	private Label scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// this is accessing by unique name, it's a very robust method for UI design
		scoreLabel = GetNode<Label>("%ScoreLabel");

		scoreLabel.Text = $"{BucketScore}";

		BodyEntered += Bucket_BodyEntered;

		GetNode<StaticBody2D>("Body").CollisionLayer = 2;

		GetNode("Body").GetNode<CollisionShape2D>("BucketCover").Disabled = false;
		GetNode("Body").GetNode<CollisionShape2D>("BucketCover").Visible = false;
	}

	private void Bucket_BodyEntered(Node2D body)
	{
		// we ONLY want to increase the score when a player disk falls in the bucket
		if (body.IsInGroup("player"))
		{
			PlinkoLevelNode.IncreaseScore(BucketScore);

			GetNode<StaticBody2D>("Body").CollisionLayer = 1;
			GetNode("Body").GetNode<CollisionShape2D>("BucketCover").Visible = true;

			Player playerDisk = (Player)body;
			playerDisk.Dead = true;
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
