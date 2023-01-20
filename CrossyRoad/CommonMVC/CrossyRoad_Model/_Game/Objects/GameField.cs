using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_Model._Game.Objects.Levels.LevelFactories;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using System;
using System.Collections.ObjectModel;

namespace CrossyRoad_Model._Game.Objects
{
  /// <summary>
  /// Модель игрового поля
  /// </summary>
  public class GameField : GameObject
  {
    /// <summary>
    /// Полосы препятствий
    /// </summary>
    public ObservableCollection<Level> Levels { get; }
    /// <summary>
    /// Фабрика создания полос препятствий
    /// </summary>
    private readonly AbstractLevelFactory _levelFactory;
    /// <summary>
    /// Игрок
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Конструктор модели игрового поля
    /// </summary>
    public GameField()
      : base(null, 0, 0, ModelConfiguration.CELLS_X_COUNT, ModelConfiguration.CELLS_Y_COUNT)
    {
      Levels = new ObservableCollection<Level>();
      _levelFactory = new LevelFactory(this);
      for (int i = 0; i < ModelConfiguration.CELLS_Y_COUNT; i++)
      {
        Levels.Add(_levelFactory.Create());
      }
      Player = new Player(Levels[ModelConfiguration.PLAYER_START_LEVEL_INDEX]);
    }

    /// <summary>
    /// Изменяет состояние модели игрового поля (перемещает все движущиеся объекты)
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public void ChangeState(long parTimeMilliseconds)
    {
      foreach (Level elLevel in Levels)
      {
        elLevel.ChangeState(parTimeMilliseconds);
      }
      MoveGameFieldWithPlayer(parTimeMilliseconds);
      GenerateObjectsIfNeed();
    }

    /// <summary>
    /// Перемещает игровое поле в соответствии с положением игрока
    /// (так, чтобы игрок оставался на заданном расстоянии от его краёв)
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    private void MoveGameFieldWithPlayer(long parTimeMilliseconds)
    {
      float elapsedSeconds = parTimeMilliseconds / 1000f;
      float maxMotion = ModelConfiguration.GAME_FIELD_SPEED * elapsedSeconds;
      float dx = (Player.AbsoluteX - AbsoluteX) - ModelConfiguration.PLAYER_X_AND_GAME_FIELD_X_DISTANCE;
      dx = (float)(Math.Abs(dx) < maxMotion ? dx : (maxMotion * dx / Math.Abs(dx)));
      float dy = Player.AbsoluteY - AbsoluteY;
      dy = dy < ModelConfiguration.PLAYER_Y_AND_GAME_FIELD_Y_MIN_DISTANCE ?
        dy - ModelConfiguration.PLAYER_Y_AND_GAME_FIELD_Y_MIN_DISTANCE : 0;
      dy = (float)(Math.Abs(dy) < maxMotion ? dy : (maxMotion * dy / Math.Abs(dy)));
      X += dx;
      Y += dy;
    }

    /// <summary>
    /// Генерирует полосы препятствий и объекты на них, если это необходимо
    /// (например, когда игрок переместился вправо/влево)
    /// </summary>
    private void GenerateObjectsIfNeed()
    {
      if (Levels[0].AbsoluteY > AbsoluteY + Height)
      {
        Levels.RemoveAt(0);
      }
      if (Levels[^1].AbsoluteY > AbsoluteY)
      {
        Levels.Add(_levelFactory.Create());
      }
      foreach (Level elLevel in Levels)
      {
        elLevel.GenerateObjectIfNeed();
      }
    }

    /// <summary>
    /// Увеличивает скорость перемещения полос препятствий по вертикали
    /// </summary>
    public void IncreaseLevelsSpeed()
    {
      Level.IncreaseSpeed();
    }

    /// <summary>
    /// Перемещает игрока и игровое поле в выбранном направлении
    /// </summary>
    /// <param name="parDirection">Направление движения игрока</param>
    public void MovePlayerOn(Directions parDirection)
    {
      if (parDirection == Directions.Left || parDirection == Directions.Right)
      {
        MovePlayerHorizontal(parDirection == Directions.Left);
      }
      else
      {
        MovePlayerVertical(parDirection == Directions.Up);
      }
      GenerateObjectsIfNeed();
    }

    /// <summary>
    /// Перемещает игрока горизонтально
    /// </summary>
    /// <param name="parMoveLeft">Флаг перемещения влево (true), или вправо (false)</param>
    private void MovePlayerHorizontal(bool parMoveLeft)
    {
      float dx = parMoveLeft ? -1 : 1;
      if (!(Player.Parent is Field field) || !field.HasObjectOnX(Player.AbsoluteX + dx))
      {
        Player.X += dx;
      }
    }

    /// <summary>
    /// Перемещает игрока вертикально
    /// </summary>
    /// <param name="parMoveUp">Флаг перемещения вверх (true), или вниз (false)</param>
    private void MovePlayerVertical(bool parMoveUp)
    {
      if (!(!parMoveUp && Levels.IndexOf(Player.CurrentLevel) == 0))
      {
        Level nextLevel = GetNeighbourLevel(parMoveUp);
        float roundedX = (float)Math.Round(Player.AbsoluteX);
        if ((nextLevel is Field field && !field.HasObjectOnX(roundedX)) || nextLevel is Road)
        {
          Player.X = roundedX;
          Player.Parent = nextLevel;
        }
        if (nextLevel is River river)
        {
          Log log = river.GetLogToMoveOnX(Player.AbsoluteX);
          if (log != null)
          {
            Player.X = (float)Math.Round(Player.AbsoluteX - log.AbsoluteX);
            Player.X = Player.X < 0 ? 0 : (Player.X + Player.Width > log.Width ? log.Width - 1 : Player.X);
            Player.Parent = log;
          }
          else
          {
            Player.X = roundedX;
            Player.Parent = river;
          }
        }
      }
    }

    /// <summary>
    /// Получает соседнюю сверху/снизу полосу препятствий от полосы, на которой находится игрок
    /// </summary>
    /// <param name="parUpNeighbour">Флаг получения полосы сверху (true), или снизу (false)</param>
    /// <returns>Соседняя полоса от той, на которой находится игрок</returns>
    private Level GetNeighbourLevel(bool parUpNeighbour)
    {
      int currentLevelIndex = Levels.IndexOf(Player.CurrentLevel);
      return Levels[currentLevelIndex + (parUpNeighbour ? 1 : -1)];
    }
  }
}