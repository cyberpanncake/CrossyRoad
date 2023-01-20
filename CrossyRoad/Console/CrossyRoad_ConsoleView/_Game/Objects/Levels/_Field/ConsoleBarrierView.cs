using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_View._Game.Objects.Levels._Field;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._Field
{
  /// <summary>
  /// Представление статичного препятствия в консольном приложении
  /// </summary>
  public class ConsoleBarrierView : BarrierView
  {
    /// <summary>
    /// Изображение статичного препятствия
    /// </summary>
    private readonly ConsoleGameObjectImage _image;

    /// <summary>
    /// Конструктор представления статичного препятствия в консольном приложении
    /// </summary>
    /// <param name="parBarrier">Модель статичного препятствия</param>
    public ConsoleBarrierView(Barrier parBarrier)
      : base(parBarrier)
    {
      switch (Type)
      {
        case BarrierTypes.Stone:
          _image = ConsoleGameObjectsImages.Stone;
          break;
        case BarrierTypes.Bush:
          _image = ConsoleGameObjectsImages.Bush;
          break;
      }
    }

    /// <summary>
    /// Отображает статичное препятствие на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((Barrier.AbsoluteY - ((Level)Barrier.Parent).GameField.AbsoluteY) * Scale);
      int j = (int)Math.Round((Barrier.AbsoluteX - ((Level)Barrier.Parent).GameField.AbsoluteX) * Scale);
      ConsoleGameObjectsUtils.DrawImageInBuffer(_image, i, j);
    }
  }
}