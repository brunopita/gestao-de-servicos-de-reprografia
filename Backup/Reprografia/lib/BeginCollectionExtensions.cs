using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

//Source
//http://ivanz.com/2011/06/16/editing-variable-length-reorderable-collections-in-asp-net-mvc-part-1/

namespace Reprografia.lib
{
    public static class BeginCollectionExtensions
    {
        public static string MustacheTemplatingEnabledKey
        {
            get
            {
                return "MustacheTemplatingEnabled";
            }
        }
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName, bool template = false)
        {
            string collectionIndexFieldName = String.Format("{0}.Index", collectionName);
            string itemIndex = null;
            //if (html.ViewData.ContainsKey(MustacheTemplatingEnabledKey))
            if (template)
                itemIndex = "{{index}}";
            else
                itemIndex = Guid.NewGuid().ToString();


            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex);

            TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
                { "name", collectionIndexFieldName },
                { "value", itemIndex },
                { "type", "hidden" },
                { "autocomplete", "off" }
            });

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }
        //public static void RenderCollectionItemMustacheTemplate<TModel, TCollectionItem>(this HtmlHelper<TModel> html,
        //                                                                            string partialViewName,
        //                                                                            TCollectionItem modelDefaultValues)
        //{
        //    ViewDataDictionary<TCollectionItem> viewData = new ViewDataDictionary<TCollectionItem>(modelDefaultValues);
        //    viewData.Add(MustacheTemplatingEnabledKey, true);
        //    viewData.Add("template", true);
        //    html.RenderPartial(partialViewName, modelDefaultValues, viewData);
        //}
        //public static void RenderCollectionItemMustacheTemplate<TModel, TCollection>(this HtmlHelper<TModel> html,
        //                                                                            string partialViewName,
        //                                                                            TCollection modelDefaultValues,
        //                                                                            ViewDataDictionary viewData)
        //{
        //    viewData.Add(MustacheTemplatingEnabledKey, true);
        //    html.RenderPartial(partialViewName, modelDefaultValues, viewData);
        //}

        private class CollectionItemNamePrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousPrefix;

            public CollectionItemNamePrefixScope(TemplateInfo templateInfo, string collectionItemName)
            {
                this._templateInfo = templateInfo;

                _previousPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = collectionItemName;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousPrefix;
            }
        }
        public static MvcHtmlString CollectionItemJQueryTemplate<TModel, TCollectionItem>(this HtmlHelper<TModel> html,
                                                                                    string partialViewName,
                                                                                    TCollectionItem modelDefaultValues)
        {
            ViewDataDictionary<TCollectionItem> viewData = new ViewDataDictionary<TCollectionItem>(modelDefaultValues);
            //viewData.Add(JQueryTemplatingEnabledKey, true);
            return html.Partial(partialViewName, modelDefaultValues, viewData);
        }
    }
}