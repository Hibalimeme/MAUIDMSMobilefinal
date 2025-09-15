using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using MAUIDMSMobile.Static;

public static class ValidationHelper
{
    public static bool IsFormValid(object model, VisualElement view)
    {
        try
        {
            bool ret = false;

            // Cacher les messages d'erreur avant la validation
            Dispatcher.GetForCurrentThread()?.Dispatch(() =>
            {
                HideValidationFields(model, view);  // Passe le modèle à HideValidationFields
            });

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, errors, true);

            // Si la validation échoue, afficher les messages d'erreur
            if (!isValid)
            {
                ShowValidationFields(errors, model, view);
            }

            ret = errors.Count == 0;
            return ret;
        }
        catch (Exception ex)
        {
            // Gérer les exceptions
            Console.WriteLine($"Validation error: {ex.Message}");
            throw;
        }
    }

    // Méthode pour cacher les messages d'erreur
    //public static void HideValidationFields
    //  (object model, VisualElement view, string validationLabelSuffix = "Error")
    //{
    //    try
    //    {

    //        if (model == null) { return; }
    //        var properties = GetValidatablePropertyNames(model);
    //        foreach (var propertyName in properties)
    //        {
    //            var errorControlName =
    //            $"{propertyName.Replace(".", "_")}{validationLabelSuffix}";
    //            var control = view.FindByName<Label>(errorControlName);
    //            if (control != null)
    //            {
    //                control.IsVisible = false;
    //            }
    //        }

    //    }
    //    catch (Exception e)
    //    { throw; }
    //}
    public static void HideValidationFields(object model, VisualElement view, string validationLabelSuffix = "Error")
    {
        try
        {
            if (model == null) return;

            var properties = GetValidatablePropertyNames(model);

            foreach (var propertyName in properties)
            {
                var modelTypeName = model.GetType().Name;
                var errorControlName = $"{modelTypeName}_{propertyName}{validationLabelSuffix}";

                var control = view.FindByName<Label>(errorControlName);
                if (control != null)
                {
                    control.IsVisible = false;
                }
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private static IEnumerable<string> GetValidatablePropertyNames(object model)
    {
        var validatableProperties = model.GetType()
            .GetProperties()
            .Where(prop => prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any())
            .Select(p => p.Name); // <- Just property name
        return validatableProperties;
    }
    private static void ShowValidationFields(List<ValidationResult> errors, object model, VisualElement view, string validationLabelSuffix = "Error")
    {
        try
        {
            Dispatcher.GetForCurrentThread()?.Dispatch(() =>
            {
                foreach (var error in errors)
                {
                    var propertyName = error.MemberNames.FirstOrDefault();

                    if (string.IsNullOrEmpty(propertyName))
                        continue;

                    // Build the label name correctly
                    var modelTypeName = model.GetType().Name;
                    var controlName = $"{modelTypeName}_{propertyName}{validationLabelSuffix}";

                    var control = view.FindByName<Label>(controlName);

                    if (control != null)
                    {
                        control.Text = error.ErrorMessage;
                        control.IsVisible = true;
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing validation fields: {ex.Message}");
            throw;
        }
    }


    // Méthode pour afficher les messages d'erreur
    //private static void ShowValidationFields(List<ValidationResult> errors, object model, VisualElement view, string validationLabelSuffix = "Error")
    //{
    //    try
    //    {
    //        Dispatcher.GetForCurrentThread()?.Dispatch(() =>
    //        {
    //            foreach (var error in errors)
    //            {
    //                // Créer le nom du Label d'erreur
    //                var memberName = $"{model.GetType().Name}_{error.MemberNames.FirstOrDefault()}";
    //                memberName = memberName.Replace(".", "_");  // Remplacer les points par des underscores

    //                var errorControlName = $"{memberName}{validationLabelSuffix}";
    //                var control = view.FindByName<Label>(errorControlName);

    //                // Si le Label existe, mettre à jour son texte et le rendre visible
    //                if (control != null)
    //                {
    //                    control.Text = error.ErrorMessage;
    //                    control.IsVisible = true; 
    //                    // Afficher les erreurs dans l'interface
    //                }
    //            }
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error showing validation fields: {ex.Message}");
    //        throw;
    //    }
    //}
    private static List<PropertyInfo> GetValidatableProperties(object model)
    {
        try
        {
            var properties = model.GetType().GetProperties().Where(prop => prop.CanRead
&& prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any()
&& prop.GetIndexParameters().Length == 0).ToList();
            return properties;
        }
        catch (Exception)
        {

            throw;
        }
    }

    // Méthode pour récupérer les propriétés validables
    //private static IEnumerable<string> GetValidatablePropertyNames(object model)
    //{
    //    try
    //    {
    //        var validatableProperties = new List<string>();
    //        var properties = GetValidatableProperties(model);
    //        foreach (var propertyInfo in properties)
    //        {
    //            string errorControlName = "";


    //            errorControlName = $"{propertyInfo.DeclaringType.Name}.{propertyInfo.Name}";
    //            validatableProperties.Add(errorControlName);
    //        }
    //        return validatableProperties;
    //    }
    //    catch (Exception)
    //    { throw; }
    //}
}