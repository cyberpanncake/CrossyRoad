namespace CrossyRoad_Model._Game.Objects.Levels
{
  /// <summary>
  /// Модель полосы препятствий
  /// </summary>
  public abstract class Level : GameObject
  {
    /// <summary>
    /// Скорость перемещения полос препятствий по вертикали (клетка/сек)
    /// </summary>
    public static float Speed { get; private set; } = GameSettings.LevelStartSpeed;
    /// <summary>
    /// Достигал ли игрок когда-либо этой полосы препятствий
    /// </summary>
    public bool IsVisited { get; private set; }
    /// <summary>
    /// Игровое поле
    /// </summary>
    public GameField GameField { get; }

    /// <summary>
    /// Конструктор модели полосы препятствий
    /// </summary>
    /// <param name="parY">Координата полосы по вертикали (клетка)</param>
    public Level(GameField parGameField, float parY)
      : base(null, 0, parY, 0, ModelConfiguration.LEVEL_HEIGHT)
    {
      GameField = parGameField;
      IsVisited = false;
    }

    /// <summary>
    /// Отмечает полосу препятствий как посещённую
    /// </summary>
    public void SetLevelVisited()
    {
      IsVisited = true;
    }

    /// <summary>
    /// Увеличивает скорость перемещения полос препятствий
    /// </summary>
    public static void IncreaseSpeed()
    {
      if (Speed + ModelConfiguration.LEVEL_SPEED_DELTA <= ModelConfiguration.LEVEL_MAX_SPEED)
      {
        Speed += ModelConfiguration.LEVEL_SPEED_DELTA;
      }
      else
      {
        Speed = ModelConfiguration.LEVEL_MAX_SPEED;
      }
    }

    /// <summary>
    /// Перемещает полосу препятствий на расстояние, соответствующее времени,
    /// прошедшему с последнего перемещения, и скорости полосы
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    protected void Move(long parTimeMilliseconds)
    {
      float elapsedSeconds = parTimeMilliseconds / 1000f;
      Y += Speed * elapsedSeconds;
    }

    /// <summary>
    /// Изменяет состояние полосы препятствий
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public abstract void ChangeState(long parTimeMilliseconds);

    /// <summary>
    /// Проверяет, возникает ли ситуация конца игры на координате по горизонтали
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>true - если игра заканчивается на выбранной координате, false - в противном случае</returns>
    public abstract bool IsGameOverOnX(float parX);

    /// <summary>
    /// Генерирует объект на полосе препятствий, если это необходимо
    /// (например, когда игрок переместился вправо/влево)
    /// </summary>
    public abstract void GenerateObjectIfNeed();

    /// <summary>
    /// Генерирует объект на полосе препятствий с некоторой вероятностью
    /// </summary>
    /// <param name="parGenerateOnRight">Флаг генерации объекта справа от уже существующих (true), или слева (false)</param>
    /// <returns>Сгенерированный объект, либо null - отсутствие объекта на координате</returns>
    protected abstract GameObject GenerateObject(bool parGenerateOnRight);
  }
}