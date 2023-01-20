using CrossyRoad_ConsoleView._Game.Objects.Images;
using CrossyRoad_Model;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_View._Game.Objects.Levels._River;
using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Levels._River
{
  /// <summary>
  /// Представление бревна в консольном приложении
  /// </summary>
  public class ConsoleLogView : LogView
  {
    /// <summary>
    /// Изображение бревна
    /// </summary>
    private readonly ConsoleGameObjectImage _image;

    /// <summary>
    /// Конструктор представления бревна в консольном приложении
    /// </summary>
    /// <param name="parLog">модель бревна</param>
    public ConsoleLogView(Log parLog)
      : base(parLog)
    {
      if (Math.Abs(Log.Width - 2) < ModelConfiguration.COORDINATE_EPS)
      {
        _image = ConsoleGameObjectsImages.Log2;
      }
      if (Math.Abs(Log.Width - 3) < ModelConfiguration.COORDINATE_EPS)
      {
        _image = ConsoleGameObjectsImages.Log3;
      }
      if (Math.Abs(Log.Width - 4) < ModelConfiguration.COORDINATE_EPS)
      {
        _image = ConsoleGameObjectsImages.Log4;
      }
      if (Log.SpeedX > 0)
      {
        _image = ConsoleGameObjectsUtils.FlipImageHorizontally(_image);
      }
    }

    /// <summary>
    /// Отображает бревно на экране приложения
    /// </summary>
    public override void Draw()
    {
      int i = (int)Math.Round((Log.AbsoluteY - ((Level)Log.Parent).GameField.AbsoluteY) * Scale);
      int j = (int)Math.Round((Log.AbsoluteX - ((Level)Log.Parent).GameField.AbsoluteX) * Scale);
      ConsoleGameObjectsUtils.DrawImageInBuffer(_image, i, j);
    }
  }
}