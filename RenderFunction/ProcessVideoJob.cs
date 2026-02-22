using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SubTube.Shared.Models;

namespace SubTube.RenderFunction
{
    public class ProcessVideoJob
    {
        [Function("ProcessVideoJob")]
        public async Task Run(
            [QueueTrigger("subtube-jobs")] string messageJson,
            FunctionContext context)
        {
            var logger = context.GetLogger<ProcessVideoJob>();
            SubTubeJob job;
            try
            {
                job = JsonSerializer.Deserialize<SubTubeJob>(messageJson);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Failed to deserialize queue message into SubTubeJob. Message: {MessageJson}", messageJson);
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while deserializing queue message into SubTubeJob. Message: {MessageJson}", messageJson);
                return;
            }

            if (job is null)
            {
                logger.LogError("Deserialized SubTubeJob was null. Message: {MessageJson}", messageJson);
                return;
            }
            logger.LogInformation(
                "Processing job {JobId} for user {UserId}", job.JobId, job.UserId);

            // Phase 2 will implement these:
            // var audioPath = await SynthesizeAudioAsync(job);
            // var videoPath = await RenderVideoAsync(audioPath, job);
            // await UploadToYouTubeAsync(job, videoPath);
        }

        private Task<string> SynthesizeAudioAsync(SubTubeJob job)
            => Task.FromResult(string.Empty); // Phase 2

        private Task<string> RenderVideoAsync(string audioPath, SubTubeJob job)
            => Task.FromResult(string.Empty); // Phase 2

        private Task UploadToYouTubeAsync(SubTubeJob job, string videoPath)
            => Task.CompletedTask; // Phase 2
    }
}
