﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionland.FxFiles.Shared.Models
{
    [Table("PinnedArtifact")]
    public class PinnedArtifact
    {
        [Key]
        public int Id { get; set; }
        public string? FullPath { get; set; }
        public string? ThumbnailPath { get; set; }
        public string? ContentHash { get; set; }
        public FsFileProviderType? ProviderType { get; set; }
        public FsArtifactType? FsArtifactType { get; set; }
        public string? ArtifactName { get; set; }
        public long? PinEpochTime { get; set; }
    }
}
