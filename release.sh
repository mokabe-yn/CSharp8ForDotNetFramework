#!/bin/bash

set -eu

# make release package

./release-asset.sh
./release-coding-style.sh
