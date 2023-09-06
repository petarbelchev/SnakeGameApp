namespace SnakeGame.Core.GameObjects;

public class Snake
{
	private const char snakeSymbol = '\u25CF';
	private Wall wall;
	private Queue<Point> snakeElements;
	private Food[] food;
	private int foodIndex;
	private int nextLeftX;
	private int nextTopY;

	public Snake(Wall wall)
	{
		this.wall = wall;
		snakeElements = new Queue<Point>();
		food = new Food[3];
		GetFoods();
		foodIndex = RandomFoodIndex;
		food[foodIndex].SetRandomPosition(snakeElements);
		CreateSnake();
	}

	private int RandomFoodIndex => new Random().Next(0, food.Length);

	private void CreateSnake()
	{
		for (int topY = 1; topY <= 6; topY++)
			snakeElements.Enqueue(new Point(2, topY));
	}

	private void GetFoods()
	{
		food[0] = new FoodAsterisk(wall);
		food[1] = new FoodDollar(wall);
		food[2] = new FoodHash(wall);
	}

	private void GetNextPoint(Point direction, Point snakeHead)
	{
		nextLeftX = snakeHead.LeftX + direction.LeftX;
		nextTopY = snakeHead.TopY + direction.TopY;
	}

	public bool IsMoving(Point direction)
	{
		Point currentSnakeHead = snakeElements.Last();
		GetNextPoint(direction, currentSnakeHead);

		bool isPointOfSnake = snakeElements.Any(x => x.LeftX == nextLeftX && x.TopY == nextTopY);
		if (isPointOfSnake)
			return false;

		Point snakeNewHead = new Point(nextLeftX, nextTopY);
		if (wall.IsPointOfWall(snakeNewHead))
			return false;

		snakeElements.Enqueue(snakeNewHead);
		snakeNewHead.Draw(snakeSymbol);
		Point snakeTail = snakeElements.Dequeue();
		snakeTail.Draw(' ');

		if (food[foodIndex].IsFoodPoint(snakeNewHead))
			Eat(direction, snakeNewHead);

		return true;
	}

	private void Eat(Point direction, Point currentSnakeHead)
	{
		int length = food[foodIndex].FoodPoints;

		for (int i = 0; i < length; i++)
		{
			GetNextPoint(direction, currentSnakeHead);
			Point snakeNewHead = new Point(nextLeftX, nextTopY);
			snakeElements.Enqueue(snakeNewHead);
			snakeNewHead.Draw(snakeSymbol);
		}

		foodIndex = RandomFoodIndex;
		food[foodIndex].SetRandomPosition(snakeElements);

		Point snakeTail = snakeElements.Dequeue();
		snakeTail.Draw(' ');
	}
}
