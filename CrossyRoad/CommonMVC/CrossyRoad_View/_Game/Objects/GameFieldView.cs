using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_View._Game.Objects.Levels;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CrossyRoad_View._Game.Objects
{
  /// <summary>
  /// Представление игрового поля
  /// </summary>
  public abstract class GameFieldView : GameObjectView
  {
    /// <summary>
    /// Модель игрового поля
    /// </summary>
    protected GameField GameField { get; private set; }
    /// <summary>
    /// Представление игрока
    /// </summary>
    protected PlayerView PlayerView { get; private set; }
    /// <summary>
    /// Список представлений полос препятствий
    /// </summary>
    protected Dictionary<Level, LevelView> LevelsViews { get; private set; }

    /// <summary>
    /// Конструктор представления игрового поля
    /// </summary>
    /// <param name="parGameField">Модель игрового поля</param>
    public GameFieldView(GameField parGameField)
      : base()
    {
      GameField = parGameField;
      PlayerView = CreatePlayerView(GameField.Player);
      LevelsViews = new Dictionary<Level, LevelView>();
      foreach (Level elLevel in GameField.Levels)
      {
        AddLevelView(elLevel);
      }
      GameField.Levels.CollectionChanged += LevelsCollectionChanged;
    }

    /// <summary>
    /// Обработчик изменения коллекции полос препятствий
    /// </summary>
    /// <param name="sender">Объект, пославший событие</param>
    /// <param name="e">Данные события изменения коллекции</param>
    private void LevelsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && (e.NewItems?[0] is Level newLevel))
      {
        AddLevelView(newLevel);
      }
      if (e.Action == NotifyCollectionChangedAction.Remove && (e.OldItems?[0] is Level oldLevel))
      {
        LevelsViews.Remove(oldLevel);
      }
    }

    /// <summary>
    /// Создаёт представление для модели игрока
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    /// <returns>Представление игрока</returns>
    protected abstract PlayerView CreatePlayerView(Player parPlayer);

    /// <summary>
    /// Добавляет представление полосы препятствий в список
    /// </summary>
    /// <param name="parLevel">Модель полосы препятствий</param>
    protected abstract void AddLevelView(Level parLevel);
  }
}