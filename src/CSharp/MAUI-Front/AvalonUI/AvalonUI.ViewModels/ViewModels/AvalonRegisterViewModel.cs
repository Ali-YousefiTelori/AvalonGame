﻿using AvalonUI.Helpers;
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
            ApplicationDataManager.Current.ApplicationData.UserName = UserName;
            ApplicationDataManager.Current.ApplicationData.Password = Password;
            await ApplicationDataManager.Current.Save();

            await DisplayAlert("موفق", "ثبت نام شما با موفقیت انجام شد.", "باشه");
            await NavigationManagerBase.Current.PopAsync();
        }
        else if (result.Error.FailedReasonType == EasyMicroservices.ServiceContracts.FailedReasonType.Duplicate)
        {
            await DisplayError("نام کاربری قبلا توسط شخص دیگری انتخاب شده است، لطفا نام کاربری دیگری انتخاب کنید و مجدد دکمه‌ی ثبت نام را کلیک کنید.");
        }
        else
        {
            await DisplayError(GetLanguage(result.Error.Message));
        }
        return result;
    }
}
