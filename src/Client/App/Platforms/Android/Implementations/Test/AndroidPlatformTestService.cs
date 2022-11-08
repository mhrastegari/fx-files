﻿
using Functionland.FxFiles.Client.Shared.TestInfra.Contracts;
using Functionland.FxFiles.Client.Shared.TestInfra.Implementations;

namespace Functionland.FxFiles.Client.App.Platforms.Android.Implementations.Test;

public partial class AndroidPlatformTestService : PlatformTestService
{
    [AutoInject] InternalAndroidFileServicePlatformTest InternalAndroidFileServicePlatformTest { get; set; }
    [AutoInject] ExternalAndroidFileServicePlatformTest ExternalAndroidFileServicePlatformTest { get; set; }
    [AutoInject] AndroidImageThumbnailPluginPlatformTest<ILocalDeviceFileService> LocalAndroidImageThumbnailPluginPlatformTest { get; set; }
    [AutoInject] AndroidImageThumbnailPluginPlatformTest<IFulaFileService> FulaAndroidImageThumbnailPluginPlatformTest { get; set; }

    protected override List<IPlatformTest> OnGetTests()
    {
        return new List<IPlatformTest>()
        {
            InternalAndroidFileServicePlatformTest,
            ExternalAndroidFileServicePlatformTest,
            LocalAndroidImageThumbnailPluginPlatformTest,
            FulaAndroidImageThumbnailPluginPlatformTest
        };
    }
}
