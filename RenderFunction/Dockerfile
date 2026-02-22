FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated10.0
RUN apt-get update && apt-get install -y ffmpeg
COPY . /home/site/wwwroot
