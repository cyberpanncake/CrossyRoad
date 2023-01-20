using CrossyRoad_Model;
using CrossyRoad_Model._Game.Objects;
using CrossyRoad_Model._Game.Objects.Levels;
using CrossyRoad_Model._Game.Objects.Levels._Field;
using CrossyRoad_Model._Game.Objects.Levels._River;
using CrossyRoad_Model._Game.Objects.Levels._Road;
using CrossyRoad_View._Game.Objects;
using CrossyRoad_View._Game.Objects.Levels;
using CrossyRoad_WpfView._Game.Objects.Levels._Field;
using CrossyRoad_WpfView._Game.Objects.Levels._River;
using CrossyRoad_WpfView._Game.Objects.Levels._Road;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Game.Objects
{
  /// <summary>
  /// Представление игрового поля в приложении WPF
  /// </summary>
  public class WpfGameFieldView : GameFieldView
  {
    /// <summary>
    /// Графический элемент, отображающий игровое поле
    /// </summary>
    public Grid Element { get; private set; }
    /// <summary>
    /// Изображение игрового поля с игровыми объектами
    /// </summary>
    private Canvas _image;

    /// <summary>
    /// Конструктор представления игрового поля в приложении WPF
    /// </summary>
    /// <param name="parGameField">Модель игрового поля</param>
    public WpfGameFieldView(GameField parGameField)
      : base(parGameField)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _image = new Canvas
        {
          ClipToBounds = true
        };
        AddGameObjectsElementsToGameField();
        Element = new Grid();
        Element.ColumnDefinitions.Add(new ColumnDefinition());
        Element.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        Element.ColumnDefinitions.Add(new ColumnDefinition());
        Grid.SetColumn(_image, 1);
        _ = Element.Children.Add(_image);
      });
    }

    /// <summary>
    /// Добавляет все изображения игровых объектов на элемент игрового поля
    /// </summary>
    private void AddGameObjectsElementsToGameField()
    {
      foreach (LevelView elLevelView in LevelsViews.Values.ToList())
      {
        if (elLevelView is WpfFieldView fieldView)
        {
          _ = _image.Children.Add(fieldView.Image);
          foreach (WpfBarrierView elBarrierView in fieldView.BarriersViews.ToList())
          {
            _ = _image.Children.Add(elBarrierView.Image);
          }
        }
        if (elLevelView is WpfRoadView roadView)
        {
          _ = _image.Children.Add(roadView.Image);
          foreach (WpfCarView elCarView in roadView.CarsViews.ToList())
          {
            _ = _image.Children.Add(elCarView.Image);
          }
        }
        if (elLevelView is WpfRiverView logView)
        {
          _ = _image.Children.Add(logView.Image);
          foreach (WpfLogView elLogView in logView.LogsViews.ToList())
          {
            _ = _image.Children.Add(elLogView.Image);
          }
        }
      }
      _ = _image.Children.Add(((WpfPlayerView)PlayerView).Image);
      if (LevelsViews[GameField.Player.CurrentLevel] is WpfRoadView currentRoadView)
      {
        foreach (WpfCarView elCarView in currentRoadView.CarsViews.ToList())
        {
          _image.Children.Remove(elCarView.Image);
          _ = _image.Children.Add(elCarView.Image);
        }
      }
    }

    /// <summary>
    /// Отображает игровоге поле на экране приложения в первый раз
    /// </summary>
    public async void DrawFirstTime()
    {
      await Task.Run(() => { WpfUtils.DrawOnWindow(Element); });
    }

    /// <summary>
    /// Отображает игровое поля на экране приложения
    /// </summary>
    public override async void Draw()
    {
      await Task.Run(() =>
      {
        CalculateScaleAndChangeCanvasSize();
        WpfUtils.Dispatcher.Invoke(() =>
        {
          try
          {
            foreach (LevelView elLevelView in LevelsViews.Values)
            {
              elLevelView.Draw();
            }
            PlayerView.Draw();
            _image.Children.Clear();
            AddGameObjectsElementsToGameField();
            WpfUtils.DrawOnWindow(Element);
          }
          catch (TaskCanceledException)
          {
          }
        });
      });
    }

    /// <summary>
    /// Вычисляет коэффициент масштабирования изображений игровых объектов
    /// и изменяет размеры игрового поля
    /// </summary>
    private void CalculateScaleAndChangeCanvasSize()
    {
      double widthScale = Element.ActualWidth / ModelConfiguration.CELLS_X_COUNT;
      double heightScale = Element.ActualHeight / ModelConfiguration.CELLS_Y_COUNT;
      Scale = (float)Math.Min(widthScale, heightScale);
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _image.Width = Scale * ModelConfiguration.CELLS_X_COUNT;
        _image.Height = Scale * ModelConfiguration.CELLS_Y_COUNT;
      });
    }

    /// <summary>
    /// Добавляет представление полосы препятствий в список
    /// </summary>
    /// <param name="parLevel">Модель полосы препятствий</param>
    protected override void AddLevelView(Level parLevel)
    {
      if (parLevel is Field field)
      {
        LevelsViews.Add(parLevel, new WpfFieldView(field));
      }
      if (parLevel is Road road)
      {
        LevelsViews.Add(parLevel, new WpfRoadView(road));
      }
      if (parLevel is River river)
      {
        LevelsViews.Add(parLevel, new WpfRiverView(river));
      }
    }

    /// <summary>
    /// Создаёт представление игрока
    /// </summary>
    /// <param name="parPlayer">Модель игрока</param>
    /// <returns>Представление игрока</returns>
    protected override PlayerView CreatePlayerView(Player parPlayer)
    {
      return new WpfPlayerView(parPlayer);
    }
  }
}