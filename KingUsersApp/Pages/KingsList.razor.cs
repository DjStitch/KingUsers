using ApiClient;
using KingUsersApp.Helpers;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace KingUsersApp.Pages;

public partial class KingsList : ComponentBase
{
    private  IEnumerable<User> _kingUsers = new List<User>();

    private RadzenDataGrid<User> _kingUsersGrid = new();
    private User? _userToInsert = new();
    private User? _userToUpdate = new();

    [Inject] public IUsersClient UsersClient { get; set; } = default!;
    [Inject] public NotificationService Notify { get; set; } = default!;


    private void Reset()
    {
        _userToInsert = null;
        _userToUpdate = null;
    }

    private async Task EditRow(User user)
    {
        _userToUpdate = user;
        await _kingUsersGrid.EditRow(user);
    }

    private async void OnUpdateRow(User user)
    {
        if (user == _userToInsert) _userToInsert = null;

        _userToUpdate = null;

        var response =
            await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.UsersPutAsync(user.UserId, user), Notify,
                "King updated");
    }

    private async Task SaveRow(User user)
    {
        await _kingUsersGrid.UpdateRow(user);
    }

    private async void CancelEdit(User user)
    {
        if (user == _userToInsert) _userToInsert = null;

        _userToUpdate = null;

        _kingUsersGrid.CancelEditRow(user);

        var response =
            await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.UsersPutAsync(user.UserId, user), Notify,
                "King cancel");
    }

    private async Task DeleteRow(User user)
    {
        if (user == _userToInsert) _userToInsert = null;

        if (user == _userToUpdate) _userToUpdate = null;

        var response =
            await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.UsersDeleteAsync(user.UserId), Notify,
                "King is eliminated");

        if (response)
        {
            await _kingUsersGrid.Reload();
        }
        else
        {
            _kingUsersGrid.CancelEditRow(user);
            await _kingUsersGrid.Reload();
        }
    }

    private async Task InsertRow()
    {
        _userToInsert = new User();
        await _kingUsersGrid.InsertRow(_userToInsert);
    }

    private async void OnCreateRow(User user)
    {
        var response =
            await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.UsersPostAsync(user), Notify,
                "A new King has has been crowd, hail the KING!!");

        _userToInsert = null;
    }

    void OnRender(DataGridRenderEventArgs<User> args)
    {
        if (args.FirstRender)
        {
            args.Grid.Groups.Add(new GroupDescriptor() { Title = "Customer", Property = "Customer.CompanyName", SortOrder = SortOrder.Descending });
            StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var response =
            await ApiHelper.ExecuteCallGuardedAsync(() => UsersClient.GetAllUsersAsync(1, 50), Notify,
                "Kings has been sum-mend");

        if (response != null)
        {
            _kingUsers = response.ToList();
            if (_kingUsers.Any())
            {
                Reset();
            }
        }


    }
}