#!/bin/bash

source ".vscode/scripts/test.sh"
source ".vscode/scripts/commit.sh"
source ".vscode/scripts/revert.sh"

test && commit || revert