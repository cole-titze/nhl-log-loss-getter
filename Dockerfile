# Get dotnet sdk
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine-arm64v8 AS build-env
WORKDIR /logloss

# Build app
COPY . ./
RUN dotnet publish nhl-log-loss-getter --self-contained -r linux-musl-arm64 -p:PublishSingleFile=true -c Release -o ./deploy

# Generate image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine-arm64v8
WORKDIR /logloss
ENV DOTNET_RUNNING_IN_CONTAINER=true
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN addgroup -S loglossgroup && adduser -S loglossuser 
USER loglossuser
COPY --from=build-env --chown=loglossuser:loglossgroup /logloss/deploy/nhl-logloss-getter .
ENTRYPOINT ["./nhl-logloss-getter"]