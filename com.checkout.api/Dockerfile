##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
##FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["com.checkout.api/com.checkout.api.csproj", "com.checkout.api/"]
#COPY ["com.checkout.data/com.checkout.data.csproj", "com.checkout.data/"]
#COPY ["com.checkout.application/com.checkout.application.csproj", "com.checkout.application/"]
#RUN dotnet restore "com.checkout.api/com.checkout.api.csproj"
#COPY . .
#WORKDIR "/src/com.checkout.api"
#RUN dotnet build "com.checkout.api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "com.checkout.api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "com.checkout.api.dll"]