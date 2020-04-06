SHELL := /bin/bash
assets_dir := OpenChart/assets
lib_dir := OpenChart/lib
output_dir := bin
project_file := OpenChart/OpenChart.csproj

# OS detection. Do not assume the existence of uname on Windows.
ifeq ($(OS), Windows_NT)
    detected_os := Windows_NT
else
    detected_os := $(shell uname)
endif

all: build run
.PHONY: all build clean cleanall run test

build:
	dotnet build -o $(output_dir) $(project_file)

	@# Copy dependencies and runtime assets
	@if [[ "$(detected_os)" == "Darwin" ]]; then \
		cp -r $(lib_dir)/osx/* $(assets_dir)/* $(output_dir); \
	else \
		cp -r $(lib_dir)/x64/* $(assets_dir)/* $(output_dir); \
	fi

clean:
	rm -rf $(output_dir) OpenChart/bin/ OpenChart.Tests/bin/

cleanall: clean
	rm -rf OpenChart/obj/ OpenChart.Tests/obj/

run:
	@echo
	@echo "Starting OpenChart..."
	@if [[ "$(detected_os)" == "Windows_NT" ]]; then \
		./$(output_dir)/OpenChart.exe; \
	elif [[ "$(detected_os)" == "Linux" ]]; then \
		./$(output_dir)/OpenChart; \
	else \
		dotnet $(output_dir)/OpenChart.dll; \
	fi

test:
	dotnet test
