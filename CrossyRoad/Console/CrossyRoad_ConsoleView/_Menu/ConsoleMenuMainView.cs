using CrossyRoad_Model._Menu;
using CrossyRoad_View._Menu;
using System;

namespace CrossyRoad_ConsoleView._Menu
{
  /// <summary>
  /// Представление окна главного меню в консольном приложении
  /// </summary>
  public class ConsoleMenuMainView : MenuView
  {
    /// <summary>
    /// Конструктор представления окна главного меню в консольном приложении
    /// </summary>
    public ConsoleMenuMainView()
      : base(MenuMain.Instance)
    {
      SetMenuPositionAndSize();
      int x = (int)X + 2;
      int y = (int)Y + 1;
      int width = (int)Width - 4;
      int height = (int)Height;
      for (int i = 0; i < Menu.Items.Length; i++)
      {
        MenuItem menuItem = Menu.Items[i];
        int menuItemHeight = menuItem.Name.Length / width + menuItem.Name.Length % width == 0 ? 0 : 1;
        ConsoleMenuItemView menuItemView = new ConsoleMenuItemView(menuItem)
        {
          X = x,
          Y = y,
          Width = width,
          Height = menuItemHeight
        };
        y += menuItemHeight + 1;
        menuItem.Changed += menuItemView.Draw;
        ItemsViews.Add(menuItemView);
      }
    }

    /// <summary>
    /// Рассчитывает и устанавливает положение и размеры главного меню
    /// </summary>
    private void SetMenuPositionAndSize()
    {
      int maxMenuItemWidth = 0;
      int height = -1;
      int maxMenuWidth = ConsoleViewConfiguration.WINDOW_WIDTH - 4;
      foreach (MenuItem menuItem in Menu.Items)
      {
        int nameLength = menuItem.Name.Length;
        if (nameLength > maxMenuItemWidth)
        {
          maxMenuItemWidth = nameLength;
        }
        height += nameLength / maxMenuWidth + nameLength % maxMenuWidth == 0 ? 0 : 1 + 1;
      }
      Width = Math.Min(maxMenuWidth, maxMenuItemWidth) + 4;
      Height = height + 2;
      ConsoleUtils.GetViewPositionInCenterWithHelpText(Width, Height, out float y, out float x);
      X = x;
      Y = y;
    }

    /// <summary>
    /// Отображает главное меню на экране приложения
    /// </summary>
    public override void Draw()
    {
      ConsoleUtils.ClearWindow();
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_Menu);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Menu_GameName, 3);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Menu_Title, 5);
      DrawBorder();
      foreach (MenuItemView menuItemView in ItemsViews)
      {
        menuItemView.Draw();
      }
    }

    /// <summary>
    /// Отображает рамку главного меню
    /// </summary>
    private void DrawBorder()
    {
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      Console.SetCursorPosition((int)X, (int)Y);
      Console.Write("╔");
      for (int i = 0; i < Width - 2; i++)
      {
        Console.Write("═");
      }
      Console.Write("╗");
      for (int i = 1; i < Height - 1; i++)
      {
        Console.SetCursorPosition((int)X, (int)Y + i);
        Console.Write("║");
        Console.SetCursorPosition((int)X + (int)Width - 1, (int)Y + i);
        Console.Write("║");
      }
      Console.SetCursorPosition((int)X, (int)Y + (int)Height - 1);
      Console.Write("╚");
      for (int i = 0; i < Width - 2; i++)
      {
        Console.Write("═");
      }
      Console.Write("╝");
    }
  }
}