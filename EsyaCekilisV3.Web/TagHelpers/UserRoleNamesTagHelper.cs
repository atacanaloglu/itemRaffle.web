using EsyaCekilisV3.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace EsyaCekilisV3.Web.TagHelpers
{
    public class UserRoleNamesTagHelper:TagHelper
    {
        public string UserId { get; set; } = null!;

        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
          
           var userRoles = await _userManager.GetRolesAsync(user!);

            var stringBuilder = new StringBuilder(); //stringleri uç uca ekleyeceğimiz için builder

            userRoles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge bg-secondary mx-1'>{x.ToLower()}</span>"); //bu @ işareti kodu istediğimiz gibi alt alta yazmamızı sağlar
            });

                output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
