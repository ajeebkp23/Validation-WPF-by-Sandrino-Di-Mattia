using System;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using POC.Validatie.Model.Validation;

namespace POC.Validatie
{
/// <summary>
/// Binding that will automatically implement the validation
/// </summary>
public class ValidationBinding : BindingDecoratorBase
{
    public ValidationBinding()
        : base()
    {
        Binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
    }

    /// <summary>
    /// This method is being invoked during initialization.
    /// </summary>
    /// <param name="provider">Provides access to the bound items.</param>
    /// <returns>The binding expression that is created by the base class.</returns>
    public override object ProvideValue(IServiceProvider provider)
    { 
        // Get the binding expression
        object bindingExpression = base.ProvideValue(provider);
        
        // Bound items
        DependencyObject targetObject;
        DependencyProperty targetProperty;

        // Try to get the bound items
        if (TryGetTargetItems(provider, out targetObject, out targetProperty))
        {
            if (targetObject is FrameworkElement)
            {
                // Get the element and implement datacontext changes
                FrameworkElement element = targetObject as FrameworkElement;
                element.DataContextChanged += new DependencyPropertyChangedEventHandler(element_DataContextChanged);

                // Set the template
                ControlTemplate controlTemplate = element.TryFindResource("validationTemplate") as ControlTemplate;
                if (controlTemplate != null)
                    Validation.SetErrorTemplate(element, controlTemplate);
            }
        }

        // Go on with the flow
        return bindingExpression;
    }

    /// <summary>
    /// Datacontext of the control has changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void element_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        object datacontext = e.NewValue;
        if (datacontext != null)
        {
            PropertyInfo property = datacontext.GetType().GetProperty(Binding.Path.Path);
            if (property != null)
            {
                IEnumerable<object> attributes = property.GetCustomAttributes(true).Where(o => o is IValidationRule);
                foreach (IValidationRule validationRule in attributes)
                    ValidationRules.Add(new GenericValidationRule(validationRule));
            }
        }
    }
}
}