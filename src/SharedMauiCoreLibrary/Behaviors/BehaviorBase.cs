namespace AndreasReitberger.Shared.Core.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T? AssociatedObject { get; set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            if (bindable.BindingContext is not null)
            {
                BindingContext = bindable.BindingContext;
            }
            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T? bindable)
        {
            if (bindable is not null)
            {
                base.OnDetachingFrom(bindable);
                bindable.BindingContextChanged -= OnBindingContextChanged;
            }
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object? sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject?.BindingContext;
        }
    }
}
