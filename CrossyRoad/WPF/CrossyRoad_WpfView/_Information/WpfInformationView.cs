using CrossyRoad_View._Information;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad_WpfView._Information
{
  /// <summary>
  /// Представление окна справки (пункт меню "Как играть") в приложении WPF
  /// </summary>
  public class WpfInformationView : InformationView
  {
    /// <summary>
    /// Графический элемент текста справки
    /// </summary>
    private TextBlock _element;

    /// <summary>
    /// Конструктор представления окна справки в консольном приложении
    /// </summary>
    public WpfInformationView()
      : base()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        _element = new TextBlock
        {
          Text = InformationText,
          TextAlignment = TextAlignment.Justify,
          TextWrapping = TextWrapping.Wrap,
          Margin = new Thickness(0, WpfViewConfiguration.DEFAULT_MARGIN, 0, 0)
        };
        DockPanel.SetDock(_element, Dock.Top);
      });
    }

    /// <summary>
    /// Отображает окно справки на экране приложения
    /// </summary>
    public override void Draw()
    {
      WpfUtils.Dispatcher.Invoke(() =>
      {
        WpfUtils.ClearWindow();
        WpfUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
        WpfUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Information_Title);
        WpfUtils.DrawOnWindow(_element);
      });
    }
  }
}