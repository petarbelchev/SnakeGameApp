namespace SnakeGame.Core.GameObjects;

public abstract class Food : Point
{
    private Wall wall;
    private Random random;
    private char foodSymbol;

    protected Food(Wall wall, char foodSymbol, int points)
        : base(wall.LeftX, wall.TopY)
    {
        this.wall = wall;
        random = new Random();
        this.foodSymbol = foodSymbol;
        FoodPoints = points;
    }

    public int FoodPoints { get; private set; }

    public void SetRandomPosition(Queue<Point> snakeElements)
    {
        bool isPointOfSnake;

        do
        {
            LeftX = random.Next(2, wall.LeftX - 2);
            TopY = random.Next(2, wall.TopY - 2);

            isPointOfSnake = snakeElements.Any(x => x.LeftX == LeftX && x.TopY == TopY);
        }
        while (isPointOfSnake);

        Console.BackgroundColor = ConsoleColor.Red;
        Draw(foodSymbol);
        Console.BackgroundColor = ConsoleColor.White;
    }

    public bool IsFoodPoint(Point snake)
        => snake.LeftX == LeftX && snake.TopY == TopY;
}
