FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StateMachineSample.StateMachine/StateMachineSample.StateMachine.csproj", "src/StateMachineSample.StateMachine/"]
RUN dotnet restore "src/StateMachineSample.StateMachine/StateMachineSample.StateMachine.csproj"
COPY . .
WORKDIR "/src/src/StateMachineSample.StateMachine"
RUN dotnet build "StateMachineSample.StateMachine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StateMachineSample.StateMachine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StateMachineSample.StateMachine.dll"]
