output_dir = bin/Debug/netcoreapp3.1

all: build run
.PHONY: build clean run

build:
	dotnet build
	cp lib/x64/* $(output_dir)

clean:
	rm -rf bin/

run:
	dotnet run