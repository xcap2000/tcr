#!/bin/bash

function test() {
  dotnet test /clp:NoSummary /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info
}