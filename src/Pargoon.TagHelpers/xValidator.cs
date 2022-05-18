using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Pargoon.TagHelpers
{
    [HtmlTargetElement("xValidator")]
    public class AjaxformValidationTagHelper : TagHelper
    {
        private const string FormIdAttributeName = "asp-form-id";
        private const string FormClassNameAttributeName = "asp-form-class";

        [HtmlAttributeName(FormIdAttributeName)]
        public string formId { get; set; }
        [HtmlAttributeName(FormClassNameAttributeName)]
        public string formClassId { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;
            if (!string.IsNullOrEmpty(formClassId))
            {
                output.Content.AppendHtml("$().ready(function () { var form = $(\"." + formClassId + "\");  form.unbind(); form.data(\"validator\", null); $.validator.unobtrusive.parse(document); form.validate(form.data(\"unobtrusiveValidation\").options); });");
            }
            else if (!string.IsNullOrEmpty(formId))
                output.Content.AppendHtml("$().ready(function () { var form = $(\"#" + formId + "\");  form.unbind(); form.data(\"validator\", null); $.validator.unobtrusive.parse(document); form.validate(form.data(\"unobtrusiveValidation\").options); });");
            else
                output.Content.AppendHtml("$().ready(function () { var form = $(\"form\");  form.unbind(); form.data(\"validator\", null); $.validator.unobtrusive.parse(document); form.validate(form.data(\"unobtrusiveValidation\").options); });");
            return base.ProcessAsync(context, output);
        }
    }
}
