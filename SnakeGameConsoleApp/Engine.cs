using SnakeGame.Core;
using SnakeGame.Core.GameObjects;

namespace SnakeGame.ConsoleApp;

public class Engine
{
	private Wall wall;
	private Snake snake;
	private int sleepTime;
	private Point[] pointsOfDirections;
	private Direction direction;

	public Engine(Wall wall, Snake snake)
	{
		this.wall = wall;
		this.snake = snake;
		sleepTime = 200;
		pointsOfDirections = new Point[4];
		direction = Direction.Right;
	}

	private void CreateDirections()
	{
		pointsOfDirections[0] = new Point(-1, 0); //Left
		pointsOfDirections[1] = new Point(1, 0); //Right
		pointsOfDirections[2] = new Point(0, -1); //Up
		pointsOfDirections[3] = new Point(0, 1); //Down
	}

	private void GetNextDirection()
	{
		ConsoleKeyInfo userInput = Console.ReadKey();

		if (userInput.Key == ConsoleKey.LeftArrow)
		{
			if (direction != Direction.Right)
				direction = Direction.Left;
		}
		else if (userInput.Key == ConsoleKey.RightArrow)
		{
			if (direction != Direction.Left)
				direction = Direction.Right;
		}
		else if (userInput.Key == ConsoleKey.UpArrow)
		{
			if (direction != Direction.Down)
				direction = Direction.Up;
		}
		else if (userInput.Key == ConsoleKey.DownArrow)
		{
			if (direction != Direction.Up)
				direction = Direction.Down;
		}

		Console.CursorVisible = false;

		bool isMoving = snake.IsMoving(pointsOfDirections[(int)direction]);
		if (!isMoving)
			AskUserForRestart();
	}

	private void AskUserForRestart()
	{
		int leftX = wall.LeftX + 1;
		int topY = 3;

		Console.SetCursorPosition(leftX, topY);
		Console.Write("Would you like to continue? y/n");

		string? input = Console.ReadLine();
		if (input == "y")
		{
			Console.Clear();
			Program.Main();
		}
		else
			StopGame();
	}

	private void StopGame()
	{
		Console.SetCursorPosition(20, 10);
		Console.Write("Game Over!");
		Environment.Exit(0);
	}

	public void Run()
	{
		CreateDirections();

		while (true)
		{
			if (Console.KeyAvailable)
				GetNextDirection();

			bool isMoving = snake.IsMoving(pointsOfDirections[(int)direction]);
			if (!isMoving)
				AskUserForRestart();

			Thread.Sleep(sleepTime);
		}
	}
}
