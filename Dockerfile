FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ItWorksOnMyMachine_org/ItWorksOnMyMachine_org.csproj", "ItWorksOnMyMachine_org/"]
RUN dotnet restore "ItWorksOnMyMachine_org/ItWorksOnMyMachine_org.csproj"
COPY . .
WORKDIR "/src/ItWorksOnMyMachine_org"
RUN dotnet build "ItWorksOnMyMachine_org.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItWorksOnMyMachine_org.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItWorksOnMyMachine_org.dll"]
