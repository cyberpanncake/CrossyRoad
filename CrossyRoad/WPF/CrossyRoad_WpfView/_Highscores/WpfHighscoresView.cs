using CrossyRoad_Model;
using CrossyRoad_Model._Highscores;
using CrossyRoad_View._Highscores;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Highscores
{
  /// <summary>
  /// Представление окна с таблицей рекордов (пункт меню "Рекорды") в приложении WPF
  /// </summary>
  public class WpfHighscoresView : HighscoresView
  {
    /// <summary>
    /// Графический элемент главного меню
    /// </summary>
    private Grid _element;

    /// <summary>
    /// Конструктор представления окна с таблицей рекордов в приложении WPF
    /// </summary>
    public WpfHighscoresView()
      : base(Highscores.Instance)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _element = new Grid
        {
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0),
          HorizontalAlignment = HorizontalAlignment.Center
        };
        _element.RowDefinitions.Add(new RowDefinition());
        _element.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        _element.RowDefinitions.Add(new RowDefinition());
        _ = _element.Children.Add(CreateHighscoresTable());
      });
    }

    /// <summary>
    /// Создаёт иерархию элементов отображения таблицы рекордов
    /// </summary>
    /// <returns>Контейнер таблицы рекордов</returns>
    private UIElement CreateHighscoresTable()
    {
      Border table = new Border()
      {
        BorderThickness = new Thickness(1)
      };
      Grid.SetRow(table, 1);
      Grid grid = new Grid();
      grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
      grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
      for (int i = 0; i < ModelConfiguration.MAX_HIGHSCORES_COUNT + 1; i++)
      {
        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        for (int j = 0; j < 2; j++)
        {
          Border cell = new Border()
          {
            BorderThickness = new Thickness(1),
            BorderBrush = WpfViewConfiguration.DefaultForegroundColor,
            MinHeight = WpfViewConfiguration.TABLE_CELL_MIN_HEIGHT
          };
          Grid.SetRow(cell, i);
          Grid.SetColumn(cell, j);
          _ = grid.Children.Add(cell);
        }
      }
      AddHeadersToTable(grid);
      AddHighscoresToTable(grid);
      table.Child = grid;
      return table;
    }

    /// <summary>
    /// Добавляет заголовки в таблицу рекордов
    /// </summary>
    /// <param name="parTable">Графический элемент таблицы рекордов</param>
    private void AddHeadersToTable(Grid parTable)
    {
      TextBlock columnPlayerHeader = new TextBlock()
      {
        Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
        Text = CrossyRoad_View.Properties.Resources.Highscores_Column_Player,
        FontWeight = FontWeights.Bold
      };
      ((Border)parTable.Children[0]).Child = columnPlayerHeader;
      TextBlock columnScoreHeader = new TextBlock()
      {
        Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
        Text = CrossyRoad_View.Properties.Resources.Highscores_Column_Score,
        FontWeight = FontWeights.Bold
      };
      ((Border)parTable.Children[1]).Child = columnScoreHeader;
    }

    /// <summary>
    /// Добавляет данные о рекордах в таблицу
    /// </summary>
    /// <param name="parTable">Графический элемент таблицы рекордов</param>
    private void AddHighscoresToTable(Grid parTable)
    {
      for (int i = 1; i <= Highscores.Scores.Length; i++)
      {
        TextBlock cellPlayer = new TextBlock()
        {
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          Text = Highscores.Scores[i - 1].Player
        };
        ((Border)parTable.Children[i * 2]).Child = cellPlayer;
        TextBlock cellScore = new TextBlock()
        {
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          Text = Highscores.Scores[i - 1].Score.ToString()
        };
        ((Border)parTable.Children[i * 2 + 1]).Child = cellScore;
      }
    }

    /// <summary>
    /// Отображает окно с таблицей рекордов на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
        WpfUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Highscores_Title);
        WpfUtils.DrawOnWindow(_element);
      });
    }
  }
}