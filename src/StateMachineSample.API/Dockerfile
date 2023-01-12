FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StateMachineSample.API/StateMachineSample.API.csproj", "src/StateMachineSample.API/"]
RUN dotnet restore "src/StateMachineSample.API/StateMachineSample.API.csproj"
COPY . .
WORKDIR "/src/src/StateMachineSample.API"
RUN dotnet build "StateMachineSample.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StateMachineSample.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StateMachineSample.API.dll"]
