﻿using Functionland.FxFiles.App.Components.Modal;
using Functionland.FxFiles.Shared.Enums;

namespace Functionland.FxFiles.App.Components;

public partial class FilterArtifactModal
{
    private bool _isModalOpen;
    private TaskCompletionSource<FileCategoryType?>? _tcs;

    [Parameter] public FileCategoryType? CurrentFilter { get; set; }

    public async Task<FileCategoryType?> ShowAsync()
    {
        _tcs?.SetCanceled();

        _isModalOpen = true;
        StateHasChanged();

        _tcs = new TaskCompletionSource<FileCategoryType?>();

        return await _tcs.Task;
    }

    private void HandleFilterItemClick(FileCategoryType? fileCategoryType)
    {
        _tcs!.SetResult(fileCategoryType);
        _tcs = null;
        _isModalOpen = false;
    }

    private void HandleClose()
    {
        _tcs!.SetResult(CurrentFilter);
        _tcs = null;
        _isModalOpen = false;
    }
}
