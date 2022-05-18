using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace Pargoon.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;a&gt; elements.
    /// </summary>    
    [HtmlTargetElement("a", Attributes = AjaxAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxLoadingAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxMethodAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxModeAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxUpdateAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxOnBeginAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxOnCompleteAttributeName)]

    [HtmlTargetElement("a", Attributes = AjaxConfirmAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxOnFailureAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxOnSuccessAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxAllowCacheAttributeName)]

    [HtmlTargetElement("a", Attributes = AjaxAddDefaultFunctionsAttributeName)]
    [HtmlTargetElement("a", Attributes = AjaxAddDefaultValuesAttributeName)]

    [HtmlTargetElement("a", Attributes = ActionAttributeName)]
    [HtmlTargetElement("a", Attributes = ControllerAttributeName)]
    [HtmlTargetElement("a", Attributes = AreaAttributeName)]
    [HtmlTargetElement("a", Attributes = PageAttributeName)]
    [HtmlTargetElement("a", Attributes = PageHandlerAttributeName)]
    [HtmlTargetElement("a", Attributes = FragmentAttributeName)]
    [HtmlTargetElement("a", Attributes = HostAttributeName)]
    [HtmlTargetElement("a", Attributes = ProtocolAttributeName)]
    [HtmlTargetElement("a", Attributes = RouteAttributeName)]
    [HtmlTargetElement("a", Attributes = RouteValuesDictionaryName)]
    [HtmlTargetElement("a", Attributes = RouteValuesPrefix + "*")]
    public class XAnchorTagHelper : TagHelper
    {
        private const string AjaxAttributeName = "asp-ajax";

        private const string AjaxLoadingAttributeName = "asp-ajax-loading";
        private const string AjaxMethodAttributeName = "asp-ajax-method";
        private const string AjaxModeAttributeName = "asp-ajax-mode";
        private const string AjaxUpdateAttributeName = "asp-ajax-update";
        private const string AjaxOnBeginAttributeName = "asp-ajax-onBegin";
        private const string AjaxOnCompleteAttributeName = "asp-ajax-onComplete";

        private const string AjaxConfirmAttributeName = "asp-ajax-confirm";
        private const string AjaxOnFailureAttributeName = "asp-ajax-failure";
        private const string AjaxOnSuccessAttributeName = "asp-ajax-success";
        private const string AjaxAllowCacheAttributeName = "asp-ajax-cache";

        private const string AjaxAddDefaultFunctionsAttributeName = "asp-ajax-default-functions";
        private const string AjaxAddDefaultValuesAttributeName = "asp-ajax-default-values";
        #region default constants
        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string AreaAttributeName = "asp-area";
        private const string PageAttributeName = "asp-page";
        private const string PageHandlerAttributeName = "asp-page-handler";
        private const string FragmentAttributeName = "asp-fragment";
        private const string HostAttributeName = "asp-host";
        private const string ProtocolAttributeName = "asp-protocol";
        private const string RouteAttributeName = "asp-route";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";
        private const string Href = "href";
        #endregion

        private IDictionary<string, string> _routeValues;


        /// <summary>
        /// Creates a new <see cref="AnchorTagHelper"/>.
        /// </summary>
        /// <param name="generator">The <see cref="IHtmlGenerator"/>.</param>
        public XAnchorTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        #region default a link
        /// <inheritdoc />
        public override int Order => -1000;

        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// The name of the action method.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        /// <summary>
        /// The name of the controller.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        /// <summary>
        /// The name of the area.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get; set; }

        /// <summary>
        /// The name of the page.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, <see cref="Controller"/>
        /// is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(PageAttributeName)]
        public string Page { get; set; }

        /// <summary>
        /// The name of the page handler.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, or <see cref="Controller"/>
        /// is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(PageHandlerAttributeName)]
        public string PageHandler { get; set; }

        /// <summary>
        /// The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.
        /// </summary>
        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        /// <summary>
        /// The host name.
        /// </summary>
        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }

        /// <summary>
        /// The URL fragment name.
        /// </summary>
        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }

        /// <summary>
        /// Name of the route.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if one of <see cref="Action"/>, <see cref="Controller"/>, <see cref="Area"/> 
        /// or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }

        /// <summary>
        /// Additional parameters for the route.
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        #endregion

        #region for Ajax link
        [HtmlAttributeName(AjaxAttributeName)]
        public string Ajax { get { return isAjax.ToString(); } set { if (value.ToLower() == "true") isAjax = true; else isAjax = false; } }
        bool isAjax = false;

        [HtmlAttributeName(AjaxLoadingAttributeName)]
        public string AjaxLoading { get; set; }
        [HtmlAttributeName(AjaxMethodAttributeName)]
        public string Method { get; set; }
        [HtmlAttributeName(AjaxModeAttributeName)]
        public string Mode { get; set; }
        [HtmlAttributeName(AjaxOnBeginAttributeName)]
        public string OnBegin { get; set; }
        [HtmlAttributeName(AjaxOnCompleteAttributeName)]
        public string OnComplete { get; set; }
        [HtmlAttributeName(AjaxUpdateAttributeName)]
        public string Update { get; set; }

        [HtmlAttributeName(AjaxOnFailureAttributeName)]
        public string OnFailure { get; set; }

        [HtmlAttributeName(AjaxOnSuccessAttributeName)]
        public string OnSuccess { get; set; }

        [HtmlAttributeName(AjaxConfirmAttributeName)]
        public string Confirm { get; set; }
        [HtmlAttributeName(AjaxAllowCacheAttributeName)]
        public bool? AllowCache { get; set; }
        [HtmlAttributeName(AjaxAddDefaultValuesAttributeName)]
        public string AjaxDefaultValues
        {
            get
            {
                return _defaultValues.ToString();
            }
            set
            {
                if (value.ToLower() == "true")
                {
                    _defaultValues = true;
                    isAjax = true;
                }
                else _defaultValues = false;
            }
        }
        [HtmlAttributeName(AjaxAddDefaultFunctionsAttributeName)]
        public string AjaxDefaultFunctions
        {
            get
            {
                return _defaultFunctions.ToString();
            }
            set
            {
                if (value.ToLower() == "true")
                {
                    _defaultFunctions = true;
                    isAjax = true;
                }
                else _defaultFunctions = false;
            }
        }
        bool _defaultValues = true;
        bool _defaultFunctions = true;

        #endregion
        public override void Init(TagHelperContext context)
        {
            if (_defaultValues)
            {
                if (string.IsNullOrEmpty(AjaxLoading))
                    AjaxLoading = "#ajaxloading";
                if (string.IsNullOrEmpty(Method))
                    Method = "post";
                if (string.IsNullOrEmpty(Update))
                    Update = "#modalContent";
            }
            else
            {
                if (string.IsNullOrEmpty(Method))
                    Method = "post";
            }
            if (_defaultFunctions)
            {
                if (string.IsNullOrEmpty(OnBegin))
                    OnBegin = "showModal();";
            }
            if (string.IsNullOrEmpty(Mode))
                Mode = "replace";

            base.Init(context);
        }
        /// <inheritdoc />
        /// <remarks>Does nothing if user provides an <c>href</c> attribute.</remarks>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            if (isAjax)
            {
                output.Attributes.Add("data-ajax", "true");
                output.Attributes.Add("data-ajax-method", Method);
                output.Attributes.Add("data-ajax-mode", Mode);

                if (!string.IsNullOrEmpty(Update))
                    output.Attributes.Add("data-ajax-update", Update);

                if (!string.IsNullOrEmpty(AjaxLoading))
                    output.Attributes.Add("data-ajax-loading", AjaxLoading);

                if (!string.IsNullOrEmpty(OnBegin))
                    output.Attributes.Add("data-ajax-begin", OnBegin);

                if (!string.IsNullOrEmpty(OnComplete))
                    output.Attributes.Add("data-ajax-complete", OnComplete);

                if (!string.IsNullOrEmpty(Confirm))
                    output.Attributes.Add("data-ajax-confirm", Confirm);

                if (!string.IsNullOrEmpty(OnSuccess))
                    output.Attributes.Add("data-ajax-success", OnSuccess);

                if (!string.IsNullOrEmpty(OnFailure))
                    output.Attributes.Add("data-ajax-failure", OnFailure);

                if (AllowCache.HasValue)
                    output.Attributes.Add("data-ajax-cache", AllowCache);

            }
            // If "href" is already set, it means the user is attempting to use a normal anchor.
            if (output.Attributes.ContainsName(Href))
            {
                if (Action != null ||
                    Controller != null ||
                    Area != null ||
                    Page != null ||
                    PageHandler != null ||
                    Route != null ||
                    Protocol != null ||
                    Host != null ||
                    Fragment != null ||
                    (_routeValues != null && _routeValues.Count > 0))
                {
                    //throw new InvalidOperationException();
                }

                return;
            }

            var routeLink = Route != null;
            var actionLink = Controller != null || Action != null;
            var pageLink = Page != null || PageHandler != null;

            if ((routeLink && actionLink) || (routeLink && pageLink) || (actionLink && pageLink))
            {
                var message = string.Join(
                    Environment.NewLine,
                    //Resources.FormatCannotDetermineAttributeFor(Href, "<a>"),
                    RouteAttributeName,
                    ControllerAttributeName + ", " + ActionAttributeName,
                    PageAttributeName + ", " + PageHandlerAttributeName);

                throw new InvalidOperationException(message);
            }

            RouteValueDictionary routeValues = null;
            if (_routeValues != null && _routeValues.Count > 0)
            {
                routeValues = new RouteValueDictionary(_routeValues);
            }

            if (Area != null)
            {
                // Unconditionally replace any value from asp-route-area.
                if (routeValues == null)
                {
                    routeValues = new RouteValueDictionary();
                }
                routeValues["area"] = Area;
            }

            TagBuilder tagBuilder;
            if (pageLink)
            {
                tagBuilder = Generator.GeneratePageLink(
                    ViewContext,
                    linkText: string.Empty,
                    pageName: Page,
                    pageHandler: PageHandler,
                    protocol: Protocol,
                    hostname: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
            else if (routeLink)
            {
                tagBuilder = Generator.GenerateRouteLink(
                    ViewContext,
                    linkText: string.Empty,
                    routeName: Route,
                    protocol: Protocol,
                    hostName: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: null);
            }
            else
            {
                tagBuilder = Generator.GenerateActionLink(
                   ViewContext,
                   linkText: string.Empty,
                   actionName: Action,
                   controllerName: Controller,
                   protocol: Protocol,
                   hostname: Host,
                   fragment: Fragment,
                   routeValues: routeValues,
                   htmlAttributes: null);
            }

            output.MergeAttributes(tagBuilder);
        }
    }

}
