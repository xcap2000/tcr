#!/bin/bash

while true; do
  inotifywait -r -e modify src test
  pwd
  ./.vscode/tcr.sh
done