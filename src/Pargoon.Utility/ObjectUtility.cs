using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pargoon.Utility
{

    public class ModelValidation
    {
        public static bool ValidateModel<T>(T model, out List<ValidationResult> validationResults) where T : class
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, validationResults, true);
        }
    }

    public static class ObjectDataConverter
    {
        public static T BuildNewObject<T, TS>(TS source)
                where T : new()
                where TS : class
        {
            var res = new T();
            res.CopyPropertiesFrom(source);
            return res;
        }

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }
        public static void CopyPropertiesFrom<TU, T>(this TU dest, T source)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }
        }
    }
}
