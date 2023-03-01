namespace Functionland.FxFiles.Client.Shared.Pages;

public partial class SettingsThemePage
{
    [AutoInject] private ThemeInterop ThemeInterop = default!;

    private FxTheme CurrentTheme { get; set; }

    protected override Task OnInitAsync()
    {
        GoBackService.SetState((Task () =>
        {
            HandleToolbarBack();
            StateHasChanged();
            return Task.CompletedTask;
        }), true, false);

        return base.OnInitAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            CurrentTheme = await ThemeInterop.GetThemeAsync();
            StateHasChanged();
        }
    }

    private async Task ChangeThemeAsync()
    {
        await ThemeInterop.SetThemeAsync(CurrentTheme);
    }

    private void HandleToolbarBack()
    {
        NavigationManager.NavigateTo("settings", false, true);
    }
}