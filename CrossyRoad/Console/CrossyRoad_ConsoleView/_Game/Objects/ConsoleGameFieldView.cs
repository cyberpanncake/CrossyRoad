using CrossyRoad_ConsoleView._Game.Objects.Levels._Field;
using CrossyRoad_ConsoleView._Game.Objects.Levels._River;
using CrossyRoad_ConsoleView._Game.Objects.Levels._Road;
using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects;
using CrossyRoad_View._Game.Objects.Levels;
using CrossyRoad_View._Game.Objects.Levels._Road;

namespace CrossyRoad_ConsoleView._Game.Objects
{
  /// <summary>
  /// Представление игрового поля в консольном приложении
  /// </summary>
  public class ConsoleGameFieldView : GameFieldView
  {
    /// <summary>
    /// Конструктор представления игрового поля в консольном приложении
    /// </summary>
    /// <param name="parGameField">Модель игрового поля</param>
    public ConsoleGameFieldView(GameField parGameField)
      : base(parGameField)
    {
      Scale = ConsoleViewConfiguration.GAME_OBJECTS_SCALE;
      Width = GameField.Width * Scale;
      Height = GameField.Height * Scale;
    }

    /// <summary>
    /// Отображает игровое поле на экране приложения
    /// </summary>
    public override void Draw()
    {
      ConsoleGameObjectsUtils.InitializeGameFieldImage((int)Width, (int)Height);
      foreach (LevelView elLevelView in LevelsViews.Values)
      {
        elLevelView.Draw();
      }
      PlayerView.Draw();
      if (LevelsViews[GameField.Player.CurrentLevel] is RoadView currentRoadView)
      {
        currentRoadView.DrawCars();
      }
      ConsoleGameObjectsUtils.DrawBufferInConsole();
    }

    /// <summary>
    /// Добавляет представление полосы препятствий в список
    /// </summary>
    /// <param name="parLevel">Модель полосы препятствий</param>
    protected override void AddLevelView(Level parLevel)
    {
      if (parLevel is Field field)
      {
        LevelsViews.Add(parLevel, new ConsoleFieldView(field));
      }
      if (parLevel is Road road)
      {
        LevelsViews.Add(parLevel, new ConsoleRoadView(road));
      }
      if (parLevel is River river)
      {
        LevelsViews.Add(parLevel, new ConsoleRiverView(river));
      }
    }

    /// <summary>
    /// Создаёт представление игрока
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    /// <returns>Представление игрока</returns>
    protected override PlayerView CreatePlayerView(Player parPlayer)
    {
      return new ConsolePlayerView(parPlayer);
    }
  }
}