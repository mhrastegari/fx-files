﻿using Functionland.FxFiles.Client.Shared.Components;
using Functionland.FxFiles.Client.Shared.Components.Modal;
using Functionland.FxFiles.Client.Shared.Services.Common;
using Prism.Events;

namespace Functionland.FxFiles.Client.Shared.Pages;

public partial class HomePage
{
    [AutoInject] public ILocalDeviceFileService LocalDeviceFileService { get; set; } = default!;
    [AutoInject] public IEventAggregator EventAggregator { get; set; } = default!;

    protected override async Task OnInitAsync()
    {
        _ = EventAggregator
                .GetEvent<IntentReceiveEvent>()
                .Subscribe(
                    ApplyIntentArtifactIfNeeded,
                    ThreadOption.BackgroundThread, keepSubscriberReferenceAlive: true);

        if (!string.IsNullOrWhiteSpace(AppStateStore.IntentFileUrl))
            return;

        await base.OnInitAsync();

        if (IsiOS)
            NavigationManager.NavigateTo("settings", false, true);
        else
            NavigationManager.NavigateTo("mydevice", false, true);
    }


    private void ApplyIntentArtifactIfNeeded(IntentReceiveEvent obj)
    {
        if (string.IsNullOrWhiteSpace(AppStateStore.IntentFileUrl))
            return;

        var encodedArtifactPath = Uri.EscapeDataString(AppStateStore.IntentFileUrl);
        NavigationManager.NavigateTo($"mydevice?encodedArtifactPath={encodedArtifactPath}", false, true);
    }
}