# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# В зависимости от операционной системы хост-компьютеров, которые будут выполнять сборку контейнеров или запускать их, может потребоваться изменить образ, указанный в инструкции FROM.
# Дополнительные сведения см. на странице https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV LANG C.UTF-8
ENV LC_ALL C.UTF-8
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Shared/Infrastructure.Abstractions/Infrastructure.Abstractions.csproj", "Shared/Infrastructure.Abstractions/"]
COPY ["NotificationsService/NotificationService.Domain/NotificationService.Domain.csproj", "NotificationsService/NotificationService.Domain/"]
COPY ["NotificationsService/NotificationService.Application/NotificationService.Application.csproj", "NotificationsService/NotificationService.Application/"]
COPY ["NotificationsService/NotificationService.Infrastructure/NotificationService.Infrastructure.csproj", "NotificationsService/NotificationService.Infrastructure/"]
COPY ["NotificationsService/NotificationService.API/NotificationService.API.csproj", "NotificationsService/NotificationService.API/"]

RUN dotnet tool install --global dotnet-ef
RUN dotnet restore "NotificationsService/NotificationService.API/NotificationService.API.csproj"

COPY . .
WORKDIR "/src/NotificationsService/NotificationService.API"
RUN dotnet build "NotificationService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV LANG C.UTF-8
ENV LC_ALL C.UTF-8
ENTRYPOINT ["dotnet", "NotificationService.API.dll"]