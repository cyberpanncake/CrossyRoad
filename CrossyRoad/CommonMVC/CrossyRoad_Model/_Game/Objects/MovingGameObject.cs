namespace CrossyRoad_Model._Game.Objects
{
  /// <summary>
  /// Модель движущегося игрового объекта
  /// </summary>
  public abstract class MovingGameObject : GameObject
  {
    /// <summary>
    /// Скорость по горизонтали (клетка/сек)
    /// </summary>
    public float SpeedX { get; protected set; }
    /// <summary>
    /// Скорость по вертикали (клетка/сек)
    /// </summary>
    public float SpeedY { get; protected set; }

    /// <summary>
    /// Конструктор модели движущегося игрового объекта
    /// </summary>
    /// <param name="parParent">Родительский объект</param>
    /// <param name="parX">Координата левого верхнего угла по горизонтали (клетка)</param>
    /// <param name="parY">Координата левого верхнего угла по вертикали (клетка)</param>
    /// <param name="parWidth">Ширина (клетка)</param>
    /// <param name="parHeight">Высота (клетка)</param>
    /// <param name="parSpeedX">Скорость по горизонтали (клетка/сек)</param>
    /// <param name="parSpeedY">Скорость по вертикали (клетка/сек)</param>
    public MovingGameObject(GameObject parParent, float parX, float parY, float parWidth, float parHeight,
      float parSpeedX, float parSpeedY)
      : base(parParent, parX, parY, parWidth, parHeight)
    {
      SpeedX = parSpeedX;
      SpeedY = parSpeedY;
    }

    /// <summary>
    /// Перемещает объект на расстояние, соответствующее времени,
    /// прошедшему с последнего перемещения, и скорости объекта
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public void Move(long parTimeMilliseconds)
    {
      float elapsedSeconds = parTimeMilliseconds / 1000f;
      X += SpeedX * elapsedSeconds;
      Y += SpeedY * elapsedSeconds;
    }
  }
}