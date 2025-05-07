# Installation Guide

## Prerequisites

- .NET 6.0 SDK or higher
- Visual Studio 2022 or compatible IDE
- Git

## Installation Steps

### 1. Clone the Repository

```bash
git clone https://github.com/MegoM2323/StudentDataAnalyzer.git
cd StudentDataAnalyzer
```

### 2. Open the Solution

- Open Visual Studio 2022
- Select "Open a project or solution"
- Navigate to the cloned repository
- Open `Project2_csv.sln`

### 3. Restore Dependencies

- Right-click on the solution in Solution Explorer
- Select "Restore NuGet Packages"
- Wait for the restoration to complete

### 4. Build the Project

- Select "Build" -> "Build Solution" from the menu
- Or press Ctrl+Shift+B
- Ensure there are no build errors

### 5. Run the Application

- Press F5 to run in debug mode
- Or Ctrl+F5 to run without debugging

## Troubleshooting

### Common Issues

#### Build Errors

- Ensure all NuGet packages are restored
- Check .NET SDK version
- Verify Visual Studio installation

#### Runtime Errors

- Check file permissions
- Verify input file format
- Ensure proper file encoding

#### Dependencies Issues

- Clear NuGet cache
- Update NuGet packages
- Check package compatibility

## Support

For additional support:

1. Check the documentation
2. Open an issue on GitHub
3. Contact the maintainer
