using KakeiboApp.ViewModels;

namespace KakeiboApp.Behaviors;

public class FocusOnVisibleBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Entry.IsVisible) && bindable.IsVisible)
            {
                bindable.Focus();
                bindable.CursorPosition = 0;
                bindable.SelectionLength  = bindable.Text?.Length ?? 0;
            }
        };
    }
}