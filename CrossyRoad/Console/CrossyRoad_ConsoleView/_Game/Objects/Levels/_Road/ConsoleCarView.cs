using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects.Levels._Road;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._Road
{
  /// <summary>
  /// Представление машины в консольном приложении
  /// </summary>
  public class ConsoleCarView : CarView
  {
    /// <summary>
    /// Изображение машины
    /// </summary>
    private readonly ConsoleGameObjectImage _image;

    /// <summary>
    /// Конструктор представления машины в консольном приложении
    /// </summary>
    /// <param name="parCar">Машина</param>
    public ConsoleCarView(Car parCar)
      : base(parCar)
    {
      switch (Color)
      {
        case CarColors.Blue:
          _image = ConsoleGameObjectsImages.CarBlue;
          break;
        case CarColors.Green:
          _image = ConsoleGameObjectsImages.CarGreen;
          break;
        case CarColors.Red:
          _image = ConsoleGameObjectsImages.CarRed;
          break;
        case CarColors.Purple:
          _image = ConsoleGameObjectsImages.CarPurple;
          break;
      }
      if (Car.SpeedX < 0)
      {
        _image = ConsoleGameObjectsUtils.FlipImageHorizontally(_image);
      }
    }
    /// <summary>
    /// Отображает машину на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((Car.AbsoluteY - ((Level)Car.Parent).GameField.AbsoluteY) * Scale);
      int j = (int)Math.Round((Car.AbsoluteX - ((Level)Car.Parent).GameField.AbsoluteX) * Scale);
      ConsoleGameObjectsUtils.DrawImageInBuffer(_image, i, j);
    }
  }
}