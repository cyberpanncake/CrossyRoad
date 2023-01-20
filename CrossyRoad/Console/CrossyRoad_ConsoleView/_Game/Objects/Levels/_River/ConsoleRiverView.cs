using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_View._Game.Objects.Levels._River;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление реки в консольном приложении
  /// </summary>
  public class ConsoleRiverView : RiverView
  {
    /// <summary>
    /// Конструктор представления реки в консольном приложении
    /// </summary>
    /// <param name="parRiver">Модель реки</param>
    public ConsoleRiverView(River parRiver)
      : base(parRiver)
    {
    }

    /// <summary>
    /// Отображает реку на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((River.AbsoluteY - River.GameField.AbsoluteY) * Scale);
      ConsoleGameObjectsUtils.DrawLevelBackgroundInBuffer(ConsoleColor.Blue, i);
      foreach (LogView elLogView in LogsViews)
      {
        elLogView.Draw();
      }
    }

    /// <summary>
    /// Добавляет представление бревна в список
    /// </summary>
    /// <param name="parLog">Модель бревна</param>
    protected override void AddLogView(Log parLog)
    {
      LogsViews.Add(new ConsoleLogView(parLog));
    }
  }
}