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
            var job = JsonSerializer.Deserialize<SubTubeJob>(messageJson);
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
