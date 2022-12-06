# MedicineReminder

Requirements:
Azure CLI
[https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli

Azure Functions Core Tools
https://github.com/Azure/azure-functions-core-tools

### how to initiate a new project

### how to download existing config file
Download config file (local.settings.json)
`az login`

`func azure functionapp fetch-app-settings <APPNAME>`

`func settings decrypt local.settings.json`

### Run and Deploy

Run
`func start`

Deploy functions
`func azure functionapp publish <APPNAME>`

Get streamlog
`func azure functionapp logstream medicinereminder <APPNAME>`
