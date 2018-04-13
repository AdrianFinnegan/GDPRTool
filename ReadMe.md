# GDPR Tool

## Overview
Tool to allow Mark to take a folder of documents older than 2 years and copy them to a new folder, modifying the timestamp to suggest they were created today.

### Requirements
Runs on a Windows platform with .net 4 or better installed

### Execution
* Build the application and navigate to the Release or Debug folder.
* From the command line execute:
  >GDPRTool.exe C:\MyDocuments

Where the location is the source of files to be processed.

A pre compiled copy can be found in the PreBuilt folder. Unzip and run as above