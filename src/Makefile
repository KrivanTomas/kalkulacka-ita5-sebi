
all:
	dotnet build ./SmolCalc/
	
pack: clean
	#dotnet build <project-installer>
	#tar/zip/7z???

clean:
	rm -rf ./*/bin/ ./*/obj/ ../.vs */.vs ../repo.zip
	#git clean ?

test:
	dotnet test ./SmolMathLibTests/

run:
	dotnet run --project ./SmolCalc/

stddev:
	# Not yet implemented

help:
	# Make sure you have .NET Core 8.0.x and .NET SDK 9.0.201 or newer installed
	# https://aka.ms/dotnet/download
	# Ensure that the `dotnet` cli utility is availible
	# Try running `dotnet --info` for version information