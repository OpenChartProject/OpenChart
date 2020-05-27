#!/bin/bash

cd ${0%/*}

export PATH=$PATH:lib:lib/gtk
./OpenChart.exe
