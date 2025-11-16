Vagrant.configure("2") do |config|

  # --------------------------
  # Linux VM
  # --------------------------
  config.vm.define "linux" do |linux|
    linux.vm.box = "ubuntu/focal64"
    linux.vm.hostname = "agro-linux"

    linux.vm.provider "virtualbox" do |vb|
      vb.memory = 2048
      vb.cpus = 2
    end

    linux.vm.provision "shell", inline: <<-SHELL
      echo "=== Updating system ==="
      sudo apt update -y
      sudo apt install -y wget git curl unzip

      echo "=== Installing .NET 8 SDK ==="
      wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
      chmod +x dotnet-install.sh
      ./dotnet-install.sh --channel 8.0
      export PATH="$PATH:/root/.dotnet"

      echo "=== Cloning repository ==="
      git clone https://github.com/Cmetanka1337/AgroindustryManagement.git
      cd AgroindustryManagement

      echo "=== Running application ==="
      /root/.dotnet/dotnet run --project AgroindustryManagement
    SHELL
  end

  # --------------------------
  # Windows VM
  # --------------------------
  config.vm.define "windows" do |win|
    win.vm.box = "gusztavvargadr/windows-10"
    win.vm.hostname = "agro-windows"
    win.vm.communicator = "winrm"

    win.vm.provider "virtualbox" do |vb|
      vb.memory = 4096
      vb.cpus = 2
    end

    win.vm.provision "shell", inline: <<-SHELL
      Write-Host "=== Installing .NET 8 SDK ==="
      iex "& { $(irm https://dot.net/v1/dotnet-install.ps1) } -Channel 8.0"

      Write-Host "=== Cloning repository ==="
      git clone https://github.com/Cmetanka1337/AgroindustryManagement.git
      cd AgroindustryManagement

      Write-Host "=== Running application ==="
      dotnet run --project AgroindustryManagement
    SHELL
  end

end