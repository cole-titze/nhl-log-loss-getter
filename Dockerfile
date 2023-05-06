# Get dotnet sdk
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine-arm64v8 AS build-env
WORKDIR /logloss

# Running as root
RUN apk update && apk upgrade

# Build app
COPY . ./
RUN dotnet publish Entry --self-contained -r linux-musl-arm64 -p:PublishSingleFile=true -c Release -o ./deploy

# Generate image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine-arm64v8
WORKDIR /logloss

# Running as root
RUN apk update && apk upgrade

ENV DOTNET_RUNNING_IN_CONTAINER=true
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN addgroup -S loglossgroup && adduser -S loglossuser 
USER loglossuser

COPY --from=build-env --chown=loglossuser:loglossgroup /logloss/deploy/Entry .
ENTRYPOINT ["./Entry"]