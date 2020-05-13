#!/bin/bash

cd ${0%/*}

export PATH=$PATH:lib:lib/gtk/bin
./OpenChart.exe
