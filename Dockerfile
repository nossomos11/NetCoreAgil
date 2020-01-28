FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
EXPOSE 5000

# copy the projects
WORKDIR /src
COPY *.sln ./
COPY ProAgil.API/. ./ProAgil.API/
COPY ProAgil.Domain/. ./ProAgil.Domain/
COPY ProAgil.Repository/. ./ProAgil.Repository/
RUN dotnet restore
COPY . .

WORKDIR /src/ProAgil.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/dist

FROM publish AS final
WORKDIR /app
COPY --from=publish /app/dist ./
ENTRYPOINT ["dotnet", "ProAgil.API.dll"]
