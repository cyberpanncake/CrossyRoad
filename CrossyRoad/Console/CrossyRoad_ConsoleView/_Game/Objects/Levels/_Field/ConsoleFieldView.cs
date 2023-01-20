using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_View._Game.Objects.Levels._Field;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление поля в консольном приложении
  /// </summary>
  public class ConsoleFieldView : FieldView
  {
    /// <summary>
    /// Конструктор представления поля в консольном приложении
    /// </summary>
    /// <param name="parField">Модель поля</param>
    public ConsoleFieldView(Field parField)
      : base(parField)
    {
    }

    /// <summary>
    /// Отображает поле на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((Field.AbsoluteY - Field.GameField.AbsoluteY) * Scale);
      ConsoleGameObjectsUtils.DrawLevelBackgroundInBuffer(ConsoleColor.Green, i);
      foreach (BarrierView elBarrierView in BarriersViews)
      {
        elBarrierView.Draw();
      }
    }

    /// <summary>
    /// Добавляет представление статичного препятствия в список
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    protected override void AddBarrierView(Barrier parBarrier)
    {
      BarriersViews.Add(new ConsoleBarrierView(parBarrier));
    }
  }
}