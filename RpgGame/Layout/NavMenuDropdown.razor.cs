using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using RpgGame.Services;

namespace RpgGame.Layout;

public sealed partial class NavMenuDropdown: IDisposable
{
    [Inject] public CurrentPlayer CurrentPlayer { get; set; } = null!;
    [Inject] public RememberMe RememberMe { get; set; } = null!;
    [Inject] public NavigationManager Navigator { get; set; } = null!;

    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Navigator.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        Navigator.LocationChanged -= OnLocationChanged;
    }

    private void OnLocationChanged(object? o, LocationChangedEventArgs a) => DoClose();
    
    private async Task DoClose() => await IsOpenChanged.InvokeAsync(false);
    
    private async Task DoLogOut()
    {
        await RememberMe.Forget();
        await CurrentPlayer.LogOutAsync();
        await DoClose();
    }
}