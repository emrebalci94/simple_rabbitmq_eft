FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore simple_rabbitmq_eft.Api

# Copy everything else and build
COPY . ./
RUN dotnet publish simple_rabbitmq_eft.Api/simple_rabbitmq_eft.Api.csproj -c Release -o ../out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
#build-env aliası veriyoruz yukarıda ordan çağırıyoruz: Orda oluşturulan dosyaları /app/out klasörünü kopyala demiş oluyoruz.
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet" , "simple_rabbitmq_eft.Api.dll"]