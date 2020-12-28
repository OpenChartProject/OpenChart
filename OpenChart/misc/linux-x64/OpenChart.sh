#!/bin/bash

cd ${0%/*}

export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:.
mono ./OpenChart.exe
