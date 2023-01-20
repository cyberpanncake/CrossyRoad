using CrossyRoad_View._GameOver;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._GameOver
{
  /// <summary>
  /// Представление окна окончания игры в приложении WPF
  /// </summary>
  public class WpfGameOverView : GameOverView
  {
    /// <summary>
    /// Графический элемент окна окончания игры
    /// </summary>
    private Grid _element;

    /// <summary>
    /// Конструктор представления окна окончания игры в приложении WPF
    /// </summary>
    /// <param name="parScore">Набранные очки</param>
    public WpfGameOverView(int parScore)
      : base(parScore)
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _element = new Grid
        {
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0)
        };
        _element.RowDefinitions.Add(new RowDefinition());
        _element.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        _element.RowDefinitions.Add(new RowDefinition());
        StackPanel panel = new StackPanel();
        Grid.SetRow(panel, 1);
        Grid.SetColumn(panel, 1);
        _ = _element.Children.Add(panel);
        panel.Children.Add(new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_GameOver,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        });
        panel.Children.Add(new TextBlock()
        {
          Text = CrossyRoad_View.Properties.Resources.GameOver_Score + Score,
          Margin = new Thickness(WpfViewConfiguration.TABLE_CELL_MARGIN),
          TextAlignment = TextAlignment.Center
        });
      });
    }

    /// <summary>
    /// Отображает окно окончания игры на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
        WpfUtils.DrawOnWindow(_element);
      });
    }
  }
}