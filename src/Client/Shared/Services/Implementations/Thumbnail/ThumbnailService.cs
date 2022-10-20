﻿namespace Functionland.FxFiles.Client.Shared.Services.Implementations;

public abstract class ThumbnailService
{
    public IFileCacheService FileCacheService { get; set; } = default!;
    public IEnumerable<IThumbnailPlugin> ThumbnailPlugins { get; set; }

    public ThumbnailService(IFileCacheService fileCacheService, IEnumerable<IThumbnailPlugin> thumbnailPlugins)
    {
        FileCacheService = fileCacheService;
        ThumbnailPlugins = thumbnailPlugins;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uniqueFileName">adfadgfasdfasdf52465s4fd6as5f4fa6sd5f4as6d5f.pdf</param>
    /// <param name="fileStream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>{cache}/adfadgfasdfasdf52465s4fd6as5f4fa6sd5f4as6d5f.jpg</returns>
    protected async Task<string?> GetOrCreateThumbnailAsync(
        CacheCategoryType cacheCategoryType, 
        string uniqueName, 
        Func<Task<Stream>>? getFileStreamFunc,
        string? filePath, CancellationToken? cancellationToken = null)
    {
        var cacheUniqueName = Path.ChangeExtension(uniqueName, "jpg");
        return await FileCacheService.GetOrCreateCachedFileAsync(
            cacheCategoryType,
            cacheUniqueName,
            async (cacheFilePath) => await OnCreateThumbnailAsync(
                                                uniqueName, 
                                                cacheFilePath, 
                                                getFileStreamFunc, 
                                                filePath,
                                                cancellationToken),
            cancellationToken);
    }

    private async Task<bool> OnCreateThumbnailAsync(
        string uniqueFileName, 
        string thumbnailFilePath, 
        Func<Task<Stream>>? getFileStreamFunc, 
        string? filePath, 
        CancellationToken? cancellationToken = null)
    {
        ThumbnailSourceType sourceType;

        if (getFileStreamFunc is not null)
            sourceType = ThumbnailSourceType.Stream;
        else if (filePath is not null)
            sourceType = ThumbnailSourceType.FilePath;
        else
            throw new InvalidOperationException("Both stream and filePath are null, which is not valid to create a thumbnail.");

        var plugin = GetRelatedPlugin(uniqueFileName, sourceType);
        
        if (plugin is null && (sourceType == ThumbnailSourceType.Stream && filePath is not null))
            plugin = GetRelatedPlugin(uniqueFileName, ThumbnailSourceType.FilePath);

        if (plugin is null)
            return false;

        Stream? stream = null;
        if (getFileStreamFunc is not null)
            stream = await getFileStreamFunc();
        var thumbnailStream = await plugin.CreateThumbnailAsync(stream, filePath, cancellationToken);

        // write stream
        using (var fileStream = File.Create(thumbnailFilePath))
        {
            thumbnailStream.Seek(0, SeekOrigin.Begin);
            thumbnailStream.CopyTo(fileStream);
        }

        return true;
    }

    protected virtual IThumbnailPlugin? GetRelatedPlugin(string uri, ThumbnailSourceType sourceType)
    {
        var extension = Path.GetExtension(uri);
        var plugin = ThumbnailPlugins.FirstOrDefault(plugin => plugin.IsSupported(extension, sourceType));
        return plugin;
    }
}
