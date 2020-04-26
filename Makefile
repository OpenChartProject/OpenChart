SHELL := /bin/bash
assets_dir := OpenChart/assets
lib_dir := OpenChart/lib
output_dir := bin
publish_dir := dist

# OS detection. Do not assume the existence of uname on Windows.
ifeq ($(OS), Windows_NT)
    detected_os := Windows_NT
else
    detected_os := $(shell uname)
endif

.PHONY: all
all: build run

.PHONY: build
build:
	dotnet build -o $(output_dir) OpenChart/OpenChart.csproj

	@# Copy dependencies and runtime assets
	@if [[ "$(detected_os)" == "Darwin" ]]; then \
		cp -r $(lib_dir)/osx/* $(assets_dir)/* $(output_dir); \
	else \
		cp -r $(lib_dir)/x64/* $(assets_dir)/* $(output_dir); \
	fi

.PHONY: clean
clean:
	rm -rf $(output_dir) $(publish_dir) OpenChart/bin/ OpenChart.Tests/bin/

.PHONY: cleanall
cleanall: clean
	rm -rf OpenChart/obj/ OpenChart.Tests/obj/

.PHONY: publish
publish: publish-linux publish-osx publish-win

.PHONY: publish-linux
publish-linux:
	rm -rf $(publish_dir)/linux-x64
	dotnet publish -o $(publish_dir)/linux-x64 -r linux-x64 -c Release OpenChart
	cp -r $(lib_dir)/x64/*.so $(assets_dir)/* $(publish_dir)/linux-x64

.PHONY: publish-osx
publish-osx:
	rm -rf $(publish_dir)/osx-x64
	dotnet publish -o $(publish_dir)/osx-x64 -r osx-x64 -c Release OpenChart
	chmod +x $(publish_dir)/osx-x64/OpenChart
	cp -r $(lib_dir)/osx/* $(assets_dir)/* $(publish_dir)/osx-x64

.PHONY: publish-win
publish-win:
	rm -rf $(publish_dir)/win-x64
	dotnet publish -o $(publish_dir)/win-x64 -r win-x64 -c Release OpenChart
	cp -r $(lib_dir)/x64/*.dll $(assets_dir)/* $(publish_dir)/win-x64

.PHONY: run
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

.PHONY: test
test:
	dotnet test
