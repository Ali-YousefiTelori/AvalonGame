using EasyMicroservices.Security;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores.Navigations;
using EasyMicroservices.UI.Identity.ViewModels.Authentications;
using Identity.GeneratedServices;

namespace AvalonUI.ViewModels;
public class AvalonRegisterViewModel : RegisterViewModel
{
    public AvalonRegisterViewModel(AuthenticationClient authenticationClient, ISecurityProvider securityProvider) : base(authenticationClient, securityProvider)
    {

    }

    public override async Task<MessageContract<RegisterResponseContract>> Register()
    {
        var result = await base.Register();
        if (result.IsSuccess)
        {
            await DisplayError("ثبت نام شما با موفقیت انجام شد.");
            await NavigationManagerBase.Current.PopAsync();
        }
        else if (result.Error.FailedReasonType == EasyMicroservices.ServiceContracts.FailedReasonType.Duplicate)
        {
            await DisplayError("نام کاربری قبلا توسط شخص دیگری انتخاب شده است، لطفا نام کاربری دیگری انتخاب کنید و مجدد دکمه‌ی ثبت نام را کلیک کنید.");
        }
        return result;
    }
}
