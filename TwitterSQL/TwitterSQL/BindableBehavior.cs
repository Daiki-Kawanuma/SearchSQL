using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TwitterSQL
{
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        protected T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindableObject)
        {
            base.OnAttachedTo(bindableObject);

            AssociatedObject = bindableObject;

            if (bindableObject.BindingContext != null)
                BindingContext = bindableObject.BindingContext;

            bindableObject.BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnDetachingFrom(T bindableObject)
        {
            bindableObject.BindingContextChanged -= OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}
