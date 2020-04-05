output_dir = bin/
project_file = OpenChart/OpenChart.csproj

all: build run
.PHONY: all build clean run test

build:
	dotnet build -o $(output_dir) $(project_file)
	cp -r lib/x64/* noteskins/ $(output_dir)

clean:
	rm -rf $(output_dir) OpenChart/bin/ OpenChart.Tests/bin/

run:
	dotnet $(output_dir)/OpenChart.dll

test:
	dotnet test