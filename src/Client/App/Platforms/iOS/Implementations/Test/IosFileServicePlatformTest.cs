﻿using Functionland.FxFiles.Client.Shared.Services;
using Functionland.FxFiles.Client.Shared.TestInfra.Implementations;

namespace Functionland.FxFiles.Client.App.Platforms.iOS.Implementations.Test;

public partial class IosFileServicePlatformTest : FileServicePlatformTest
{
    [AutoInject] public FakeFileServiceFactory FakeFileServiceFactory { get; set; } = default!;
    public override string Title => "IosFileService Test";

    public override string Description => "Tests the common features of this FileService";

    protected override IFileService OnGetFileService()
    {
        return FakeFileServiceFactory.CreateTypical();
    }

    protected override string OnGetTestsRootPath() => "fakeroot";
}
