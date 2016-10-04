using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TwitterSQL
{
    public class EventToCommandBehavior : BindableBehavior<VisualElement>
    {
        #region Command BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<EventToCommandBehavior, ICommand>(p => p.Command, null);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<EventToCommandBehavior, object>(p => p.CommandParameter, null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Converter BindableProperty
        public static readonly BindableProperty ConverterProperty = BindableProperty.Create<EventToCommandBehavior, IValueConverter>(p => p.Converter, null);
        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }
        #endregion

        public string EventName { get; set; }

        EventInfo _eventInfo;

        Delegate _eventHandler;

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            if (string.IsNullOrWhiteSpace(EventName))
            {
                return;
            }

            _eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);
            if (_eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{EventName}' event.");

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            _eventHandler = methodInfo.CreateDelegate(_eventInfo.EventHandlerType, this);
            _eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (_eventInfo != null && _eventHandler != null)
                _eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);

            base.OnDetachingFrom(bindable);
        }

        void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            object resolvedParameter;
            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }
            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }
        }
    }
}
