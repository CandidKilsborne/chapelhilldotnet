FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install necessary packages
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends \
    git \
    curl \
    procps

# Create a non-root user
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

RUN groupadd --gid $USER_GID $USERNAME \
    && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME

# [Optional] Set the default user
USER $USERNAME

# Configure global CLI tools
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1