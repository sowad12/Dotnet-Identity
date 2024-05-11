﻿//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;

//namespace WC.Addon.ClubeezWF.Main.Extensions
//{
//    public static class PagingExtensions
//    {
//        #region HtmlHelper extensions

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null);
//        }

//        public static string PagerWithScript(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string formName, string url, string divName)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null, formName, url, divName);
//        }

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null);
//        }

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values));
//        }

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values));
//        }

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
//        {
//            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary);
//        }

//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary)
//        {
//            if (valuesDictionary == null)
//            {
//                valuesDictionary = new RouteValueDictionary();
//            }
//            if (actionName != null)
//            {
//                if (valuesDictionary.ContainsKey("action"))
//                {
//                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
//                }
//                valuesDictionary.Add("action", actionName);
//            }
//            var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary);
//            return pager.RenderHtml("", "", "");
//        }


//        public static string Pager(this IHtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string formName, string url, string divName)
//        {
//            if (valuesDictionary == null)
//            {
//                valuesDictionary = new RouteValueDictionary();
//            }
//            if (actionName != null)
//            {
//                if (valuesDictionary.ContainsKey("action"))
//                {
//                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
//                }
//                valuesDictionary.Add("action", actionName);
//            }
//            var pager = new Pager(htmlHelper.ViewContext, pageSize, currentPage, totalItemCount, valuesDictionary);

//            return pager.RenderHtml(formName, url, divName);
//        }


//        #endregion

//        #region IQueryable<T> extensions

//        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
//        {
//            return new PagedList<T>(source, pageIndex, pageSize);
//        }

//        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
//        {
//            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
//        }

//        #endregion

//        #region IEnumerable<T> extensions

//        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
//        {
//            return new PagedList<T>(source, pageIndex, pageSize);
//        }

//        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
//        {
//            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
//        }

//        #endregion
//    }
//}