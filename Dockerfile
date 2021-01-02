FROM mono:6.12

# Add the path to the nunit runner.
ENV PATH=$PATH:/root/.nuget/packages/nunit.consolerunner/3.12.0-beta1/tools

RUN apt-get update && apt-get install -y \
    libsdl2-dev \
    libsdl2-image-dev

WORKDIR /app
COPY . .

RUN ./tasks.sh restore
