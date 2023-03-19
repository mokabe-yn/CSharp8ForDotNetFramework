#!/bin/bash

cd $(dirname "$0")


git ls-files | grep '\.cs$' | xargs python release-coding-style.py

