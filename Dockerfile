FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

RUN apk add --no-cache clang zlib-dev musl-dev

COPY . .
RUN cd server && dotnet publish -c Release -o out && rm -rf out/Gray.Server.dbg

FROM alpine AS runtime
WORKDIR /app

RUN apk add --no-cache fontconfig

COPY --from=build /app/server/out .

ENTRYPOINT [ "/app/Gray.Server" ]
