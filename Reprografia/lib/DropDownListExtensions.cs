using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.ComponentModel;
using Reprografia.Models;

namespace Reprografia.lib
{
    public static class DropDownListExtensions
    {
        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "Selecione...", Value = "" } };
        public static MvcHtmlString EnumDropDownListFor<T>(this HtmlHelper<ItemAvaliacao> helper, Expression<Func<ItemAvaliacao, T>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<SelectListItem> items = new[]
            {
                new SelectListItem(){Text = "Aceitável", Value = "A", Selected = 'A'.Equals(metadata.Model)},
                new SelectListItem(){Text = "Não Aceitável", Value = "N", Selected = 'N'.Equals(metadata.Model)},
                new SelectListItem(){Text = "Não Aplicável", Value = "X", Selected = 'X'.Equals(metadata.Model)}
            };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return helper.DropDownListFor(expression, items);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, Type EnumType)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType;
            if (EnumType == null)
                enumType = GetNonNullableModelType(metadata);
            else
                enumType = EnumType;

            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = converter.ConvertToString(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(
                expression,
                items
                );
        }
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }
    }
}