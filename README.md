# copy-base
Quickly overwrite the target file into the base file.

## How to use it?

It is required .NET Core 3.0

```
dotnet run
```

```
CopyBase:
  You can work quickly overwrite the target file into the base file.

Usage:
  CopyBase [options] [command]

Options:
  --version    Display version information

Commands:
  copy <alias>
  alias
```

Example:

```
# Add alias
dotnet run -- alias add <alias-name> <base-file-path> <target-file-path>

# Copy file
dotnet run -- copy <alias-name>
```

