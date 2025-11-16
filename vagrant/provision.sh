#!/usr/bin/env bash

echo "=== Updating system ==="
apt update -y
apt install -y wget git curl unzip

echo "=== Installing .NET 8 SDK ==="
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0
export PATH="$PATH:/root/.dotnet"

echo "=== Cloning the repository ==="
git clone https://github.com/Cmetanka1337/AgroindustryManagement.git
cd AgroindustryManagement

echo "=== Running the application ==="
/root/.dotnet/dotnet run --project AgroindustryManagement