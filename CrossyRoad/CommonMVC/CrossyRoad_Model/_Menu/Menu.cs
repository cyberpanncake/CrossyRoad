using System.Collections.Generic;
using System.Linq;

namespace CrossyRoad_Model._Menu
{
  /// <summary>
  /// Модель меню
  /// </summary>
  public class Menu
  {
    /// <summary>
    /// Пункты меню
    /// </summary>
    private readonly Dictionary<int, MenuItem> _items = new Dictionary<int, MenuItem>();
    /// <summary>
    /// Позиция текущего пункта меню в фокусе
    /// </summary>
    private int _focusedItemPosition = 0;
    /// <summary>
    /// Пункты меню
    /// </summary>
    public MenuItem[] Items
    {
      get
      {
        return _items.Values.ToArray();
      }
    }

    /// <summary>
    /// Добавляет пункт меню в меню
    /// </summary>
    /// <param name="parItem">Пункт меню</param>
    /// <param name="parPosition">Позиция пункта меню в меню</param>
    public void AddItem(MenuItem parItem, int parPosition)
    {
      _items.Add(parPosition, parItem);
    }

    /// <summary>
    /// Смещает фокус на предыдущий пункт меню (циклически)
    /// </summary>
    public void MoveFocusToPreviousItem()
    {
      MoveFocus(-1);
    }

    /// <summary>
    /// Смещает фокус на предыдущий пункт меню (циклически)
    /// </summary>
    public void MoveFocusToNextItem()
    {
      MoveFocus(1);
    }

    /// <summary>
    /// Смещает фокус на пункт меню по сдвигу (циклически)
    /// </summary>
    /// <param name="parOffset">Сдвиг (1 - следующий пункт меню, -1 - предыдущий)</param>
    private void MoveFocus(int parOffset)
    {
      _items[_focusedItemPosition].State = MenuItemState.Default;
      _focusedItemPosition += parOffset;
      _focusedItemPosition = (_focusedItemPosition == -1 ? _items.Count - 1 : _focusedItemPosition) % _items.Count;
      _items[_focusedItemPosition].State = MenuItemState.Focused;
    }

    /// <summary>
    /// Выбирает текущий пункт меню в фокусе
    /// </summary>
    public void SelectCurrentItem()
    {
      _items[_focusedItemPosition].State = MenuItemState.Selected;
    }
  }
}