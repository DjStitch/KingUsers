using ApiClient;
using Radzen;

namespace KingUsersApp.Helpers;

public static class ApiHelper
{
    public static async Task<T?> ExecuteCallGuardedAsync<T>(
        Func<Task<T>> call,
        NotificationService notify,
        string? successMessage = null,
        bool noError = false,
        DialogService? dialogService = null)
    {
        try
        {
            var result = await call();

            if (string.IsNullOrWhiteSpace(successMessage)) return result;
            notify.Notify(NotificationSeverity.Success, "KingsResponse", successMessage);

            return result;
        }

        catch (ApiException ex)
        {
            if (noError == false) notify.Notify(NotificationSeverity.Error, ex.Message);
        }
        catch (Exception ex)
        {
            if (noError == false) notify.Notify(NotificationSeverity.Error, ex.Message);
        }

        return default;
    }


    public static async Task<bool> ExecuteCallGuardedAsync(
        Func<Task> call,
        NotificationService snackBar,
        string? successMessage = null, bool noError = false, DialogService dialogService = null)
    {
        try
        {
            await call();

            if (!string.IsNullOrWhiteSpace(successMessage))
                snackBar.Notify(NotificationSeverity.Success, successMessage);

            return true;
        }
        catch (Exception ex)
        {
            if (noError == false) snackBar.Notify(NotificationSeverity.Error, ex.Message);
        }

        return default;
    }
}