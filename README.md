# copy-base

기본 베이스 파일을 덮어쓰길 원하는 파일로 빠르게 복사할 수 있습니다.

## 설치방법

.NET Core 3.0를 필요로 합니다.

해당 튜토리얼은 Windows 10을 기준으로 설명합니다.

먼저 해당 프로젝트를 아래 명령어로 빌드합니다.

```
dotnet publish -c Release -o [원하는_폴더]
```

그리고 `Path` 환경변수에 프로젝트가 빌드된 위치를 추가합니다.

설치는 끝났습니다. 좀 더 편하게 사용하기 위해서는 PowerShell로 alias를 설정해주는 것을 추천합니다.

PowerShell에서 새 프로필을 만들기 위해 아래 명령어를 입력합니다.

```
if (!(Test-Path -Path $PROFILE ))
{ New-Item -Type File -Path $PROFILE -Force }
```

PowerShell에서 새로운 세션을 시작할 때마다 Alias를 설정하기 위해 메모장을 열어 프로필을 편집합니다.

```
notepad $PROFILE
```

메모장에 아래 명령어를 추가합니다.

```
Set-Alias cb CopyBase.exe
```

이제 `cb`만 입력하는 것으로도 copybase를 사용할 수 있습니다.

# 사용방법

```
copybase:
  You can work quickly overwrite the target file into the base file.

Usage:
  copybase [options] [command]

Options:
  --version    Display version information

Commands:
  copy <alias>
  alias
```

Example:

```
# Add alias

copybase alias add <alias-name> <base-file-path> <target-file-path>

# Copy file

copybase copy <alias-name>

```
