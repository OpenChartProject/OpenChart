assets_dir = OpenChart/assets
lib_dir = OpenChart/lib
output_dir = bin
project_file = OpenChart/OpenChart.csproj

all: build run
.PHONY: all build clean run test

build:
	dotnet build -o $(output_dir) $(project_file)
	cp -r $(lib_dir)/x64/* $(assets_dir)/* $(output_dir)

clean:
	rm -rf $(output_dir) OpenChart/bin/ OpenChart.Tests/bin/

run:
	./$(output_dir)/OpenChart

test:
	dotnet test