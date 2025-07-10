namespace KakeiboApp.Behaviors;

public class FocusManagerBehavior : Behavior<VisualElement>
{
    public static readonly BindableProperty NextElementProperty =
        BindableProperty.Create(nameof(NextElement), typeof(VisualElement), typeof(FocusManagerBehavior));

    public VisualElement? NextElement
    {
        get => (VisualElement?)GetValue(NextElementProperty);
        set => SetValue(NextElementProperty, value);
    }

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);

        if (bindable is Entry entry)
        {
            entry.Completed += OnCompleted;
        }

        if (bindable is Picker picker)
        {
            picker.SelectedIndexChanged += OnCompleted;
        }
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        base.OnDetachingFrom(bindable);

        if (bindable is Entry entry)
        {
            entry.Completed -= OnCompleted;
        }

        if (bindable is Picker picker)
        {
            picker.SelectedIndexChanged -= OnCompleted;
        }
    }

    private void OnCompleted(object? sender, EventArgs e)
    {
        NextElement?.Focus();
    }
}