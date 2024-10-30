{
  description = "CLI meeting planner - backend course";

  inputs = {
    flake-utils.url = "github:numtide/flake-utils";
    # NOTE: this CLI application is primitive enough to avoid the direct need for Rider,
    # however the pre-requisites to enable it in a dev-shell remain in place for easy access.
    # nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable"; # Rider is only 'free' in channels newer than 24.05
  };

  outputs = {
    self,
    nixpkgs,
    flake-utils,
  }:
    flake-utils.lib.eachDefaultSystem (
      system: let
        pkgs = nixpkgs.legacyPackages.${system};
        # pkgs = import nixpkgs {
        #   inherit system;
        #   # config.allowUnfree = true; # not neccessary unless we need Rider in a dev shell
        # };

        dotnetPkg = with pkgs.dotnetCorePackages;
        # NOTE: the first sdk in the list is the one whose cli utility is present in the environment
          combinePackages [
            sdk_9_0
            aspnetcore_9_0
            sdk_8_0
          ];
      in {
        devShells.default = pkgs.mkShell {
          buildInputs = with pkgs; [];

          nativeBuildInputs = with pkgs; [
            dotnetPkg
            csharp-ls
            # # Testing jetbrains rider IDE for C# now that it is 'free'
            # jetbrains.rider # Requires unstable channel (2024-10-30 and unfree)
            sqlite
            mermaid-cli
          ];
          shellHook = ''
            export DOTNET_ROOT="${dotnetPkg}";
            echo ".net root: '${dotnetPkg}'"
            echo ".net version: $(${dotnetPkg}/bin/dotnet --version)"
            echo ".net SDKs:"
            echo "$(${dotnetPkg}/bin/dotnet --list-sdks)"
            echo "sqlite version: $(${pkgs.sqlite}/bin/sqlite3 -version)"
            echo "Flake shell active..."
          '';
        };
      }
    );
}
