/* SubTube Phase 1A — Initial Schema
   Applied by SubTubeManager (IInstallable) via EF Core migrations.
   Audit columns (CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) come from ModelBase.
*/

CREATE TABLE [SubTubeJobs] (
    [JobId]               INT            NOT NULL IDENTITY(1,1),
    [UserId]              INT            NOT NULL,
    [SubstackPostUrl]     NVARCHAR(MAX)  NOT NULL,
    [SubstackPostTitle]   NVARCHAR(MAX)  NOT NULL,
    [SubstackPostContent] NVARCHAR(MAX)  NOT NULL,
    [Status]              INT            NOT NULL DEFAULT 0,
    [CompletedOn]         DATETIME2      NULL,
    [AudioBlobPath]       NVARCHAR(MAX)  NULL,
    [VideoBlobPath]       NVARCHAR(MAX)  NULL,
    [YouTubeVideoId]      NVARCHAR(MAX)  NULL,
    [ErrorMessage]        NVARCHAR(MAX)  NULL,
    [DurationSeconds]     INT            NULL,
    [CreatedBy]           NVARCHAR(256)  NOT NULL DEFAULT '',
    [CreatedOn]           DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]          NVARCHAR(256)  NOT NULL DEFAULT '',
    [ModifiedOn]          DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_SubTubeJobs] PRIMARY KEY ([JobId])
);

CREATE TABLE [SubstackFeeds] (
    [FeedId]          INT            NOT NULL IDENTITY(1,1),
    [UserId]          INT            NOT NULL,
    [SubstackUrl]     NVARCHAR(MAX)  NOT NULL,
    [LastCheckedOn]   DATETIME2      NULL,
    [LastPostGuid]    NVARCHAR(MAX)  NULL,
    [CreatedBy]       NVARCHAR(256)  NOT NULL DEFAULT '',
    [CreatedOn]       DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy]      NVARCHAR(256)  NOT NULL DEFAULT '',
    [ModifiedOn]      DATETIME2      NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_SubstackFeeds] PRIMARY KEY ([FeedId])
);

CREATE INDEX [IX_SubTubeJobs_UserId]    ON [SubTubeJobs]    ([UserId]);
CREATE INDEX [IX_SubstackFeeds_UserId]  ON [SubstackFeeds]  ([UserId]);
