FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
# Setup NodeJs
RUN apt-get update -qq && \
    apt-get install -qq -y wget && \
    apt-get install -qq -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -qq -y build-essential nodejs && \
    apt-get install -qq -y nginx
# End setup

WORKDIR /app

EXPOSE 5050

FROM microsoft/dotnet:2.2-sdk AS build
# Setup NodeJs
RUN apt-get update -qq && \
    apt-get install -qq -y wget && \
    apt-get install -qq -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -qq -y build-essential nodejs && \
    apt-get install -qq -y nginx
# End setup

WORKDIR /src
COPY ["AspNetCoreSpa.Web.csproj", "./AspNetCoreSpa.Web/"]
RUN dotnet restore "./AspNetCoreSpa.Web/AspNetCoreSpa.Web.csproj"
COPY ["ClientApp/package.json", "./AspNetCoreSpa.Web/ClientApp/"]

RUN cd ./AspNetCoreSpa.Web/ClientApp \
    && npm i --silent

COPY . ./AspNetCoreSpa.Web/
WORKDIR "/src/AspNetCoreSpa.Web"
RUN dotnet build "AspNetCoreSpa.Web.csproj" -c Release -o /app

FROM build AS publish

RUN dotnet publish "AspNetCoreSpa.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCoreSpa.Web.dll"]