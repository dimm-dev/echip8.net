FROM ubuntu:20.04

ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get -y install wget --quiet software-properties-common && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb && \
    rm -rf packages-microsoft-prod.deb && add-apt-repository universe && apt-get update && apt-get -y install apt-transport-https && apt-get update && apt-get -y install dotnet-sdk-3.1 && \
    apt-get -y install sudo git-core && apt-get clean && \
    groupadd --gid $USER_GID $USERNAME && \
    adduser --shell /bin/bash --uid $USER_UID --gid $USER_GID --disabled-password --gecos '' $USERNAME && \
    echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME && \
    chmod 0440 /etc/sudoers.d/$USERNAME

USER $USERNAME
