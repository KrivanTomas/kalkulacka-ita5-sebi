PACK_NAME = "xkrivat00_xhavlap00_xkurtil00_xkunseb00"


.PHONY: all
all: smolcalc stddev
	
smolcalc: 
	dotnet build ./SmolCalc/ -c Release

.PHONY: pack
pack: clean
	cd ..; mkdir $(PACK_NAME); cd $(PACK_NAME); mkdir repo; mkdir doc; mkdir install
	cd ..; cp ./* ./.* ./$(PACK_NAME)/repo; cp -r ./plan ./src ./profiling ./mockup ./.git ./$(PACK_NAME)/repo

	doxygen
	cd ..; cp -r ./src/docs/* ./$(PACK_NAME)/doc

	read -p "Please build the installer projects (in Visual Studio) and press Enter to continue"

	cd ..; cp ./src/SmolCalcInstaller/Release/SmolCalcInstaller.msi ./$(PACK_NAME)/install
	cd ..; cp ./src/StandardDeviationCalculatorInstaller/Release/StandardDeviationCalculatorInstaller.msi ./$(PACK_NAME)/install


	cd ..; zip -r $(PACK_NAME).zip $(PACK_NAME)/*
	cd ..; rm -rf ./$(PACK_NAME)

.PHONY: doc
doc:
	doxygen

.PHONY: clean
clean:
	rm -rf ./*/bin/ ./*/obj/ ../.vs */.vs ../repo.zip
	cd ./../; git clean -f

.PHONY: test
test:
	dotnet test ./SmolMathLibTests/ -c Release

.PHONY: run
run:
	dotnet run --project ./SmolCalc/ -c Release

stddev: stddev-build
	cat ./StandardDeviationCalculator/testFiles/input1.txt | ./StandardDeviationCalculator/bin/Release/net8.0-windows/stddev.exe

stddev-build:
	dotnet build ./StandardDeviationCalculator -c Release

.PHONY: help
help:
	# Make sure you have .NET Core 8.0.x and .NET SDK 9.0.201 or newer installed
	# https://aka.ms/dotnet/download
	# Ensure that the `dotnet` cli utility is availible
	# Try running `dotnet --info` for version information