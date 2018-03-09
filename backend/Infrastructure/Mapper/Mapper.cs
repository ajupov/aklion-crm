using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.DateTime;

namespace Infrastructure.Mapper
{
    public static class Mapper
    {
        public static TNewParameterModel MapParameterNew<TNewParameterModel>(this object oldParameterModel)
            where TNewParameterModel : new()
        {
            if (oldParameterModel == null)
            {
                return default(TNewParameterModel);
            }

            var newParameterModel = new TNewParameterModel();

            var oldparameterModelProperties = oldParameterModel.GetType().GetProperties();
            var newParameterModelProperties = typeof(TNewParameterModel).GetProperties();

            Map(oldParameterModel, newParameterModel, oldparameterModelProperties, newParameterModelProperties);

            return newParameterModel;
        }

        public static TNewModel MapNew<TNewModel>(this object oldModel) where TNewModel : new()
        {
            if (oldModel == null)
            {
                return default(TNewModel);
            }

            var newModel = new TNewModel();

            var oldModelProperties = oldModel.GetType().GetProperties();
            var newModelProperties = typeof(TNewModel).GetProperties();

            Map(oldModel, newModel, oldModelProperties, newModelProperties);
            SetCreateDateNow(newModel, newModelProperties);

            return newModel;
        }

        public static List<TNewModel> MapListNew<TNewModel>(this IEnumerable<object> oldModels) where TNewModel : new()
        {
            if (oldModels == null)
            {
                return null;
            }

            var oldModelsList = oldModels.ToList();
            var firstOldModel = oldModelsList.FirstOrDefault();
            if (firstOldModel == null)
            {
                return null;
            }

            var newModels = new List<TNewModel>();

            var oldModelProperties = firstOldModel.GetType().GetProperties();
            var newModelProperties = typeof(TNewModel).GetProperties();

            foreach (var oldModel in oldModelsList)
            {
                var newModel = new TNewModel();

                Map(oldModel, newModel, oldModelProperties, newModelProperties);

                newModels.Add(newModel);
            }

            return newModels;
        }

        public static TToModel MapFrom<TToModel>(this TToModel toModel, object fromModel)
        {
            if (fromModel == null)
            {
                return toModel;
            }

            var fromModelProperties = fromModel.GetType().GetProperties();
            var toModelProperties = typeof(TToModel).GetProperties();

            Map(fromModel, toModel, fromModelProperties, toModelProperties);
            SetModifyDateNow(toModel, toModelProperties);

            return toModel;
        }

        private static void Map(object fromModel, object toModel, IEnumerable<PropertyInfo> fromModelProperties,
            IEnumerable<PropertyInfo> toModelProperties)
        {
            var newModelPropertiesList = toModelProperties.ToList();

            foreach (var oldModelProperty in fromModelProperties)
            {
                var newModelProperty = newModelPropertiesList.FirstOrDefault(p => p.Name == oldModelProperty.Name);
                if (newModelProperty == null)
                {
                    continue;
                }

                if (!newModelProperty.CanWrite)
                {
                    continue;
                }

                if (newModelProperty.PropertyType == oldModelProperty.PropertyType)
                {
                    var oldModelPropertyValue = oldModelProperty.GetValue(fromModel);
                    newModelProperty.SetValue(toModel, oldModelPropertyValue);

                    continue;
                }

                if (oldModelProperty.PropertyType == typeof(System.DateTime) && newModelProperty.PropertyType == typeof(string))
                {
                    var oldModelPropertyValue = (System.DateTime) oldModelProperty.GetValue(fromModel);
                    newModelProperty.SetValue(toModel, oldModelPropertyValue.ToDateTimeString());

                    continue;
                }

                if (oldModelProperty.PropertyType == typeof(System.DateTime?) && newModelProperty.PropertyType == typeof(string))
                {
                    var oldModelPropertyValue = (System.DateTime?) oldModelProperty.GetValue(fromModel);
                    if (oldModelPropertyValue.HasValue)
                    {
                        newModelProperty.SetValue(toModel, oldModelPropertyValue.Value.ToDateTimeString());
                    }

                    continue;
                }

                if (oldModelProperty.PropertyType == typeof(string) && (newModelProperty.PropertyType == typeof(System.DateTime) || newModelProperty.PropertyType == typeof(System.DateTime?)))
                {
                    var oldModelPropertyValue = oldModelProperty.GetValue(fromModel)?.ToString();
                    if (!string.IsNullOrWhiteSpace(oldModelPropertyValue))
                    {
                        var value = oldModelPropertyValue.ToNullableDateTime() ?? oldModelPropertyValue.ToNullableDate();
                        newModelProperty.SetValue(toModel, value);
                    }
                }
            }
        }

        private static void SetCreateDateNow(object model, IEnumerable<PropertyInfo> properties)
        {
            SetPropertyValueNow(model, properties, "CreateDate");
        }

        private static void SetModifyDateNow(object model, IEnumerable<PropertyInfo> properties)
        {
            SetPropertyValueNow(model, properties, "ModifyDate");
        }

        private static void SetPropertyValueNow(object model, IEnumerable<PropertyInfo> properties, string propertyName)
        {
            var property = properties.FirstOrDefault(p => p.Name == propertyName);
            if (property == null)
            {
                return;
            }

            if (property.PropertyType != typeof(System.DateTime?) && property.PropertyType != typeof(System.DateTime))
            {
                return;
            }

            property.SetValue(model, System.DateTime.Now);
        }
    }
}